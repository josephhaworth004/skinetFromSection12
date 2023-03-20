import { HttpClient, HttpParams } from '@angular/common/http';
//import { Type } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { Pagination } from '../shared/models/Pagination';
import { Product } from '../shared/models/Product';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = "https://localhost:5001/api/"

  // Centralize HTTP requests here.
  constructor(private http: HttpClient) { }

  // shopParams is a type of ShopParams.
  // ShopParams is and exported class not an interface can use it as a type or create a new instance
  // Creating a new instance is what I'm doing below
  getProducts(shopParams: ShopParams) { 
    let params = new HttpParams();

    // Both brandId and typeId are initialized to zero in shopParams.ts
    // We give them real values here.
    if(shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId);
    if(shopParams.typeId) params = params.append('typeId', shopParams.typeId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    if(shopParams.search) params = params.append('search', shopParams.search);
    
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {params});
  }

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  getBrands() {
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }
  
  getTypes() {
    return this.http.get<Type[]>(this.baseUrl + 'products/types');
  }
}
