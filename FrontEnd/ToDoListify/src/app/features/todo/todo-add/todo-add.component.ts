import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddToDoRequest } from '../models/add-todo-request.model';
import { TodoService } from '../services/todo.service';
import { Priority } from '../models/priority.model';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-todo-add',
  templateUrl: './todo-add.component.html',
  styleUrls: ['./todo-add.component.css']
})
export class TodoAddComponent implements OnInit, OnDestroy {

  model: AddToDoRequest;
  priorities: Priority[] = []; // Array to hold list of priorities
  todayDate: string = new Date().toISOString().split('T')[0];

  private addToDoSubscription?: Subscription;
  private getPrioritiesSubscription?: Subscription;

  constructor(private todoService: TodoService,
              private router: Router,
              private authService: AuthService) {
    this.model = {
      title: '',
      isCompleted: false,
      detail: '',
      priorityId: null,
      userId: ''
    };
  }

  ngOnInit() {
    // Fetch the priorities from the service when the component is initialized
    this.getPrioritiesSubscription = this.todoService.getPriorities()
      .subscribe((priorities: Priority[]) => {
        this.priorities = priorities;
      });

    // Retrieve the userId from the backend using the email from localStorage
    const userEmail = localStorage.getItem('user-email');
    if (userEmail) {
      this.authService.getUserIdByEmail(userEmail)
        .subscribe({
          next: (userId) => {
            this.model.userId = userId;
          },
          error: (error) => {
            console.error('Error fetching user ID:', error);
          }
        });
    }
  }

  onFormSubmit() {
    if (this.model.title.trim() === '') {
      console.error('Title is required');
      return;
    }

    console.log(this.model);

    this.addToDoSubscription = this.todoService.addToDo(this.model)
      .subscribe({
        next: (response) => {
          console.log('This was successful!');
          this.router.navigate(['/tasks/displaytasks']);
        },
        error: (error) => {
          console.error('There was an error:', error);
        }
      });
  }

  ngOnDestroy(): void {
    //  When the component is destroyed, unsubscribe from subscriptions
    this.addToDoSubscription?.unsubscribe();
    this.getPrioritiesSubscription?.unsubscribe();
  }
}
