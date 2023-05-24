import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AlertService } from '../_services/alert.service';
import { UserService } from '../_services/user.service';
import { TaskService } from '../_services/task.service';
import { AccountService } from '../_services/account.service';
import { TokenResponse } from '../_models/user';

@Component({ templateUrl: 'add-task.component.html' })
export class AddTaskComponent implements OnInit {
    form!: FormGroup;
    id?: number;
    title!: string;
    loading = false;
    submitting = false;
    submitted = false;
    name?: string;
    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private userService: UserService,
        private alertService: AlertService,
        private taskservice: TaskService,
        private accountservice : AccountService
        
    ) {        
    }
    
    ngOnInit() {
        this.id = this.route.snapshot.params['id'];
        this.form = this.formBuilder.group({
            title: ['', Validators.required],
            taskdescription: ['', Validators.required],
            file: ['', Validators.required],
            status: ['', Validators.required],
        });

        this.title = 'Add Task';
        if (this.id) {
            // edit mode
            this.title = 'Edit App';
            this.loading = true;
            this.userService.getById(this.id)
                .pipe(first())
                .subscribe(x => {
                    this.form.patchValue(x);
                    this.loading = false;
                });
        }
    }
     
    // convenience getter for easy access to form fields
    get f() { return this.form.controls; }
    file:any;
    getFile(event:any)
    {
      this.file=event.target.files[0];

    }
    onSubmit() {
        this.submitted = true;

        // reset alerts on submit
        this.alertService.clear();

        // stop here if form is invalid
        if (this.form.invalid) {
            return;
        }
         const user= JSON.parse(localStorage.getItem('user')!);
        // const users=this.accountservice.userValue;
        let formdata=new FormData();
        formdata.set("username",user.userName);        
        formdata.set("title",this.f.title.value);
        formdata.set("taskdescription",this.f.taskdescription.value);
        formdata.set("file",this.file);
        formdata.set("status",this.f.status.value);
        
        this.submitting = true;
        this.taskservice.post(formdata)
            .subscribe({
                next: () => {
                    this.alertService.success('Task details are saved', { keepAfterRouteChange: true });
                    this.router.navigateByUrl('/tasks');
                },
                error: (error: any) => {
                    this.alertService.error(error);
                    this.submitting = false;
                }
            })
    }

}