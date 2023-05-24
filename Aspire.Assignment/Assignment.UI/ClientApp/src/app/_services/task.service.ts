import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Task } from "../_models/task";
import { Observable } from "rxjs";
import { User } from "oidc-client";
import { HttpHeaders } from '@angular/common/http';
import { Type } from "@angular/compiler";
import { HttpParams } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class TaskService {
    constructor(private http: HttpClient) { }

    public getAll() {
        
        return this.http.get<Task[]>(`${environment.apiUrl}/api/TaskDetail`);
    }

    public post(formdata:FormData) {
      
        console.log(formdata);
        
        return this.http.post(`${environment.apiUrl}/api/TaskDetail`, formdata);
    }
    
    public DownloadFile(fileName:string)
    {
        
        // let params=new HttpParams();
        // params=params.append(,filename);
        return this.http.get(`${environment.apiUrl}/api/TaskDetail/DownloadFile`,{params:{fileName},observe:'response',responseType:'blob'});

    }
} 