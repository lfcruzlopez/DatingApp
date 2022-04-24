import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
{path:'', component:HomeComponent},{
  path:'',
  runGuardsAndResolvers:'always',
  children:[
    {path:'members', component:MemberListComponent, canActivate: [AuthGuard]},
    {path:'member/:id', component:MemberDetailComponent, canActivate: [AuthGuard]},
    {path:'lists', component:ListsComponent},
    {path:'message', component:MessagesComponent},
    {path:'**', component:MessagesComponent,pathMatch: 'full'},
  ]
}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
