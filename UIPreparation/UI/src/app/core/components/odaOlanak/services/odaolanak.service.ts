import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OdaOlanak } from '../models/OdaOlanak';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OdaOlanakService {

  constructor(private httpClient: HttpClient) { }


  getOdaOlanakList(): Observable<OdaOlanak[]> {

    return this.httpClient.get<OdaOlanak[]>(environment.getApiUrl + '/odaOlanaks/getall')
  }

  getOdaOlanakById(id: number): Observable<OdaOlanak> {
    return this.httpClient.get<OdaOlanak>(environment.getApiUrl + '/odaOlanaks/getbyid?odaOlanakId='+id)
  }

  addOdaOlanak(odaOlanak: OdaOlanak): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/odaOlanaks/', odaOlanak, { responseType: 'text' });
  }

  updateOdaOlanak(odaOlanak: OdaOlanak): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/odaOlanaks/', odaOlanak, { responseType: 'text' });

  }

  deleteOdaOlanak(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/odaOlanaks/', { body: { odaOlanakId: id } });
  }


}