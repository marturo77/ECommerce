import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { OrdersService, Order, OrderItem } from '../services/orders.service';
import { ProductsService, Product } from '../services/products.service';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {
  orders: Order[] = [];
  products: Product[] = [];
  newOrder: Order = {
    orderId: 0,
    customerId: 0,
    orderDate: new Date().toISOString().substring(0, 10),
    status: 'Pending',
    total: 0,
    orderItems: []
  };
  newOrderItem: OrderItem = {
    orderItemId: 0,
    orderId: 0,
    productId: 0,
    quantity: 1,
    price: 0
  };
  errorMessages: string[] = [];

  constructor(private ordersService: OrdersService, private productsService: ProductsService) { }

  ngOnInit(): void {
    this.ordersService.getOrders().subscribe(data => {
      this.orders = data;
    });
    this.productsService.getProducts().subscribe(data => {
      this.products = data;
    });
  }

  addOrderItem(): void {
    const product = this.products.find(p => p.productId === this.newOrderItem.productId);
    if (product) {
      this.newOrderItem.price = product.price;
      this.newOrder.orderItems.push({ ...this.newOrderItem });
      this.newOrder.total += this.newOrderItem.price * this.newOrderItem.quantity;
      this.newOrderItem = {
        orderItemId: 0,
        orderId: 0,
        productId: 0,
        quantity: 1,
        price: 0
      };
    }
  }

  createOrder(): void {
    this.ordersService.createOrder(this.newOrder).subscribe(
      order => {
        this.orders.push(order);
        this.resetForm();
        this.errorMessages = [];
      },
      error => {
        this.errorMessages = error.split('\n');
      }
    );
  }

  resetForm(): void {
    this.newOrder = {
      orderId: 0,
      customerId: 0,
      orderDate: new Date().toISOString().substring(0, 10),
      status: 'Pending',
      total: 0,
      orderItems: []
    };
    this.newOrderItem = {
      orderItemId: 0,
      orderId: 0,
      productId: 0,
      quantity: 1,
      price: 0
    };
  }
}
