import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BlobToB64Service {

  constructor() { }

  getBase64(file: any) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = error => reject(error);
    });
  }
  getFileExtToIframe(ext: string) {
    return `${this.getFileExt(ext)};`
  }
  getFileExt(ext: any) {
    if (ext == 'pdf') {
      return 'application/pdf';
    }
    else if (ext == 'png') {
      return 'image/png';
    }
    else if (ext == 'jpeg') {
      return 'image/jpeg';
    }
    else if (ext == 'jpg') {
      return 'image/jpg';
    }
    else if (ext == 'tiff') {
      return 'image/tiff';
    }
    else if (ext == 'bmp') {
      return 'image/bmp';
    }
    else if (ext == 'xls' || ext == 'xlsx') {
      return 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
    }
    else {
      return 'text/plain';
    }
  }
}
