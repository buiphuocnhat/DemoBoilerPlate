import { Component, Injector, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CarServiceProxy,
  CreateCarDto,
  CarDto,
  CompanyDto,
  CompanyServiceProxy
} from '@shared/service-proxies/service-proxies';
import { Subscriber } from 'rxjs';
@Component({
  templateUrl: 'update-car-dialog.component.html',
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
export class UpdateCarDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  companies: CompanyDto[] = [];
  car: CarDto=  new CarDto();

  constructor(
    injector: Injector,
    public _carService: CarServiceProxy,
    public _companyService: CompanyServiceProxy,
    private _dialogRef: MatDialogRef<UpdateCarDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
    
  ) {
    super(injector);
    this._companyService.getAllCompanies().subscribe((res) => {
      this.companies = res;
  });
  }

  ngOnInit(): void {
    this._carService.getCar(this._id).subscribe((res) => {
      this.car.id = res.id;
      this.car.name=res.name;
      this.car.description=res.description;
      this.car.inventory=res.inventory;
      this.car.price=res.price;
      this.car.total=res.total;
    });
  }
  save(): void {
    this.saving = true;

    this._carService
      .updateCar(this.car)
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
