import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OdaEkService } from '../models/OdaEkService';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OdaEkServiceService {

  constructor(private httpClient: HttpClient) { }


  getOdaEkServiceList(): Observable<OdaEkService[]> {

    return this.httpClient.get<OdaEkService[]>(environment.getApiUrl + '/odaEkServices/getall')
  }

  getOdaEkServiceById(id: number): Observable<OdaEkService> {
    return this.httpClient.get<OdaEkService>(environment.getApiUrl + '/odaEkServices/getbyid?odaEkServiceId='+id)
  }

  addOdaEkService(odaEkService: OdaEkService): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/odaEkServices/', odaEkService, { responseType: 'text' });
  }

  updateOdaEkService(odaEkService: OdaEkService): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/odaEkServices/', odaEkService, { responseType: 'text' });

  }

  deleteOdaEkService(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/odaEkServices/', { body: { odaEkServiceId: id } });
  }


}