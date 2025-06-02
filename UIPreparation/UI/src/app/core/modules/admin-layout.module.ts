import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { DashboardComponent } from '../components/app/dashboard/dashboard.component';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { TranslateLoader, TranslateModule, TranslatePipe } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslationService } from 'app/core/services/translation.service';
import { LanguageComponent } from '../components/admin/language/language.component';
import { TranslateComponent } from '../components/admin/translate/translate.component';
import { OperationClaimComponent } from '../components/admin/operationclaim/operationClaim.component';
import { LogDtoComponent } from '../components/admin/log/logDto.component';
import { MatSortModule } from '@angular/material/sort';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { AnasayfaComponent } from '../components/anasayfa/anasayfa.component';
import { BlogComponent } from '../components/blog/blog.component';
import { BlogDetailComponent } from '../components/blogDetail/blogDetail.component';
import { EvComponent } from '../components/ev/ev.component';
import { EvDetailComponent } from '../components/evDetail/evDetail.component';
import { GalaryBlogComponent } from '../components/galaryBlog/galaryBlog.component';
import { GalaryComponent } from '../components/galary/galary.component';
import { GirisComponent } from '../components/giris/giris.component';
import { IletisimComponent } from '../components/iletisim/iletisim.component';
import { OdaEkServiceComponent } from '../components/odaEkService/odaEkService.component';
import { OdaOlanakComponent } from '../components/odaOlanak/odaOlanak.component';
import { OlanaklarComponent } from '../components/olanaklar/olanaklar.component';
import { ServiceComponent } from '../components/service/service.component';
import { ResimTipiComponent } from '../components/resimTipi/resimTipi.component';
import { BlogPagesComponent } from '../components/blogPages/blogPages.component';
import { EvPagesComponent } from '../components/evPages/evPages.component';
import { EvPagesOdaEkServiceComponent } from '../components/evPagesOdaEkService/evPagesOdaEkService.component';
import { EvPagesOdaOlanaklarComponent } from '../components/evPagesOdaOlanaklar/evPagesOdaOlanaklar.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
// import { EvPagesGallaryComponent } from '../components/evPagesGallary/evPagesGallary.component';


// export function layoutHttpLoaderFactory(http: HttpClient) {
// 
//   return new TranslateHttpLoader(http,'../../../../../../assets/i18n/','.json');
// }

@NgModule({
    imports: [
        AngularEditorModule,   //Ng kalkov editör
        CommonModule,
        RouterModule.forChild(AdminLayoutRoutes),
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatTooltipModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCheckboxModule,
        NgbModule,
        NgMultiSelectDropDownModule,
        SweetAlert2Module,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                //useFactory:layoutHttpLoaderFactory,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })
    ],
    declarations: [
        DashboardComponent,
        UserComponent,
        LoginComponent,
        GroupComponent,
        LanguageComponent,
        TranslateComponent,
        OperationClaimComponent,
        LogDtoComponent,
        AnasayfaComponent,
        BlogComponent,
        BlogDetailComponent,
        EvComponent,
        EvDetailComponent,
        GalaryComponent,
        GalaryBlogComponent,
        GirisComponent,
        IletisimComponent,
        OdaEkServiceComponent,
        OdaOlanakComponent,
        OlanaklarComponent,
        ServiceComponent,
        ResimTipiComponent,
        BlogPagesComponent,
        EvPagesComponent,
        EvPagesOdaEkServiceComponent,
        EvPagesOdaOlanaklarComponent,
        // EvPagesGallaryComponent

    ]
})

export class AdminLayoutModule { }
