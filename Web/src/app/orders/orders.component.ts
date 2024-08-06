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
    customer: '',
    orderDate: '',
    status: '',
    total: 0,
    orderItems: []
  };
  newOrderItem: OrderItem = {
    orderItemId: 0,
    orderId: 0,
    productId: 0,
    quantity: 0,
    price: 0
  };
  selectedProduct: Product | null = null;
  errorMessages: string[] = [];

  constructor(private ordersService: OrdersService, private productsService: ProductsService) { }

  ngOnInit(): void {
    this.loadOrders();
    this.loadProducts();
  }

  loadOrders(): void {
    this.ordersService.getOrders().subscribe(data => {
      this.orders = data;
      this.orders.forEach(order => {
        order.orderItems.forEach(item => {
          item.product = this.products.find(p => p.productId === item.productId); // Asegúrate de que esto devuelve undefined si no se encuentra
        });
      });
    });
  }

  loadProducts(): void {
    this.productsService.getProducts().subscribe(data => {
      this.products = data;
    });
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

  deleteOrder(id: number): void {
    this.ordersService.deleteOrder(id).subscribe(
      () => {
        this.orders = this.orders.filter(order => order.orderId !== id);
      },
      error => {
        this.errorMessages = error.split('\n');
      }
    );
  }

  addOrderItem(): void {
    if (this.selectedProduct) {
      const orderItem: OrderItem = {
        ...this.newOrderItem,
        orderId: this.newOrder.orderId,
        price: this.selectedProduct.price // Utilizar el precio del producto seleccionado
      };
      this.newOrder.orderItems.push(orderItem);
      this.newOrder.total += orderItem.price * orderItem.quantity;
      this.resetOrderItemForm();
    }
  }

  resetForm(): void {
    this.newOrder = {
      orderId: 0,
      customer: '',
      orderDate: '',
      status: '',
      total: 0,
      orderItems: []
    };
    this.selectedProduct = null;
  }

  resetOrderItemForm(): void {
    this.newOrderItem = {
      orderItemId: 0,
      orderId: 0,
      productId: 0,
      quantity: 0,
      price: 0
    };
    this.selectedProduct = null;
  }

  onProductChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const productId = Number(target.value);
    this.selectedProduct = this.products.find(p => p.productId === productId) || null;
    if (this.selectedProduct) {
      this.newOrderItem.price = this.selectedProduct.price;
    }
  }

  onQuantityChange(): void {
    if (this.selectedProduct) {
      this.newOrderItem.price = this.selectedProduct.price * this.newOrderItem.quantity;
    }
  }
}
