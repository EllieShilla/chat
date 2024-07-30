import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, map } from 'rxjs';
import { Message } from 'src/app/_models/message';
import { ForConnection } from 'src/app/_models/forConnection';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  hubUrl = environment.hubUrl;
  private hubConnection?: HubConnection;
  chatMessageList: Array<Message> = [];

  private messageThreadSource = new BehaviorSubject<
    Array<{
      userName: string;
      text: string;
      timestamp: Date;
      sentiment: string;
    }>
  >([]);

  messageThread$ = this.messageThreadSource.asObservable();

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'chat')
      .withAutomaticReconnect()
      .build();

    if (this.hubConnection)
      this.hubConnection.start().catch((error) => console.log(error));

    this.getMessage();
  }

  sendMessages(chatMessage: Message) {
    if (this.hubConnection) {
      this.hubConnection
        .invoke('SendMessage', chatMessage)
        .catch(function (err) {
          return console.error(err.toString());
        });
    }
  }

  getMessage() {
    if (this.hubConnection) {
      this.hubConnection.on('ReceiveMessage', (message: any) => {
        const currentMessages = this.messageThreadSource.value;

        const updatedMessages = [
          ...currentMessages,
          {
            userName: message.userName,
            text: message.text,
            timestamp: new Date(message.timestamp),
            sentiment: message.sentiment,
          },
        ];
        this.messageThreadSource.next(updatedMessages);
      });
    }
  }

  stopHubConnection() {
    if (this.hubConnection)
    this.hubConnection.stop();
  }
}
