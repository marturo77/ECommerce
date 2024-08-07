import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ProductsService, Product } from '../services/products.service';
import { NotificationService } from '../services/notificationService';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  newProduct: Product = {
    productId: 0,
    name: '',
    description: '',
    price: 0,
    category: '',
    stock: 0
  };
  errorMessages: string[] = [];

  constructor(private productsService: ProductsService, private notificationService:NotificationService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productsService.getProducts().subscribe(data => {
      this.products = data;
    });
  }

  createProduct(): void {
    this.productsService.createProduct(this.newProduct).subscribe(
      product => {
        this.products.push(product);
        this.resetForm();
        this.loadProducts();
        this.errorMessages = [];
      },
      error => {
        this.errorMessages = error.split('\n');
      }
    );
  }

  deleteProduct(id: number): void {
    this.productsService.deleteProduct(id).subscribe(
      () => {
        this.products = this.products.filter(product => product.productId !== id);
      },
      error => {
        this.errorMessages = error.split('\n');
      }
    );
  }

  resetForm(): void {
    this.newProduct = {
      productId: 0,
      name: '',
      description: '',
      price: 0,
      category: '',
      stock: 0
    };
  }
}
