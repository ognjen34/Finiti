import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface AuthorLoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7035/authors/';

  constructor(private http: HttpClient) {}

  login(request: AuthorLoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.baseUrl + 'login', request,{ withCredentials: true });
  }
  logout(): Observable<any> {
    return this.http.post(this.baseUrl + 'logout', {}, { observe: 'response', withCredentials: true });
  }

  authenticate(): Observable<any> {
    return this.http.get(this.baseUrl + 'authenticate', { observe: 'response',withCredentials: true });
  }
}
