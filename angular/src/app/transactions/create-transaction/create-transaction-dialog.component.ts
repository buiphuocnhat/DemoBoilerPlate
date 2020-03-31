import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
TransactionDto,
TransactionServiceProxy,
GetCompanyDto,
GetCarDto,
CarServiceProxy,
CompanyServiceProxy,
CreateTransactionDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-transaction-dialog.component.html',
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
export class CreateTransactionDialogComponent extends AppComponentBase
  implements OnInit {
  public idCompany=1;
  saving = false;
  tran: CreateTransactionDto= new CreateTransactionDto();
  companies: GetCompanyDto[] =[];
  cars: GetCarDto[] = [];
  company: GetCompanyDto= new GetCompanyDto();

  constructor(
    injector: Injector,
    public _companyService: CompanyServiceProxy,
    public _carService: CarServiceProxy,
    public _transactionService: TransactionServiceProxy,
    private _dialogRef: MatDialogRef<CreateTransactionDialogComponent>
    
  ) {
    super(injector);
    this._carService.getAllCars().subscribe((res) =>{
        this.cars=res
    });
  }

  ngOnInit(): void {
  }
  save(): void {
    this.saving = true;

    this._transactionService
      .createTransation(this.tran)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }
}
