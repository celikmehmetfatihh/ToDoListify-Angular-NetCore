import { Component, OnInit } from '@angular/core';
import { TodoService } from '../services/todo.service';
import { ToDoItem } from '../models/todoitem.model';
import { Priority } from '../models/priority.model';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit{

  pendingTodos?: ToDoItem[];
  completedTodos?: ToDoItem[];
  priorities: Priority[] = [];  // To store priority levels
  

  constructor(private todoService: TodoService) {
  }

  ngOnInit(): void {
    // Returns Observable, we have the subscribe to it -> get pendingToDos from backend as array
    this.todoService.getPendingTodos()
    .subscribe({
      next: (response) => {
        this.pendingTodos = response;
      }
    });


    this.todoService.getCompletedTodos()
      .subscribe({
        next: (response) => {
          this.completedTodos = response;
        }
      });

      this.todoService.getPriorities()
      .subscribe({
        next: (priorities) => {
          this.priorities = priorities;
        },
        error: (error) => {
          console.error('Error fetching priorities:', error);
        }
      });
    
  }

  // Method to get the priority level by ID
  getPriorityLevel(priorityId: string | null | undefined): string {
    const priority = this.priorities.find(p => p.id === priorityId);
    return priority ? priority.priorityLevel : 'Not Set';
  }
}
