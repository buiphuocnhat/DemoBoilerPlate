import { Component, Injector, AfterViewInit, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core/testing';
import { error } from '@angular/compiler/src/util';
import { finalize } from 'rxjs/operators';
import { MatDialog } from '@angular/material';
import { CarServiceProxy, GetCarDto, CarDto, PagedCarResultRequestDto} from '@shared/service-proxies/service-proxies';
import { CreateCarDialogComponent } from './create-car/create-car-dialog.component';
import { UpdateCarDialogComponent } from './update-car/update-car-dialog.component';
import { ListTransactionsComponent } from './list-transactions/list-transactions-dialog.component';
import { UploadImageDialogComponent } from './upload-image/upload-image-dialog.component';
import { importExpr } from '@angular/compiler/src/output/output_ast';
import {
    PagedListingComponentBase,
    PagedRequestDto
} from 'shared/paged-listing-component-base';

@Component({
    templateUrl: './cars.component.html',
    animations: [appModuleAnimation()]
})
export class CarsComponent extends PagedListingComponentBase<CarDto>  
implements OnInit{
    public keyword: string;
    cars: GetCarDto[] = [];
    constructor(
        injector: Injector,
         private _carServiceProxy: CarServiceProxy,
         private _dialog: MatDialog
    ) {
        super(injector);
    }
    list(
        request: PagedCarResultRequestDto= new PagedCarResultRequestDto(),
        pageNumber: number,
        finishedCallback: Function
    ): void {

        request.keyword = this.keyword;

        this._carServiceProxy
        .createFilteredQuery(request)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result) => {
                this.cars = result.items;
                this.showPaging(result, pageNumber);
            });
    }
    listTrans(car: GetCarDto): void {
        this.showListTransactionsDialog(car.id);
    }

    showListTransactionsDialog(id?: number): void {
        if (id != undefined || id > 0) {
            this._dialog.open(ListTransactionsComponent,{
                data: id
            });
        } 
       
    }

    showUploadImageDialog(): void{
        this._dialog.open(UploadImageDialogComponent)
    }
    createCar(): void {
        this.showCreateOrEditCarDialog(); 
    }
    updateCar(car: CarDto): void {
        this.showCreateOrEditCarDialog(car.id);
    }
    showCreateOrEditCarDialog(id?: number): void {
        let createOrEditCarDialog;
        if (id === undefined || id <= 0) {
            createOrEditCarDialog = this._dialog.open(CreateCarDialogComponent);
        
        } else {
            createOrEditCarDialog = this._dialog.open(UpdateCarDialogComponent, {
                data: id
            });
        }

        createOrEditCarDialog.afterClosed().subscribe(result => {
            if(result){
                this.refresh();
            }
          });
    }
    delete(car: CarDto): void {
        abp.message.confirm(
            this.l('Are you sure', car.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._carServiceProxy
                        .deleteCar(car.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this._carServiceProxy.getAllCars().subscribe((res) => {
                                   this.refresh();
                                });
                            })
                        )
                        .subscribe(() => { });
                }
            }
        );
    }
}