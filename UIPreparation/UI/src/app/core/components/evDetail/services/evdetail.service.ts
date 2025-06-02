import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EvDetail } from '../models/EvDetail';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class EvDetailService {

  constructor(private httpClient: HttpClient) { }


  getEvDetailList(): Observable<EvDetail[]> {

    return this.httpClient.get<EvDetail[]>(environment.getApiUrl + '/evDetails/getall')
  }

  getEvDetailById(id: number): Observable<EvDetail> {
    return this.httpClient.get<EvDetail>(environment.getApiUrl + '/evDetails/getbyid?evDetailId='+id)
  }

  addEvDetail(evDetail: EvDetail): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/evDetails/', evDetail, { responseType: 'text' });
  }

  updateEvDetail(evDetail: EvDetail): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/evDetails/', evDetail, { responseType: 'text' });

  }

  deleteEvDetail(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/evDetails/', { body: { evDetailId: id } });
  }


    getEvDetailiById(id: number): Observable<EvDetail> {
      return this.httpClient.get<EvDetail>(environment.getApiUrl + '/evDetails/getbyid?evDetailId='+id)
    }

}