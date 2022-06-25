import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { models } from '../models';

@Injectable({
  providedIn: 'root'
})
export class HttpDBService {

  constructor(private http: HttpClient) { }

  getmodels(): Promise<any>{
    return this.http.get(`http://localhost:3000/models`).toPromise();
  }
  getPurchase(id:number): Promise<any>{
    return this.http.get("http://localhost:3000/models"+`/${id}`).toPromise();
  }
  postmodels(data: models){
    return this.http.post("http://localhost:3000/models",data).toPromise();
  }
  deletemodels(id:number){
    return this.http.delete("http://localhost:3000/models" + `/${id}`).toPromise();
  }
  editmodels(id:number,data:models){
    return this.http.patch("http://localhost:3000/models" + `/${id}`,data).toPromise();
  }
}
