import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { Navbar } from "../../components/navbar/navbar";
import { Table } from "../../components/table/table";
import { AuthorTable } from "../../components/author-table/author-table";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [Navbar, AuthorTable],
  templateUrl: './my-terms.html',
  styleUrls: ['./my-terms.css']
})
export class MyTerms implements OnInit {

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
