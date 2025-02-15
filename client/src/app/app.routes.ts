import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';
import { Routes } from '@angular/router';
import { CartComponent } from './features/cart/cart.component';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';
import { OrderComponent } from './features/order/order.component';

export const routes: Routes = [
  { path: '', redirectTo: 'shop', pathMatch: 'full' },
  { path: 'shop', component: ShopComponent },
  { path: 'shop/:id', component: ProductDetailsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'orders', component: OrderComponent },
  {
    path: 'checkout',
    loadChildren: () =>
      import('./features/checkout/routes').then((r) => r.checkoutRoutes),
  },
  {
    path: 'account',
    loadChildren: () =>
      import('./features/account/routes').then((r) => r.accountRoutes),
  },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  {
    path: 'admin',
    loadComponent: () =>
      import('./features/admin/admin.component').then((c) => c.AdminComponent),
    canActivate: [authGuard, adminGuard],
  },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' },
];
