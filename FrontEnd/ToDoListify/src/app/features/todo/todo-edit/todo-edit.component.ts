import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { TodoService } from '../services/todo.service';
import { ToDoItem } from '../models/todoitem.model';
import { Priority } from '../models/priority.model';
import { UpdateToDoRequest } from '../models/update-todo-request.model';

@Component({
  selector: 'app-todo-edit',
  templateUrl: './todo-edit.component.html',
  styleUrls: ['./todo-edit.component.css']
})
export class TodoEditComponent implements OnInit, OnDestroy{

  id: string | null = null;
  priorities: Priority[] = []; // Array to hold list of priorities

  paramsSubscription?: Subscription;
  editToDoSubscription?: Subscription;
  private getPrioritiesSubscription?: Subscription;


  toDoItem: ToDoItem = {
    id: '',
    title: '',
    isCompleted: false,
    createDate: '',
    detail: '',
    priorityId: '',
    userId: ''
  };

  constructor(private route: ActivatedRoute,
    private todoService: TodoService,
    private router: Router
  ) {
    
  }

  ngOnInit(): void {

        // Fetch the priorities from the service when the component is initialized
        this.getPrioritiesSubscription = this.todoService.getPriorities()
        .subscribe((priorities: Priority[]) => {
          this.priorities = priorities;
        });

    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id =  params.get('id');

        if (this.id) {
          // Get data from API for this id
          this.todoService.getToDoById(this.id)
          .subscribe({
            next: (response) => {
              this.toDoItem = response;
            }
          });
        }
      }
    });
  }

  onFormSubmit(): void {
    const updateToDoRequest: UpdateToDoRequest = {
      title: this.toDoItem.title,
      isCompleted: this.toDoItem.isCompleted,
      detail: this.toDoItem.detail ?? '',
      priorityId: this.toDoItem.priorityId ?? '',
      userId: this.toDoItem.userId
    };

    // pass this object to service
    if (this.id) {
      this.editToDoSubscription = this.todoService.updateToDo(this.id, updateToDoRequest)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/tasks/displaytasks');
        }
      });
    }
  }

  onDelete(): void {
    if (this.id) {
      this.todoService.deleteToDo(this.id)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/tasks/displaytasks');
        }
      });
    }

  }

  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editToDoSubscription?.unsubscribe();
  }

}
