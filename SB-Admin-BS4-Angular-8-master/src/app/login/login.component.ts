import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { routerTransition } from '../router.animations';
import { LoginModel } from './login';
import { LoginService } from './login.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    errorMessage: string;
    login: LoginModel ;
    constructor(private formBuilder: FormBuilder,
        private router: Router,
        private loginService: LoginService
        ) {}
    ngOnInit() {
        this.buildForm();
    }
    buildForm(): void {
        this.loginForm = this.formBuilder.group({
            email:      ['', [ Validators.required ]],
            password:   ['', [ Validators.required ]]
        });
    }
    onLoggedin(value:LoginModel) {
        //localStorage.setItem('isLoggedin', 'true');
       console.log(value);
       this.loginService.login(value).subscribe(res=>{
                  console.log(res); 
       },()=>{})

    }
}
