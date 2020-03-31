import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CompanyServiceProxy,
  CreateCompanyDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-company-dialog.component.html',
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
export class CreateCompanyDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  company: CreateCompanyDto= new CreateCompanyDto();
  

  constructor(
    injector: Injector,
    public _companyService: CompanyServiceProxy,
    private _dialogRef: MatDialogRef<CreateCompanyDialogComponent>
    
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }
  save(): void {
    this.saving = true;

    this._companyService
      .createCompany(this.company)
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
