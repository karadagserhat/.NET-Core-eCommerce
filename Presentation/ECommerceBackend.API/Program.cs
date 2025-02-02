using ECommerceBackend.API.Extensions;
using ECommerceBackend.Application;
using ECommerceBackend.Domain.Entities;
using ECommerceBackend.Infrastructure;
using ECommerceBackend.Infrastructure.Helpers;
using ECommerceBackend.Persistence;
using ECommerceBackend.Persistence.Contexts;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using Microsoft.AspNetCore.HttpLogging;
using System.Collections.ObjectModel;
using ECommerceBackend.API.Configurations.ColumnWriters;
using ECommerceBackend.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddControllers();
builder.Services.AddCors();

var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn
        {
            ColumnName = "UserName",
            DataType = System.Data.SqlDbType.NVarChar,
            DataLength = 50,
            AllowNull = true,
        }
    }
};

columnOptions.Store.Remove(StandardColumn.Properties);
columnOptions.Store.Add(StandardColumn.LogEvent);

Logger log = new LoggerConfiguration()
  .WriteTo.Console()
  .WriteTo.File("logs/log.txt")
 .WriteTo.MSSqlServer(
        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
           sinkOptions: new MSSqlServerSinkOptions
           {
               TableName = "logs",
               AutoCreateSqlTable = true
           },
        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
        columnOptions: columnOptions)
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"]!)
    .Enrich.FromLogContext()
    .Enrich.With<CustomUserNameColumn>()
    .MinimumLevel.Information()
  .CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ECommerceBackendDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowCredentials()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));


app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated == true
        ? context.User.Identity.Name
        : "Anonymous";
    using (LogContext.PushProperty("UserName", username))
    {
        await next();
    }
});

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>(); // api/login

app.MapHubs();

try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ECommerceBackendDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, userManager);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

app.Run();
