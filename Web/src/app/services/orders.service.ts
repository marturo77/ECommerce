import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ErrorHandlerService } from './errorHandlerService';
import { environment } from '../../environments/environment';

export interface OrderItem {
  orderItemId: number;
  orderId: number;
  productId: number;
  quantity: number;
  price: number;
  product?: Product;
}

export interface Product {
  productId: number;
  name: string;
  description: string;
  price: number;
  category: string;
  stock: number;
}

export interface Order {
  orderId: number;
  customerName: string;
  orderDate: string;
  status: string;
  total: number;
  orderItems: OrderItem[];
}

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  private apiUrl = `${environment.apiUrl}/orders`; 

  constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<{ items: Order[] }>(this.apiUrl).pipe(
      map(response => response.items),
      catchError(this.errorHandler.handleError)
    );
  }

  createOrder(order: Order): Observable<Order> {
    return this.http.post<{ order: Order }>(this.apiUrl, order).pipe(
      map(response => response.order), // Mapear la respuesta para extraer la orden
      catchError(this.errorHandler.handleError)
    );
  }

  deleteOrder(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.errorHandler.handleError)
    );
  }
}
