import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService, AuthorLoginRequest, AuthorRegisterRequest } from '../../services/auth-service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class Register {




  registerForm: FormGroup;

  constructor(private fb: FormBuilder,private authService: AuthService, private router: Router, private snackBar: MatSnackBar) {
    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required,]],
      lastName: ['', Validators.required],
      username: ['', [Validators.required,]],
      password: ['', Validators.required],
    });
  }

  onRegister() {
    this.router.navigate(['/login']);
}


  onSubmit() {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }
  
    const registerRequest: AuthorRegisterRequest = this.registerForm.value;
  
    this.authService.register(registerRequest).subscribe({
      next: (response) => {
        this.snackBar.open('Registration successful! You can now log in.', 'Close', {
          duration: 3000,
        });
        this.router.navigate(['/login']);
        
      },
      error: (err) => {
          this.snackBar.open(err.error.error, 'Close', {
            duration: 3000,
          });
      }
    });
  }
}
