import { ErrorHandler, Injectable, Injector} from '@angular/core';
import { Toasts } from './app-toasts';

@Injectable()
export class AppErrorHandler implements ErrorHandler{
    private toasts: Toasts;
    constructor(private injector: Injector) {}
    
    private injectToasts(){
        if(!this.toasts){
            this.toasts = this.injector.get(Toasts);
        }
    }

    handleError(error: any): void {
        let message = "Unexpected error has occured:(";
        
        // Extraction is questinable
        if(error.status === 400)
        {
            var featuresErrors = error.error.Features as string[];
            message = featuresErrors[0];
        }
        
        this.injectToasts();
        this.toasts.displayErrorToast(message);
    }
    
}