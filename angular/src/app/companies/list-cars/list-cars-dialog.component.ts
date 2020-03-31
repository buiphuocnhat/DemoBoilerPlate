import { Component, Injector, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CarServiceProxy,
  GetCarDto,
  CompanyServiceProxy
}from '@shared/service-proxies/service-proxies';
import { Subscriber } from 'rxjs';

@Component({
  templateUrl: './list-cars-dialog.component.html',
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
export class ListCarsComponent extends AppComponentBase
  implements OnInit {
  cars: GetCarDto[] = [];    
  
  constructor(
    injector: Injector,
    public _carService: CarServiceProxy,
    public _companyService: CompanyServiceProxy,
    private _dialogRef: MatDialogRef<ListCarsComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
    
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._carService.getCarsByCompany(this._id).subscribe((res) => {
      this.cars=res
    });
  }
}
