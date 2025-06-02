import { Routes } from '@angular/router';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LanguageComponent } from 'app/core/components/admin/language/language.component';
import { LogDtoComponent } from 'app/core/components/admin/log/logDto.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { OperationClaimComponent } from 'app/core/components/admin/operationclaim/operationClaim.component';
import { TranslateComponent } from 'app/core/components/admin/translate/translate.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { LoginGuard } from 'app/core/guards/login-guard';
import { DashboardComponent } from '../../dashboard/dashboard.component';
import { AnasayfaComponent } from 'app/core/components/anasayfa/anasayfa.component';
import { BlogComponent } from 'app/core/components/blog/blog.component';
import { BlogDetailComponent } from 'app/core/components/blogDetail/blogDetail.component';
import { EvComponent } from 'app/core/components/ev/ev.component';
import { EvDetailComponent } from 'app/core/components/evDetail/evDetail.component';
import { GalaryBlogComponent } from 'app/core/components/galaryBlog/galaryBlog.component';
import { GalaryComponent } from 'app/core/components/galary/galary.component';
import { GirisComponent } from 'app/core/components/giris/giris.component';
import { IletisimComponent } from 'app/core/components/iletisim/iletisim.component';
import { OdaEkServiceComponent } from 'app/core/components/odaEkService/odaEkService.component';
import { OdaOlanakComponent } from 'app/core/components/odaOlanak/odaOlanak.component';
import { ResimTipiComponent } from 'app/core/components/resimTipi/resimTipi.component';
import { ServiceComponent } from 'app/core/components/service/service.component';
import { OlanaklarComponent } from 'app/core/components/olanaklar/olanaklar.component';
import { BlogPagesComponent } from 'app/core/components/blogPages/blogPages.component';
import { EvPagesComponent } from 'app/core/components/evPages/evPages.component';
import { EvPagesOdaEkServiceComponent } from 'app/core/components/evPagesOdaEkService/evPagesOdaEkService.component';
import { EvPagesOdaOlanaklarComponent } from 'app/core/components/evPagesOdaOlanaklar/evPagesOdaOlanaklar.component';
// import { EvPagesGallaryComponent } from 'app/core/components/evPagesGallary/evPagesGallary.component';





export const AdminLayoutRoutes: Routes = [

    { path: 'dashboard',      component: DashboardComponent,canActivate:[LoginGuard] }, 
    { path: 'user',           component: UserComponent, canActivate:[LoginGuard] },
    { path: 'group',          component: GroupComponent, canActivate:[LoginGuard] },
    { path: 'login',          component: LoginComponent },
    { path: 'language',       component: LanguageComponent,canActivate:[LoginGuard]},
    { path: 'translate',      component: TranslateComponent,canActivate:[LoginGuard]},
    { path: 'operationclaim', component: OperationClaimComponent,canActivate:[LoginGuard]},
    { path: 'log',            component: LogDtoComponent,canActivate:[LoginGuard]},

    { path: 'anasayfa',            component: AnasayfaComponent,canActivate:[LoginGuard]},
    { path: 'blog',            component: BlogComponent,canActivate:[LoginGuard]},
    { path: 'blogDetay',            component: BlogDetailComponent,canActivate:[LoginGuard]},
    { path: 'ev',            component: EvComponent,canActivate:[LoginGuard]},
    { path: 'evDetay',            component: EvDetailComponent,canActivate:[LoginGuard]},
    { path: 'gallary',            component: GalaryComponent,canActivate:[LoginGuard]},
    { path: 'gallaryBlog',            component: GalaryBlogComponent,canActivate:[LoginGuard]},
    { path: 'giris',            component: GirisComponent,canActivate:[LoginGuard]},
    { path: 'iletisim',            component: IletisimComponent,canActivate:[LoginGuard]},
    { path: 'odaEkService',            component: OdaEkServiceComponent,canActivate:[LoginGuard]},
    { path: 'odaOlanaklar',            component: OdaOlanakComponent,canActivate:[LoginGuard]},
    { path: 'olanaklar',            component: OlanaklarComponent,canActivate:[LoginGuard]},
    { path: 'resimTipi',            component: ResimTipiComponent,canActivate:[LoginGuard]},
    { path: 'service',            component: ServiceComponent,canActivate:[LoginGuard]},
    { path: 'blogpages/:blogId', component: BlogPagesComponent ,canActivate:[LoginGuard] },
    { path: 'evpages/:evId', component: EvPagesComponent ,canActivate:[LoginGuard] },
    { path: 'evpagesodaekservice/:evId', component: EvPagesOdaEkServiceComponent ,canActivate:[LoginGuard] },
    { path: 'evpagesodaolanaklar/:evId', component: EvPagesOdaOlanaklarComponent ,canActivate:[LoginGuard] },
    // { path: 'evpagesgalary/:evId', component: EvPagesGallaryComponent ,canActivate:[LoginGuard] },
    
];
