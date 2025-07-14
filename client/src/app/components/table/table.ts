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
import { TermResponse, PaginationReturnObject, PaginationFilter } from '../../services/models/glossary-term-model';


@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatPaginatorModule, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  templateUrl: './table.html',
  styleUrls: ['./table.css'],
})
export class Table implements OnInit {
  displayedColumns: string[] = ['term', 'definition', 'author'];
  dataSource: TermResponse[] = [];

  pageSize = 10;
  pageIndex = 0;
  length = 0;

  termQuery: string = '';
  authorQuery: string = '';

  private filterChange$ = new Subject<void>();

  constructor(private glossaryTermService: GlossaryTermService) {}

  ngOnInit() {
    this.filterChange$.pipe(
      debounceTime(1000)
    ).subscribe(() => {
      this.pageIndex = 0;
      this.loadData();
    });

    this.loadData();
  }
  onAddTerm() {
    console.log('Add term button clicked');
  }

  loadData() {
    const filter: PaginationFilter = {
      pageNumber: this.pageIndex + 1,  
      pageSize: this.pageSize,
      termQuery: this.termQuery,
      authorQuery: this.authorQuery,
    };

    this.glossaryTermService.getTerms(filter).subscribe({
      next: (response: PaginationReturnObject<TermResponse>) => {
        this.dataSource = response.items;
        this.length = response.totalItems;
      },
      error: (err) => {
        console.error('Error loading glossary terms', err);
      }
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
