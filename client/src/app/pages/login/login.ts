import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService, AuthorLoginRequest } from '../../services/auth-service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './login.html',
  styleUrls: ['./login.css']
})
export class Login {

  loginForm: FormGroup;

  constructor(private fb: FormBuilder,private authService: AuthService) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required,]],
      password: ['', Validators.required],
    });
    console.log('Component initializaaaaed')
  }


  onSubmit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }
  
    const loginRequest: AuthorLoginRequest = this.loginForm.value;
  
    this.authService.login(loginRequest).subscribe({
      next: (response) => {
        console.log('Login successful:', response);
        
      },
      error: (err) => {
        console.error('Login failed', err);
      }
    });
  }
}
