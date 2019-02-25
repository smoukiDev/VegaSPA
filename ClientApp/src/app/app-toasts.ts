import { ToastrManager } from 'ng6-toastr-notifications';
import { Injectable, NgZone } from "@angular/core";

@Injectable()
export class Toasts {
    private readonly options: any = 
    {
        newestOnTop: false,
        showCloseButton: true,
        toastTimeout: 5000,
        position: 'top-right',
        animate: 'slideFromTop',
        maxShown: 3,
    };

    constructor(
        private toastrManager: ToastrManager,
        private ngZone: NgZone) {}

    displaySuccessToast(message: string, title: string = "Info") {
        this.ngZone.run(() => {
            this.toastrManager.successToastr(message, title, this.options);
        });
    }

    displayErrorToast(message: string, title: string = "Error") {
        this.ngZone.run(() => {
            this.toastrManager.errorToastr(message, title, this.options);
        });
    }
}