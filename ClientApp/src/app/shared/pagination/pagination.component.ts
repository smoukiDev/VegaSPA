import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit, OnChanges {
  @Input('total-items') totalItems;
  @Input('page-size') pageSize;
  @Input('current-page') currentPage = 1;
  @Input('pages-between') pagesBetween;
  @Output('page-changed') pageChanged = new EventEmitter();
  pages: any[] = [];

  constructor() { 
  }

  ngOnChanges(): void {
    this.CountPagesAmount();
  }

  ngOnInit() { }

  onFirst() {
    this.currentPage = 1;
    this.pageChanged.emit(this.currentPage);
  }

  onPrevious() {
    if (this.currentPage == 1) {
      return;
    }
    this.currentPage--;
    this.pageChanged.emit(this.currentPage);
  }

  onPage(page) {
    this.currentPage = page;
    this.pageChanged.emit(page);
  }

  onNext() {
    if (this.currentPage == this.pages.length) {
      return;
    }
    this.currentPage++;
    this.pageChanged.emit(this.currentPage);
  }

  onLast() {
    this.currentPage = this.pages.length;
    this.pageChanged.emit(this.currentPage);
  }

  private CountPagesAmount() {
    let pagesCount = Math.ceil(this.totalItems / this.pageSize);
    this.pages = [];
    for (let index = 1; index <= pagesCount; index++) {
      this.pages.push(index);
    }
  }

  private getPagesRange() {
    const c = Math.ceil(this.totalItems / this.pageSize);
    const p = this.currentPage || 1;
    const pagesToShow = this.pagesBetween || 3;
    const pages: number[] = [];
    pages.push(p);
    const times = pagesToShow - 1;
    for (let i = 0; i < times; i++) {
      if (pages.length < pagesToShow) {
        if (Math.min.apply(null, pages) > 1) {
          pages.push(Math.min.apply(null, pages) - 1);
        }
      }
      if (pages.length < pagesToShow) {
        if (Math.max.apply(null, pages) < c) {
          pages.push(Math.max.apply(null, pages) + 1);
        }
      }
    }
    pages.sort((a, b) => a - b);
    return pages;
  }
}
