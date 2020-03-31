import { Component, Injector, AfterViewInit, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core/testing';
import { error } from '@angular/compiler/src/util';
import { finalize } from 'rxjs/operators';
import { MatDialog } from '@angular/material';
import { 
CompanyDto, CompanyServiceProxy
, GetCompanyDto,
PagedCompanyResultRrequestDto} from '@shared/service-proxies/service-proxies';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from 'shared/paged-listing-component-base';
import { importExpr } from '@angular/compiler/src/output/output_ast';
import {ListCarsComponent} from './list-cars/list-cars-dialog.component';
import {UpdateCompanyDialogComponent} from './update-company/update-company-dialog.component';
import {CreateCompanyDialogComponent} from './create-company/create-company-dialog.component';
@Component({
    templateUrl: './companies.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
      ]
})
export class CompaniesComponent extends PagedListingComponentBase<CompanyDto>  
{
    keyword = '';
    companies: CompanyDto[] = []
    constructor(
        injector: Injector,
        public _companyService: CompanyServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }
    list(
        request: PagedCompanyResultRrequestDto= new PagedCompanyResultRrequestDto(),
        pageNumber: number,
        finishedCallback: Function
    ): void {

        request.keyword = this.keyword;

        this._companyService
        .createFilteredQuery(request)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result) => {
                this.companies = result.items;
                this.showPaging(result, pageNumber);
            });
    }
    
    listCars(com: GetCompanyDto): void {
        this.showListCarsDialog(com.id);
    }

    showListCarsDialog(id?: number): void {
        if (id != undefined || id > 0) {
            this._dialog.open(ListCarsComponent,{
                data: id
            });
        } 
       
    }
    createCar(): void {
        this.showCreateOrEditCompanyDialog(); 
    }
    updateCar(company: CompanyDto): void {
        this.showCreateOrEditCompanyDialog(company.id);
    }
    showCreateOrEditCompanyDialog(id?: number): void {
        let createOrEditCompanyDialog;
        if (id === undefined || id <= 0) {
            createOrEditCompanyDialog = this._dialog.open(CreateCompanyDialogComponent);
        
        } else {
            createOrEditCompanyDialog = this._dialog.open(UpdateCompanyDialogComponent, {
                data: id
            });
        }

        createOrEditCompanyDialog.afterClosed().subscribe(result => {
            // if (result) {
            //     this._companyService.getAllCompanies().subscribe((res) => {
            //         this.companies = res;
            //     });
            // }
            if (result) {
                this.refresh();
            }
          });
    }
    delete(company: CompanyDto): void {
        abp.message.confirm(
            this.l('Are you sure', company.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._companyService
                        .deleteCompany(company.id)
                        .pipe(
                            // finalize(() => {
                            //     abp.notify.success(this.l('SuccessfullyDeleted'));
                            //     this._companyService.getAllCompanies().subscribe((res) => {
                            //         this.companies = res;
                            //     });
                            // })
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this.refresh();
                            })
                        )
                        .subscribe(() => { });
                }
            }
        );
    }
}
