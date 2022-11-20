import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();

  vm: any = {};
  constructor(private accountService: AccountService, private toastrService: ToastrService) { }

  ngOnInit(): void {
  }
  register(){
    this.accountService.register(this.vm).subscribe(response =>{
      console.log(response);
      this.cancel();
    }, error => {
      console.log();
      this.toastrService.error(error.error);
    })
  }
  cancel(){
    this.cancelRegister.emit(false);
  }
}
