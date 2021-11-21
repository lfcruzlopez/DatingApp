import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Dating App';
  users: any;
  currentUser$: Observable<User> | undefined;

  constructor( private accountService: AccountService){}

  ngOnInit(){
    //this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser()
  { 
   
    const user : User = JSON.parse(localStorage.getItem('user')!);
    this.accountService.setCurrentUser(user);
    console.log('setCurrentUser-app',user);
    
  }
  // }

}
