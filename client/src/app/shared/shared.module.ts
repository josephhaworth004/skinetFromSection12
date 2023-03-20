import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    // forRoot() keeps this as a singleton for the whole project.
    // Angular will not try and instantiate another Pagination Module 
    // Import when I use Lazy-Loading
    // Also need to import THIS MODULE where features of Pagination are used
    // Also imported and exported shared.module.ts in shop.module.ts
    PaginationModule.forRoot(),
    CarouselModule.forRoot() 
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule
  ]
})
export class SharedModule { }
