import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { AnalyzedMessage } from 'src/app/_models/analyzedMessage';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class MessagesService {
  baseUrl = environment.apiUrl;
  message:AnalyzedMessage | undefined;
  constructor(private http: HttpClient) {}

  saveMessage(model: any) {
    return this.http
      .post<AnalyzedMessage>(this.baseUrl + 'Messages/savemessage', model)
      .pipe(
        map((response) => {
          this.message=response;
          return this.message;
        })
      );
  }

}
