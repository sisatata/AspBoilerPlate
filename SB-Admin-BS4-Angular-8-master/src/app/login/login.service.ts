import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginModel } from './login';
@Injectable({
    providedIn: 'root'
  })
  export class LoginService {
  authenticationBaseUri: any = 'https://localhost:44322/Api/User/token';

    
  constructor(private http: HttpClient, private router: Router) {
   
    

  }
  login(loginModel: LoginModel) {
    return this.http.get<any[]>('https://localhost:44322/Api/User/get-roles');

  }

  }