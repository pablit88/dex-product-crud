import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})

export class ProductService {

  private apiUrl = 'https://localhost:7286/api/Product';

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  addProduct(product: Product): Observable<number> {
    return this.http.post<number>(this.apiUrl, product);
  }

  updateProduct(id: number, product: Product): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  addCategoryToProduct(productId: number, categoryId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${productId}/categories/${categoryId}`, null);
  }

  removeCategoryFromProduct(productId: number, categoryId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${productId}/categories/${categoryId}`);
  }
}
