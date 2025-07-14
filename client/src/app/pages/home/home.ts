import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { Navbar } from "../../components/navbar/navbar";
import { Table } from "../../components/table/table";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [Navbar, Table],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class Home implements OnInit {

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.authService.authenticate().subscribe({
      next: response => {
        console.log('User authenticated', response.body);
      },
      error: err => {
        if (err.status === 401) {
          this.router.navigate(['/login']);
        } else {
          console.error('Authentication check failed', err);
        }
      }
    });
  }
}
