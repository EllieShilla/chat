import { Text } from '@angular/compiler';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ChatService } from 'src/_services/chat.service';
import { LoginService } from 'src/_services/login.service';
import { MessagesService } from 'src/_services/messages.service';
import { AnalyzedMessage } from 'src/app/_models/analyzedMessage';

@Component({
  selector: 'app-room',
  templateUrl: './room.component.html',
  styleUrls: ['./room.component.css'],
})
export class RoomComponent implements OnInit {
  @ViewChild('loginForm') loginForm: NgForm | undefined;
  model: any = {};
  username: any;
  message: AnalyzedMessage | undefined;

  constructor(
    public chatService: ChatService,
    private loginService: LoginService,
    private messagesService: MessagesService
  ) {}

  ngOnInit() {
    this.loginService.currentUser$.subscribe({
      next: (data) => {
        this.username = data?.userName;
      },
      error: (error) => {
        console.error('Error fetching data', error);
      },
    });
  }

  sendMessage() {
    this.model.userName = this.username;
    this.model.timestamp = new Date();
    this.messagesService.saveMessage(this.model).subscribe({
      next: (data) => {
        this.message = data;
        this.chatService.sendMessages(this.message);
      },
      error: (error) => {
        console.error('Error fetching data', error);
      },
    });
  }

  setColor(result: string) {
    return result === 'Negative'
      ? 'red'
      : result === 'Positive'
      ? 'green'
      : result === 'Neutral'
      ? 'grey'
      : '#579ffb';
  }

  ngOnDestroy(): void {
    this.chatService.stopHubConnection();
  }
}
