import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginModel } from './login';
import { IUserLogin } from '../shared/interfaces/interfaces';
@Injectable({
    providedIn: 'root'
  })
  export class LoginService {
  authenticationBaseUri: any = 'https://localhost:44322/Api/User/token';
 requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'True' }) };

    
  constructor(private http: HttpClient, private router: Router) {
  
  }
  login(userLogin: IUserLogin) {
    var requestHeader = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "No-Auth": "True",
      }),
    };
    return this.http.post(
      'https://localhost:44322/Api/User/token',
      userLogin,
      requestHeader
    );
  }

  }