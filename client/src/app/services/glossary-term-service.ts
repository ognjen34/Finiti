import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginationFilter } from '../services/glossary-term-model';
import { PaginationReturnObject, TermResponse } from '../services/glossary-term-model'; 

@Injectable({
  providedIn: 'root',
})
export class GlossaryTermService {
  private baseUrl = 'https://localhost:7035/terms'; // adjust your API base path

  constructor(private http: HttpClient) {}

  getTerms(filter: PaginationFilter): Observable<PaginationReturnObject<TermResponse>> {
    let params = new HttpParams()
      .set('PageNumber', filter.pageNumber.toString())
      .set('PageSize', filter.pageSize.toString());

    if (filter.termQuery) {
      params = params.set('TermQuery', filter.termQuery);
    }
    if (filter.authorQuery) {
      params = params.set('AuthorQuery', filter.authorQuery);
    }

    return this.http.get<PaginationReturnObject<TermResponse>>(this.baseUrl, { params });
  }
}
