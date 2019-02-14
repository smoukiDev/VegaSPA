import { ToastrManager } from 'ng6-toastr-notifications';
import { ErrorHandler, Injector, Injectable, OnInit } from '@angular/core';

@Injectable()
export class AppErrorHandler implements ErrorHandler{
    private toastrManager: ToastrManager;
    
    constructor(private injector: Injector) {}
    
    handleError(error: any): void {
        this.toastrManager = this.injector.get(ToastrManager);

        this.toastrManager.errorToastr("Unexpected error", "Error", {
            showCloseButton: true,          
            toastTimeout: 5000,
            position: 'bottom-right',
            animate: 'slideFromBottom'});
    }
    
}