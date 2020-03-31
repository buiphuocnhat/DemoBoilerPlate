import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CarServiceProxy,
  CreateCarDto,
  CompanyServiceProxy,
  GetCompanyDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-car-dialog.component.html',
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
export class CreateCarDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  car: CreateCarDto= new CreateCarDto();
  companies: GetCompanyDto[]=[];

  constructor(
    injector: Injector,
    public _companyService: CompanyServiceProxy,
    public _carService: CarServiceProxy,
    private _dialogRef: MatDialogRef<CreateCarDialogComponent>
    
  ) {
    super(injector);
    this._companyService.getAllCompanies().subscribe((res) => {
      this.companies = res;
  });
  }

  ngOnInit(): void {
  }
  save(): void {
    this.saving = true;

    this._carService
      .createCar(this.car)
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
