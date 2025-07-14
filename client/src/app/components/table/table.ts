import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { GlossaryTermService } from '../../services/glossary-term-service';
import { TermResponse, PaginationReturnObject, PaginationFilter } from '../../services/glossary-term-model';

@Component({
  selector: 'app-table',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatPaginatorModule],
  templateUrl: './table.html',
  styleUrls: ['./table.css'],
})
export class Table implements OnInit {
  displayedColumns: string[] = ['id', 'term', 'definition', 'author'];
  dataSource: TermResponse[] = [];

  pageSize = 5;
  pageIndex = 0;
  length = 0;

  constructor(private glossaryTermService: GlossaryTermService) {}

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    const filter: PaginationFilter = {
      pageNumber: this.pageIndex + 1,  // API pages are usually 1-based
      pageSize: this.pageSize,
      termQuery: '',
      authorQuery: ''
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
}
