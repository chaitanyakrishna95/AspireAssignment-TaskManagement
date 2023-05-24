import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { TasksRoutingModule } from './tasks-routing.module';
import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { AddTaskComponent } from './add-task.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        TasksRoutingModule
    ],
    declarations: [
        LayoutComponent,
        ListComponent,
        AddTaskComponent
    ]
})
export class TasksModule { }