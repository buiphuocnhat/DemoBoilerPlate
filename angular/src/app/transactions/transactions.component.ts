import { Component, Injector, AfterViewInit, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core/testing';
import { error } from '@angular/compiler/src/util';
import { finalize } from 'rxjs/operators';
import { MatDialog } from '@angular/material';
import { TransactionDto, GetTransactionDto, TransactionServiceProxy } from '@shared/service-proxies/service-proxies';
import { importExpr } from '@angular/compiler/src/output/output_ast';
import {CreateTransactionDialogComponent} from './create-transaction/create-transaction-dialog.component';
@Component({
    templateUrl: './transactions.component.html',
    animations: [appModuleAnimation()]
})
export class TransactionsComponent extends AppComponentBase  
{
    transactions: GetTransactionDto[] = []

    constructor(
        injector: Injector,
        private _transactionService: TransactionServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
        this._transactionService.getAllTransactions().subscribe((res) =>
        {
            this.transactions=res;
        });
        
    }
    createtran(): void {
        this.showCreateOrEditCarDialog(); 
    }
    // updateCar(car: CarDto): void {
    //     this.showCreateOrEditCarDialog(car.id);
    // }
    showCreateOrEditCarDialog(id?: number): void {
        let createOrEditCarDialog;
        if (id === undefined || id <= 0) {
            createOrEditCarDialog = this._dialog.open(CreateTransactionDialogComponent);}
        
        // } else {
        //     createOrEditCarDialog = this._dialog.open(UpdateCarDialogComponent, {
        //         data: id
        //     });
        // }

        createOrEditCarDialog.afterClosed().subscribe(result => {
            if (result) {
                this._transactionService.getAllTransactions().subscribe((res) => {
                    this.transactions = res;
                });
            }
          });
    }
    delete(tran: GetTransactionDto): void {
        abp.message.confirm(
            this.l('Are you sure'),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._transactionService
                        .deleteTransaction(tran.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this._transactionService.getAllTransactions().subscribe((res) => {
                                    this.transactions = res;
                                });
                            })
                        )
                        .subscribe(() => { });
                }
            }
        );
    }
}