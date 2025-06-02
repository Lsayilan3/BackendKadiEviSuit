import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Olanaklar } from '../models/Olanaklar';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class OlanaklarService {

  constructor(private httpClient: HttpClient) { }


  getOlanaklarList(): Observable<Olanaklar[]> {

    return this.httpClient.get<Olanaklar[]>(environment.getApiUrl + '/olanaklars/getall')
  }

  getOlanaklarById(id: number): Observable<Olanaklar> {
    return this.httpClient.get<Olanaklar>(environment.getApiUrl + '/olanaklars/getbyid?olanaklarId='+id)
  }

  addOlanaklar(olanaklar: Olanaklar): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/olanaklars/', olanaklar, { responseType: 'text' });
  }

  updateOlanaklar(olanaklar: Olanaklar): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/olanaklars/', olanaklar, { responseType: 'text' });

  }

  deleteOlanaklar(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/olanaklars/', { body: { olanaklarId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/olanaklars/addPhoto', formData, { responseType: 'text' });
  }
}