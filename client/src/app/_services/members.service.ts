import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

//Jwt interceptor implement for this.
// const httpOption = {
//   headers: new HttpHeaders({
//     Authorization : 'bearer ' + JSON.parse(localStorage.getItem('user'))?.token
//   })
// }


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  member: Member[] = [];
  baseUrl = environment.apiUrl;
  

  constructor(private http: HttpClient) { }

  getMembers(){
    //return this.http.get<Member[]>(this.baseUrl + 'Users', httpOption);//Jwt interceptor implement for this.
    if(this.member.length > 0) return of(this.member)
    return this.http.get<Member[]>(this.baseUrl + 'Users').pipe(
      map(members =>{
        this.member = members;
        return members;
      })
    );
  }
  
  getMember(username: string){
    //return this.http.get<Member[]>(this.baseUrl + 'Users/' + username, httpOption) //Jwt interceptor implement for this.
    const member = this.member.find(x => x.userName === username);
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'Users/' + username)
  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() =>{
        const index = this.member.indexOf(member);
        this.member[index] = member
      })
    );
  }
}
