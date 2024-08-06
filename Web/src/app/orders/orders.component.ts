import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { OrdersService, Order, OrderItem } from '../services/orders.service';
import { ProductsService, Product } from '../services/products.service';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

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
    customerName: '',
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
    price: 0,
    product: undefined
  };
  selectedProduct: Product | undefined;
  errorMessages: string[] = [];
  private modalRef: NgbModalRef | undefined;

  constructor(private ordersService: OrdersService, private productsService: ProductsService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadOrders();
  }

  loadOrders(): void {
    this.ordersService.getOrders().subscribe(data => {
      const ordersArray = data || [];
      this.orders = Array.isArray(ordersArray) ? ordersArray : [];
      this.orders.forEach(order => {
        order.orderItems.forEach(item => {
          item.product = this.products.find(p => p.productId === item.productId);
        });
      });
    }, error => {
      this.errorMessages = [error.message];
    });
  }

  loadProducts(): void {
    this.productsService.getProducts().subscribe(data => {
      this.products = Array.isArray(data) ? data : [];
    }, error => {
      this.errorMessages = [error.message];
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
      const existingItem = this.newOrder.orderItems.find(item => item.productId === this.newOrderItem.productId);
      if (existingItem) {
        this.errorMessages = ['Este producto ya está en la orden.'];
        return;
      }
      const orderItem: OrderItem = {
        ...this.newOrderItem,
        orderId: this.newOrder.orderId,
        price: this.selectedProduct.price * this.newOrderItem.quantity,
        product: this.selectedProduct
      };
      this.newOrder.orderItems = [...this.newOrder.orderItems, orderItem];
      this.newOrder.total += orderItem.price;
      this.resetOrderItemForm();
      this.closeModal();
    }
  }

  removeOrderItem(item: OrderItem): void {
    this.newOrder.orderItems = this.newOrder.orderItems.filter(i => i !== item);
    this.newOrder.total -= item.price;
  }

  resetForm(): void {
    this.newOrder = {
      orderId: 0,
      customerName: '',
      orderDate: '',
      status: '',
      total: 0,
      orderItems: []
    };
    this.selectedProduct = undefined;
  }

  resetOrderItemForm(): void {
    this.newOrderItem = {
      orderItemId: 0,
      orderId: 0,
      productId: 0,
      quantity: 0,
      price: 0,
      product: undefined
    };
    this.selectedProduct = undefined;
  }

  onProductChange(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const productId = Number(target.value);
    this.selectedProduct = this.products.find(p => p.productId === productId);
    if (this.selectedProduct) {
      this.newOrderItem.price = this.selectedProduct.price * this.newOrderItem.quantity;
    }
  }

  onQuantityChange(): void {
    if (this.selectedProduct) {
      this.newOrderItem.price = this.selectedProduct.price * this.newOrderItem.quantity;
    }
  }

  openModal(content: any): void {
    this.modalRef = this.modalService.open(content);
  }

  closeModal(): void {
    if (this.modalRef) {
      this.modalRef.close();
    }
  }
}
