import { Component, Injector, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CarServiceProxy,
  GetTransactionDto,
  TransactionServiceProxy
}from '@shared/service-proxies/service-proxies';
import { Subscriber } from 'rxjs';

@Component({
  templateUrl: './list-transactions-dialog.component.html',
  styles: [
    `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `
  ]
})
export class ListTransactionsComponent extends AppComponentBase
  implements OnInit {
  transactions: GetTransactionDto[] = [];    
  
  constructor(
    injector: Injector,
    public _carService: CarServiceProxy,
    public _transactionService: TransactionServiceProxy,
    private _dialogRef: MatDialogRef<ListTransactionsComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
    
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._transactionService.getTransactionsByCar(this._id).subscribe((res) => {
      this.transactions=res
    });
  }
}
