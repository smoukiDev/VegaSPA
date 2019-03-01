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
}
