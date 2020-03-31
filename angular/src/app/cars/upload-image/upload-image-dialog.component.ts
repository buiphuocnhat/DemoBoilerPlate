// import { Component, OnInit, Output, EventEmitter, Injector, AfterViewInit } from '@angular/core';
// import { HttpEventType, HttpClient } from '@angular/common/http';
// import { appModuleAnimation } from '@shared/animations/routerTransition';
// import { MatDialog } from '@angular/material';
// import {
//     CarServiceProxy,
//     CreateCarDto,
//     CompanyServiceProxy,
//     GetCompanyDto
//   } from '@shared/service-proxies/service-proxies';


// @Component({
//   //selector: 'app-upload',
//   templateUrl: './upload-image-dialog.component.html',
//   animations: [appModuleAnimation()]
//   //styleUrls: ['./upload.component.css']
// })
// export class UploadImageDialogComponent implements OnInit {
//   public progress: number;
//   public message: string;
//   @Output() public onUploadFinished = new EventEmitter();

// constructor(
//     private http: HttpClient,
//     injector: Injector,
//      private _dialog: MatDialog
// ) 
// {
//     // super(injector);
// }

//   ngOnInit() {
//   }

//   public uploadFile = (files) => {
//     if (files.length === 0) {
//       return;
//     }

//     let fileToUpload = <File>files[0];
//     const formData = new FormData();
//     formData.append('file', fileToUpload, fileToUpload.name);

//     this.http.post('http://localhost:21021/api/services/app/Car/UploadImage', formData, {reportProgress: true, observe: 'events'})
//       .subscribe(event => {
//         if (event.type === HttpEventType.UploadProgress)
//           {
//             this.progress = Math.round(100 * event.loaded / event.total);
//           }
//         else if (event.type === HttpEventType.Response) {
//           this.message = 'Upload success.';
//           this.onUploadFinished.emit(event.body);
//         }
//       });
//   }
// }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpEventType } from '@angular/common/http';
 
@Component({
  //selector: 'app-image-upload-with-preview',
  templateUrl: './upload-image-dialog.component.html',
  //styleUrls: ['./image-upload-with-preview.component.css']
})
 
export class UploadImageDialogComponent implements OnInit {
 
  fileData: File = null;
  previewUrl:any = null;
  fileUploadProgress: string = null;
  uploadedFilePath: string = null;
  constructor(private http: HttpClient) { }
   
  ngOnInit() {
  }
   
  fileProgress(fileInput: any) {
      this.fileData = <File>fileInput.target.files[0];
      this.preview();
  }
 
  preview() {
    // Show preview 
    var mimeType = this.fileData.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
 
    var reader = new FileReader();      
    reader.readAsDataURL(this.fileData); 
    reader.onload = (_event) => { 
      this.previewUrl = reader.result; 
    }
  }
   
  onSubmit() {
      const formData = new FormData();
      formData.append('file', this.fileData);
      this.http.post('http://localhost:21021/api/services/app/Car/UploadImage', formData)
        .subscribe(res => {
          console.log(res);
          this.uploadedFilePath = res.data.filePath;
          alert('SUCCESS !!');
        }) 
  }
}