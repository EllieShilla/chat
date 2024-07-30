import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ChatService } from 'src/_services/chat.service';
import { LoginService } from 'src/_services/login.service';
import { AnalyzedMessage } from 'src/app/_models/analyzedMessage';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  ngOnInit(): void {}
  @ViewChild('loginForm') loginForm: NgForm | undefined;
  model: any = {};
  @Output() cancelRegister = new EventEmitter();
  nextClicked = false;

  private greatingMessage: AnalyzedMessage = {
    userName: 'Admin',
    text: ``,
    timestamp: new Date(),
    sentiment: 'Neutral',
  };

  constructor(
    private loginService: LoginService,
    private router: Router,
    private chatService: ChatService
  ) {}

  login() {
    this.loginService.login(this.model).subscribe({
      next: (result) => {
        this.router.navigateByUrl('/room');
      },
      error: (error) => console.log(error),
    });
  }

  register() {
    this.loginService.register(this.model).subscribe({
      next: (result) => {
        this.cancel();
        this.router.navigateByUrl('/room');
        this.createGreatingMessage();
        this.chatService.sendMessages(this.greatingMessage);
      },
      error: (error) => console.log(error),
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  public onSubmit(): void {
    if (this.nextClicked) {
      this.register();
    } else {
      this.login();
    }
  }

  public onRegistrationClick(): void {
    this.nextClicked = true;
  }

  public onLoginClick(): void {
    this.nextClicked = false;
  }

  createGreatingMessage() {
    this.greatingMessage.text = `${this.model.UserName} has entered the chat`;
  }
}
