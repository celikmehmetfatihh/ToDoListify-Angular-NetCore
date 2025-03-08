import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
// Binds form controls like inputs, checkboxes to the component's properties.
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/components/navbar/navbar.component';
import { TodoListComponent } from './features/todo/todo-list/todo-list.component';
import { TodoAddComponent } from './features/todo/todo-add/todo-add.component';
import { LoginComponent } from './features/auth/login/login.component';
// Angular service to be able to talk to an external API
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { TodoEditComponent } from './features/todo/todo-edit/todo-edit.component';
import { HomeComponent } from './features/home/home.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    TodoListComponent,
    TodoAddComponent,
    LoginComponent,
    TodoEditComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
