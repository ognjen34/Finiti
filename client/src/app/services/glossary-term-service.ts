import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateTermRequest, PaginationFilter, UpdateTermRequest } from './models/glossary-term-model';
import { PaginationReturnObject, TermResponse } from './models/glossary-term-model'; 

@Injectable({
  providedIn: 'root',
})
export class GlossaryTermService {
  private baseUrl = 'https://localhost:7035/terms'; 

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

    return this.http.get<PaginationReturnObject<TermResponse>>(this.baseUrl, { params,withCredentials: true });
  }
  getAuthorTerms(filter: PaginationFilter): Observable<PaginationReturnObject<TermResponse>> {
    let params = new HttpParams()
      .set('PageNumber', filter.pageNumber.toString())
      .set('PageSize', filter.pageSize.toString());

    if (filter.termQuery) {
      params = params.set('TermQuery', filter.termQuery);
    }
    if (filter.authorQuery) {
      params = params.set('AuthorQuery', filter.authorQuery);
    }

    return this.http.get<PaginationReturnObject<TermResponse>>(this.baseUrl + "/author", { params, withCredentials: true });
  }

  archive(id:string): Observable<any> {
    return this.http.put(this.baseUrl + '/archive/'+id, {}, { observe: 'response', withCredentials: true });
  }
  publish(id:string): Observable<any> {
    return this.http.put(this.baseUrl + '/publish/'+id, {}, { observe: 'response', withCredentials: true });
  }
  delete(id:string): Observable<any> {
    return this.http.delete(this.baseUrl + '/delete/'+id, { observe: 'response', withCredentials: true });
  }
  createTerm(request: CreateTermRequest): Observable<TermResponse> {
    return this.http.post<TermResponse>(this.baseUrl+"/add", request, { withCredentials: true });
  }
  updateTerm(request: UpdateTermRequest): Observable<TermResponse> {
    return this.http.put<TermResponse>(this.baseUrl, request, { withCredentials: true });
  }
}
