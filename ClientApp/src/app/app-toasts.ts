import { ToastrManager } from 'ng6-toastr-notifications';
import { Injectable } from "@angular/core";

@Injectable()
export class Toasts {
    constructor(private toastrManager: ToastrManager) {}

    displaySuccessToast(message: string, title: string = "Info"){
        this.toastrManager.successToastr(message, title, {
            showCloseButton: true,          
            toastTimeout: 5000,
            position: 'bottom-right',
            animate: 'slideFromBottom'});
    }

    displayErrorToast(message: string, title: string = "Error"){
        this.toastrManager.errorToastr(message, title, {
            showCloseButton: true,          
            toastTimeout: 5000,
            position: 'bottom-right',
            animate: 'slideFromBottom'});
    }
}