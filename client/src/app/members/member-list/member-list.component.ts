import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  //member : Member[]
  member$ : Observable<Member[]>;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    //this.getMembers();
    this.member$ = this.memberService.getMembers();
  }
  // getMembers(){
  //   this.memberService.getMembers().subscribe(m => {
  //     this.member = m;
  //   });
  // }
  // getMember(username : string){
  //   //this.memberService.getMember(username);
  //   console.log(this.memberService.getMember(username));
  // }
}
