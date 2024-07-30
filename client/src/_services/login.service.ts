import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { User } from 'src/app/_models/user';
import { BehaviorSubject, map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'user/register', model).pipe(
      map((user) => {
        if (user) {
          {
            this.setCurrentUser(user);
          }
        }
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'user/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) this.setCurrentUser(user);
      })
    );
  }
}
