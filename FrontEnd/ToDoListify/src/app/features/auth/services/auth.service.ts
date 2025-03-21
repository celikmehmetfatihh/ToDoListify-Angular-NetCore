import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  // When logged in, this behavior subject will emit the user value
  $user = new BehaviorSubject<User | undefined>(undefined);

  constructor(private http: HttpClient,
    private cookieService: CookieService
  ) { }

  // email and password should match exactly wih api
  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>('https://localhost:7220/api/Auth/login', {
      email: request.email,
      password: request.password
    });
  }

  setUser(user: User): void {

    this.$user.next(user);
    localStorage.setItem('user-email', user.email);
    // localStorage.setItem('user-roles', user.roles.join(','));
    localStorage.setItem('user-name', user.username);
  }

  // Allows other components to subscribe to the user state, and be notified when the user changes.
  user(): Observable<User | undefined> {
    return this.$user.asObservable();
  }

  getUser(): User | undefined {
    const email = localStorage.getItem('user-email');
    const username = localStorage.getItem('user-name');

    if (email && username) {
      const user: User = {
        email: email,
        username: username
      }

      return user;
    }
    return undefined;
  }

  getUserIdByEmail(email: string): Observable<string> {
    return this.http.get<string>(`https://localhost:7220/api/Auth/GetUserIdByEmail?email=${email}`);
  }

  logout(): void {
    localStorage.clear();
    this.cookieService.delete('Authorization', '/');
    this.$user.next(undefined);
  }
}
