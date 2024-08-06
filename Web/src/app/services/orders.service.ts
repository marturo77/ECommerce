import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

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
  private apiUrl = 'https://localhost:7151/api/orders';

  constructor(private http: HttpClient) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<{ items: Order[] }>(this.apiUrl).pipe(
      map(response => response.items),  
      catchError(this.handleError)
    );
  }

  createOrder(order: Order): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, order).pipe(
      catchError(this.handleError)
    );
  }

  deleteOrder(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any): Observable<never> {
    console.error('An error occurred', error);
    return throwError(error);
  }
}
