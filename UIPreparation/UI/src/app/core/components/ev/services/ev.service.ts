import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Ev } from '../models/Ev';
import { environment } from 'environments/environment';
import { EvDetail } from '../../evDetail/models/EvDetail';
import { OdaEkService } from '../../odaEkService/models/OdaEkService';
import { OdaOlanak } from '../../odaOlanak/models/OdaOlanak';
import { Galary } from '../../galary/models/Galary';


@Injectable({
  providedIn: 'root'
})
export class EvService {

  constructor(private httpClient: HttpClient) { }


  getEvList(): Observable<Ev[]> {

    return this.httpClient.get<Ev[]>(environment.getApiUrl + '/evs/getall')
  }

  getEvById(id: number): Observable<Ev> {
    return this.httpClient.get<Ev>(environment.getApiUrl + '/evs/getbyid?evId='+id)
  }

  addEv(ev: Ev): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/evs/', ev, { responseType: 'text' });
  }

  updateEv(ev: Ev): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/evs/', ev, { responseType: 'text' });

  }

  deleteEv(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/evs/', { body: { evId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/evs/addPhoto', formData, { responseType: 'text' });
  }

  getEvDetailById(id: number): Observable<EvDetail[]> {
    return this.httpClient.get<EvDetail[]>(environment.getApiUrl + '/evDetails/getlist?evId=' + id);
  }

  getOdaEkServiceById(id: number): Observable<OdaEkService[]> {
    return this.httpClient.get<OdaEkService[]>(environment.getApiUrl + '/odaEkServices/getlist?evId=' + id);
  }

  getOdaOlanakById(id: number): Observable<OdaOlanak[]> {
    return this.httpClient.get<OdaOlanak[]>(environment.getApiUrl + '/odaOlanaks/getlist?evId=' + id);
  }
    
    // EV SERVİCE DETAYI ÜSTEKİ KOD 
    // 
    // 
  
    

    getGalaryById(id: number): Observable<Galary[]> {
      return this.httpClient.get<Galary[]>(environment.getApiUrl + '/galaries/getlist?evId=' + id);
    } 

}