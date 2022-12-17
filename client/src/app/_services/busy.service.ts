import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  busyReQuestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) {}
  busy() {
    this.spinnerService.show(undefined, {
      type: 'line-scale-party',
      bdColor: 'rgba(255, 255, 255, 0)',
      color: '#333333',
    });
  }
  idle() {
    this.busyReQuestCount--;
    if (this.busyReQuestCount <= 0) {
      this.busyReQuestCount = 0;
      this.spinnerService.hide();
    }
  }
}
