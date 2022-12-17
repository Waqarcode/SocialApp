import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
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

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMembers(){
    //return this.http.get<Member[]>(this.baseUrl + 'Users', httpOption);//Jwt interceptor implement for this.
    return this.http.get<Member[]>(this.baseUrl + 'Users');
  }
  
  getMember(username: string){
    //return this.http.get<Member[]>(this.baseUrl + 'Users/' + username, httpOption) //Jwt interceptor implement for this.
    return this.http.get<Member>(this.baseUrl + 'Users/' + username)
  }
}
