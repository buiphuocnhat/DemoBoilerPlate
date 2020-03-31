import { Component, Injector, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CompanyDto,
  CompanyServiceProxy
} from '@shared/service-proxies/service-proxies';
import { Subscriber } from 'rxjs';
@Component({
  templateUrl: 'update-company-dialog.component.html',
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
export class UpdateCompanyDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  company: CompanyDto = new CompanyDto();

  constructor(
    injector: Injector,
    public _companyService: CompanyServiceProxy,
    private _dialogRef: MatDialogRef<UpdateCompanyDialogComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number
    
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._companyService.getCompany(this._id).subscribe((res) => {
      this.company=res
    });
  }
  save(): void {
    this.saving = true;

    this._companyService
      .updateCompany(this.company)
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
