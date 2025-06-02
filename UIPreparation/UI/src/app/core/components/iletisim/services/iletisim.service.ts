import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Iletisim } from '../models/Iletisim';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class IletisimService {

  constructor(private httpClient: HttpClient) { }


  getIletisimList(): Observable<Iletisim[]> {

    return this.httpClient.get<Iletisim[]>(environment.getApiUrl + '/iletisims/getall')
  }

  getIletisimById(id: number): Observable<Iletisim> {
    return this.httpClient.get<Iletisim>(environment.getApiUrl + '/iletisims/getbyid?iletisimId='+id)
  }

  addIletisim(iletisim: Iletisim): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/iletisims/', iletisim, { responseType: 'text' });
  }

  updateIletisim(iletisim: Iletisim): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/iletisims/', iletisim, { responseType: 'text' });

  }

  deleteIletisim(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/iletisims/', { body: { iletisimId: id } });
  }


}