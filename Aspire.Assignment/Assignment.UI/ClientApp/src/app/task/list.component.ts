import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { TaskService } from '../_services/task.service';
import { TaskFile } from '../_models/taskfile';
import { AlertService } from '../_services/alert.service';


@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    tasks?: TaskFile[];

    constructor(private taskService: TaskService,private alertService: AlertService ) {}

    ngOnInit() {
        this.taskService.getAll()
           // .pipe(first())
            .subscribe(tasks => this.tasks = tasks);
    }
    downloadFiles(filename:any)
    {
        this.taskService.DownloadFile(filename).subscribe(response=>{
             let fileName=filename;
             let blob:Blob=response.body as Blob;
             let a=document.createElement('a');
             a.download=fileName;
             a.href=window.URL.createObjectURL(blob);
             a.click();
            
           
        });
    }
}