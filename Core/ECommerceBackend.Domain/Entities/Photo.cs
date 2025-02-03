using System.ComponentModel.DataAnnotations.Schema;
using ECommerceBackend.Domain.Entities.Common;

namespace ECommerceBackend.Domain.Entities;

[Table("Photos")]
public class Photo : BaseEntity
{
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }


    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}