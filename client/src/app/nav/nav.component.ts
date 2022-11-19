import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  vm: any = {};
  //Creating accountService public to access in template file
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

  login(){
    this.accountService.login(this.vm).subscribe(response =>{
      console.log(response);
    },error=>{
      console.log(error);
    })
  }

  logout(){
    this.accountService.logout();
  }
}
