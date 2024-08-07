import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private hubConnection: signalR.HubConnection;

  private apiUrl = `${environment.apiUrl}/notificationHub`; 

  constructor(private toastr: ToastrService) {
    console.log(`usando signalr desde ${this.apiUrl}`);
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.apiUrl) // Asegúrate de usar la URL correcta
      .build();

    this.hubConnection.start().catch(err => console.error(err.toString()));

    this.hubConnection.on('ReceiveNotification', (message: string) => {
      this.toastr.success(message);
    });
  }
}
