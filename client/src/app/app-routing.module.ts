import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './chat/login/login.component';
import { RoomComponent } from './chat/room/room.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'room', component: RoomComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
