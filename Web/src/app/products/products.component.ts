import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ProductsService, Product } from '../services/products.service';

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
  errorMessage: string = '';

  constructor(private productsService: ProductsService) { }

  ngOnInit(): void {
    this.productsService.getProducts().subscribe(data => {
      this.products = data;
    });
  }

  createProduct(): void {
    this.productsService.createProduct(this.newProduct).subscribe(
      product => {
        this.products.push(product);
        this.resetForm();
        this.errorMessage = ''; // Clear any previous error message
      },
      error => {
        this.errorMessage = error;
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
