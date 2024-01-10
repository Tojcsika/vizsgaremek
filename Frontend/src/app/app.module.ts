import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HeaderComponent } from './header/header.component';
import { ToolbarModule } from 'primeng/toolbar';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { DividerModule } from 'primeng/divider';
import { CardModule } from 'primeng/card';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';

import { AppComponent } from './app.component';
import { SigninRedirectCallbackComponent } from './signin-redirect-callback/signin-redirect-callback.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { SignoutRedirectCallbackComponent } from './signout-redirect-callback/signout-redirect-callback.component';
import { AuthInterceptorService } from './services/auth-interceptor.service';
import { UnauthorizedComponent } from './error-pages/unauthorized/unauthorized.component';
import { StoragesComponent } from './storages/storages.component';
import { StorageComponent } from './storage/storage.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    UnauthorizedComponent,
    SigninRedirectCallbackComponent,
    StoragesComponent,
    StorageComponent,
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
    RouterModule.forRoot([
      { path: 'storages', component: StoragesComponent, pathMatch: 'full' },
      {
        path: 'storage/:storageId',
        component: StorageComponent,
        pathMatch: 'full',
      },
      { path: 'signin-callback', component: SigninRedirectCallbackComponent },
      { path: 'signout-callback', component: SignoutRedirectCallbackComponent },
      { path: 'unauthorized', component: UnauthorizedComponent },
      { path: '', redirectTo: '/storages', pathMatch: 'full' },
      //{ path: '404', component: NotFoundComponent },
      //{ path: '**', redirectTo: '/404', pathMatch: 'full' },
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
