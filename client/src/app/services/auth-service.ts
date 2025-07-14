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
    return this.http.post<LoginResponse>(this.baseUrl + 'login', request);
  }
}
