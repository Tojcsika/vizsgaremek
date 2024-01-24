import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, provideRouter } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { DividerModule } from 'primeng/divider';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';

import { AppComponent } from './app.component';
import { SigninRedirectCallbackComponent } from './signin-redirect-callback/signin-redirect-callback.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { SignoutRedirectCallbackComponent } from './signout-redirect-callback/signout-redirect-callback.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { UnauthorizedComponent } from './error-pages/unauthorized/unauthorized.component';
import { StoragesComponent } from './storages/storages.component';
import { StorageComponent } from './storage/storage.component';
import { StorageRackComponent } from './storage-rack/storage-rack.component';
import { ShelfComponent } from './shelf/shelf.component';
import { StorageEditComponent } from './storages/storage-edit.component';
import { StorageRackEditComponent } from './storage/storage-rack-edit.component';
import { ShelfEditComponent } from './storage-rack/shelf-edit.component';
import { ProductsComponent } from './products/products.component';
import { ProductEditComponent } from './products/product-edit.component';
import { ShelfProductEditComponent } from './shelf/shelf-product-edit.component';
import { SearchProductComponent } from './search-product/search-product.component';
import { ProductAvailabilityComponent } from './product-availability/product-availability.component';

@NgModule({
  declarations: [
    AppComponent,
    UnauthorizedComponent,
    SigninRedirectCallbackComponent,
    ProductsComponent,
    ProductEditComponent,
    StoragesComponent,
    StorageComponent,
    StorageEditComponent,
    StorageRackComponent,
    StorageRackEditComponent,
    ShelfComponent,
    ShelfEditComponent,
    ShelfProductEditComponent,
    SearchProductComponent,
    ProductAvailabilityComponent
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToolbarModule,
    ButtonModule,
    MenuModule,
    FormsModule,
    DividerModule,
    CardModule,
    CheckboxModule,
    InputTextModule,
    TableModule,
    DialogModule,
    DropdownModule,
    RouterModule.forRoot([
      { path: 'storages', component: StoragesComponent },
      {
        path: 'storage/:storageId',
        component: StorageComponent,
        pathMatch: 'full',
      },
      {
        path: 'storageracks/:storageRackId',
        component: StorageRackComponent,
        pathMatch: 'full',
      },
      {
        path: 'shelves/:shelfId',
        component: ShelfComponent,
        pathMatch: 'full',
      },
      {
        path: 'products',
        component: ProductsComponent,
        pathMatch: 'full',
      },
      {
        path: 'searchProduct',
        component: SearchProductComponent,
        pathMatch: 'full',
      },
      {
        path: 'productAvailability/:productId',
        component: ProductAvailabilityComponent,
        pathMatch: 'full',
      },
      { path: 'signin-callback', component: SigninRedirectCallbackComponent },
      {
        path: 'signout-callback',
        component: SignoutRedirectCallbackComponent,
      },
      { path: 'unauthorized', component: UnauthorizedComponent },
      { path: '', redirectTo: '/storages', pathMatch: 'full' },
      { path: '**', component: NotFoundComponent },
    ]),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
