import { Injectable } from '@angular/core';
import { AddToDoRequest } from '../models/add-todo-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Priority } from '../models/priority.model';
import { CookieService } from 'ngx-cookie-service';
import { ToDoItem } from '../models/todoitem.model';
import { environment } from 'src/environments/environment.development';
import { UpdateToDoRequest } from '../models/update-todo-request.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  // Inject HttpClientModule
  constructor(private http: HttpClient,
    private cookieService: CookieService
  ) { }

// gets list of properties from the backend
  getPriorities(): Observable<Priority[]> {
    return this.http.get<Priority[]>(`${environment.apiBaseUrl}/api/Priorities?addAuth=true`);
  }

  //   Observable like a promise which you have to subscribe to and once you have subscribed, you can get the values from
  // that observable.
  addToDo(model: AddToDoRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/ToDoItems?addAuth=true`, model);
  }

    // Fetch pending To-Do items from the backend
    getPendingTodos(): Observable<ToDoItem[]> {
      return this.http.get<ToDoItem[]>(`${environment.apiBaseUrl}/api/ToDoItems/pending?addAuth=true`);
    }
  
    // Fetch completed To-Do items from the backend
    getCompletedTodos(): Observable<ToDoItem[]> {
      return this.http.get<ToDoItem[]>(`${environment.apiBaseUrl}/api/ToDoItems/completed?addAuth=true`);
    }

    getToDoById(id: string): Observable<ToDoItem> {
      return this.http.get<ToDoItem>(`${environment.apiBaseUrl}/api/ToDoItems/${id}?addAuth=true`);
    }

    updateToDo(id: string, updateToDoRequest: UpdateToDoRequest): Observable<ToDoItem> {
      return this.http.put<ToDoItem>(`${environment.apiBaseUrl}/api/ToDoItems/${id}?addAuth=true`, updateToDoRequest);
    }

    deleteToDo(id: string): Observable<ToDoItem> {
      return this.http.delete<ToDoItem>(`${environment.apiBaseUrl}/api/ToDoItems/${id}?addAuth=true`);
    }
}
