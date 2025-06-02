import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Galary } from '../models/Galary';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GalaryService {

  constructor(private httpClient: HttpClient) { }


  getGalaryList(): Observable<Galary[]> {

    return this.httpClient.get<Galary[]>(environment.getApiUrl + '/galaries/getall')
  }

  getGalaryById(id: number): Observable<Galary> {
    return this.httpClient.get<Galary>(environment.getApiUrl + '/galaries/getbyid?galaryId='+id)
  }

  addGalary(galary: Galary): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/galaries/', galary, { responseType: 'text' });
  }

  updateGalary(galary: Galary): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/galaries/', galary, { responseType: 'text' });

  }

  deleteGalary(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/galaries/', { body: { galaryId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/galaries/addPhoto', formData, { responseType: 'text' });
  }

}