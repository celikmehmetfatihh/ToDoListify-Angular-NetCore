import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodoListComponent } from './features/todo/todo-list/todo-list.component';
import { TodoAddComponent } from './features/todo/todo-add/todo-add.component';
import { LoginComponent } from './features/auth/login/login.component';
import { TodoEditComponent } from './features/todo/todo-edit/todo-edit.component';
import { HomeComponent } from './features/home/home.component';

const routes: Routes = [
  {
    // When user goes to tasks/displaytasks -> show to todoListComponent
    path: 'tasks/displaytasks',
    component: TodoListComponent
  },
  {
    path: 'tasks/displaytasks/add',
    component: TodoAddComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'tasks/displaytasks/:id',
    component: TodoEditComponent
  },
  { 
    path: 'home',
     component: HomeComponent
  },
  { 
    path: '', redirectTo: '/home', pathMatch: 'full' // This should redirect the default route to 'home'
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
