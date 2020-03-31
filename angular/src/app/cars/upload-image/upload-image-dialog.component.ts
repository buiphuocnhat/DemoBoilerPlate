import { Component, OnInit, Output, EventEmitter, Injector, AfterViewInit } from '@angular/core';
import { HttpEventType, HttpClient } from '@angular/common/http';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { MatDialog } from '@angular/material';
import {
    CarServiceProxy,
    CreateCarDto,
    CompanyServiceProxy,
    GetCompanyDto
  } from '@shared/service-proxies/service-proxies';


@Component({
  //selector: 'app-upload',
  templateUrl: './upload-image-dialog.component.html',
  animations: [appModuleAnimation()]
  //styleUrls: ['./upload.component.css']
})
export class UploadImageDialogComponent implements OnInit {
  public progress: number;
  public message: string;
  @Output() public onUploadFinished = new EventEmitter();

constructor(
    private http: HttpClient,
    injector: Injector,
     private _dialog: MatDialog
) 
{
    // super(injector);
}

  ngOnInit() {
  }

  public uploadFile = (files) => {
    if (files.length === 0) {
      return;
    }

    let fileToUpload = <File>files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);

    this.http.post('http://localhost:21021/api/services/app/Car/UploadImage', formData, {reportProgress: true, observe: 'events'})
      .subscribe(event => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      });
  }
}
