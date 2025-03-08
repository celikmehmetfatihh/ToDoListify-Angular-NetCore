import { Component } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  // Use this model in html
  model: LoginRequest;
  loginError: string | null = null; // To handle login error messages

  constructor(private authService: AuthService, 
              private cookieService: CookieService,
              private router: Router) {
    this.model = {
      email: '',
      password: ''
    };
  }

  onFormSubmit(): void {
    if (this.model.email && this.model.password) {
      this.authService.login(this.model)
        .subscribe({
          next: (response) => {
            // Clear previous errors
            this.loginError = null;

            // Set Auth Cookie
            this.cookieService.set('Authorization', `Bearer ${response.token}`,
              undefined, '/', undefined, true, 'Strict');

            // Set User in the local storage
            this.authService.setUser({
              email: response.email,
              username: response.username
            });

            // Redirect to Home
            this.router.navigateByUrl('/');
          },
          error: (error) => {
            // Handle login failure
            this.loginError = 'Invalid email or password. Please try again.';
          }
        });
    }
  }
}
