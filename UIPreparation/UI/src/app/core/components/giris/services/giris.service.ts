import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Giris } from '../models/Giris';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GirisService {

  constructor(private httpClient: HttpClient) { }


  getGirisList(): Observable<Giris[]> {

    return this.httpClient.get<Giris[]>(environment.getApiUrl + '/girises/getall')
  }

  getGirisById(id: number): Observable<Giris> {
    return this.httpClient.get<Giris>(environment.getApiUrl + '/girises/getbyid?girisId='+id)
  }

  addGiris(giris: Giris): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/girises/', giris, { responseType: 'text' });
  }

  updateGiris(giris: Giris): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/girises/', giris, { responseType: 'text' });

  }

  deleteGiris(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/girises/', { body: { girisId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/girises/addPhoto', formData, { responseType: 'text' });
  }


}