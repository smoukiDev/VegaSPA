import { ToastrManager } from 'ng6-toastr-notifications';
import { Injectable } from "@angular/core";

@Injectable()
export class Toasts {
    private readonly options: any = 
    {
        newestOnTop: false,
        showCloseButton: true,
        toastTimeout: 5000,
        position: 'bottom-right',
        animate: 'slideFromTop',
        maxShown: 3,
    };

    constructor(private toastrManager: ToastrManager) {}

    displaySuccessToast(message: string, title: string = "Info") {
        this.toastrManager.successToastr(message, title, this.options);
    }

    displayErrorToast(message: string, title: string = "Error") {
        this.toastrManager.errorToastr(message, title, this.options);
    }
}