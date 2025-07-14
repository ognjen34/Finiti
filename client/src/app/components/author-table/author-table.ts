import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { GlossaryTermService } from '../../services/glossary-term-service';
import { MatDialog } from '@angular/material/dialog';
import { AddTermDialog } from '../add-term-dialog/add-term-dialog';
import {
  TermResponse,
  PaginationReturnObject,
  PaginationFilter,
  CreateTermRequest,
} from '../../services/models/glossary-term-model';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-author-table',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
  ],
  templateUrl: './author-table.html',
  styleUrls: ['./author-table.css'],
})
export class AuthorTable implements OnInit {
  displayedColumns: string[] = ['term', 'definition', 'status', 'actions'];

  dataSource: TermResponse[] = [];

  pageSize = 10;
  pageIndex = 0;
  length = 0;

  termQuery: string = '';
  authorQuery: string = '';

  private filterChange$ = new Subject<void>();

  constructor(
    private glossaryTermService: GlossaryTermService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.filterChange$.pipe(debounceTime(1000)).subscribe(() => {
      this.pageIndex = 0;
      this.loadData();
    });

    this.loadData();
  }
  onAddTerm() {
    const dialogRef = this.dialog.open(AddTermDialog, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        let request: CreateTermRequest = {
          term: result.term,
          definition: result.definition,
        };
        this.glossaryTermService
          .createTerm(request)
          .subscribe({
            next: () => {
              this.snackBar.open('Term added successfully', 'Close', {
                duration: 3000,
              });
              this.loadData();
            },
            error: (err) => {
              console.error('Error adding term', err);
              this.snackBar.open(
                'Error adding term: ' + (err.error?.error || err.message),
                'Close',
                { duration: 5000 }
              );
            },
          });
      }
    });
  }

  onArchive(element: TermResponse) {
    this.glossaryTermService.archive(element.id.toString()).subscribe({
      next: () => {
        console.log('Term archived:', element.id);
        this.snackBar.open('Term archived successfully', 'Close', {
          duration: 3000,
        });

        this.loadData(); // Reload data after archiving
      },
      error: (err) => {
        console.error('Error archiving term', err);
        this.snackBar.open(err.error.error, 'Close', { duration: 3000 });
      },
    });
  }

  onPublish(element: TermResponse) {
    this.glossaryTermService.publish(element.id.toString()).subscribe({
      next: () => {
        console.log('Term published:', element.id);
        this.snackBar.open('Term published successfully', 'Close', {
          duration: 3000,
        });

        this.loadData();
      },
      error: (err) => {
        this.snackBar.open(err.error.error, 'Close', { duration: 3000 });
      },
    });
  }

  onDelete(element: TermResponse) {
    this.glossaryTermService.delete(element.id.toString()).subscribe({
      next: () => {
        console.log('Term deleted:', element.id);
        this.snackBar.open('Term deleted successfully', 'Close', {
          duration: 3000,
        });
        this.loadData();
      },
      error: (err) => {
        console.error('Error deleting term', err);
        this.snackBar.open(err.error.error, 'Close', { duration: 3000 });
      },
    });
  }

  loadData() {
    const filter: PaginationFilter = {
      pageNumber: this.pageIndex + 1,
      pageSize: this.pageSize,
      termQuery: this.termQuery,
      authorQuery: this.authorQuery,
    };

    this.glossaryTermService.getAuthorTerms(filter).subscribe({
      next: (response: PaginationReturnObject<TermResponse>) => {
        this.dataSource = response.items;
        this.length = response.totalItems;
      },
      error: (err) => {
        console.error('Error loading glossary terms', err);
      },
    });
  }

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadData();
  }

  onFilterChange() {
    this.filterChange$.next();
  }
}
