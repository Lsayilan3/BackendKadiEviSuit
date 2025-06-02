import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GalaryBlog } from '../models/GalaryBlog';
import { environment } from 'environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GalaryBlogService {

  constructor(private httpClient: HttpClient) { }


  getGalaryBlogList(): Observable<GalaryBlog[]> {

    return this.httpClient.get<GalaryBlog[]>(environment.getApiUrl + '/galaryBlogs/getall')
  }

  getGalaryBlogById(id: number): Observable<GalaryBlog> {
    return this.httpClient.get<GalaryBlog>(environment.getApiUrl + '/galaryBlogs/getbyid?galaryBlogId='+id)
  }

  addGalaryBlog(galaryBlog: GalaryBlog): Observable<any> {

    return this.httpClient.post(environment.getApiUrl + '/galaryBlogs/', galaryBlog, { responseType: 'text' });
  }

  updateGalaryBlog(galaryBlog: GalaryBlog): Observable<any> {
    return this.httpClient.put(environment.getApiUrl + '/galaryBlogs/', galaryBlog, { responseType: 'text' });

  }

  deleteGalaryBlog(id: number) {
    return this.httpClient.request('delete', environment.getApiUrl + '/galaryBlogs/', { body: { galaryBlogId: id } });
  }

  addFile(formData:FormData): Observable<any>{
    return this.httpClient.post(environment.getApiUrl + '/galaryBlogs/addPhoto', formData, { responseType: 'text' });
  }
}