import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { environment } from 'environments/environment';
import { OdaOlanak } from '../odaOlanak/models/OdaOlanak';
import { OdaOlanakService } from '../odaOlanak/services/OdaOlanak.service';
import { Ev } from '../ev/models/Ev';
import { EvService } from '../ev/services/ev.service';
import { ActivatedRoute, Router } from '@angular/router';

declare var jQuery: any;

@Component({
  selector: 'app-evPagesOdaOlanaklar',
  templateUrl: './evPagesOdaOlanaklar.component.html',
  styleUrls: ['./evPagesOdaOlanaklar.component.scss']
})
export class EvPagesOdaOlanaklarComponent implements OnInit {

  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  displayedColumns: string[] = ['odaOlanakId','evId','baslik','icon','aciklama','sira','dil', 'update','delete'];

  odaOlanaks: OdaOlanak[] = [];
  odaOlanakList:OdaOlanak[];


  odaOlanakAddForm: FormGroup;
  photoForm: FormGroup;
  odaOlanak: OdaOlanak = new OdaOlanak();
  odaOlanakId: number;

  evId: number;
  evList:Ev[];
  ev: Ev = new Ev();

  modalBaslik: string = '';
  updateForm: FormGroup;

  constructor(private route: ActivatedRoute,  private router: Router, private evService:EvService, private odaOlanakService:OdaOlanakService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

  ngAfterViewInit(): void {
     this.getOdaOlanakById(this.ev.evId);
     this.evService.getEvList().subscribe(data=>this.evList=data);
 
 }
 
   ngOnInit() {
     this.getOdaOlanakById(this.ev.evId);
     this.updateForm = this.formBuilder.group({
       odaEkServiceId : [0],
       evId : [0],
       baslik : [""],
       icon : [""],
       aciklama : [""],
       sira : [0],
       dil : [0]
     });
     this.getOdaOlanakById(this.ev.evId);
     this.route.params.subscribe((params) => {
       const evId = params['evId'];
       this.evId = +evId;
       this.getEvById(evId);
 
       this.getOdaOlanakById(evId);
     });
     this.createOdaOlanakAddForm();
   }
 
   navigateToRotaPages(evId: number) {
     this.router.navigate(['/evPagesOdaOlanaklar', evId]);
   }
 
 
   
 
 
   getEvById(evId: number) {
     this.evService.getEvById(evId).subscribe(
       (ev: Ev) => {
         this.ev = ev;
       },
       (error) => {
         // Hata yönetimi
       }
     );
   }
 
   
   getOdaOlanakById(evId: number) {
     this.evService.getOdaOlanakById(evId).subscribe(
       (odaOlanaks: OdaOlanak[]) => {
         this.odaOlanaks = odaOlanaks;
         this.dataSource = new MatTableDataSource(odaOlanaks);
         this.configDataTable();
       },
       (error) => {
         console.error(error);
         this.odaOlanaks = []; 
       }
     );
   }
 
 
 
   getOdaOlanakList() {
     this.odaOlanakService.getOdaOlanakList().subscribe(data => {
       this.odaOlanakList = data;
       this.dataSource = new MatTableDataSource(data);
             this.configDataTable();
     });
   }
 
   openModall() {
     this.odaOlanak = new OdaOlanak();
     this.odaOlanakAddForm.patchValue({
       evId: this.ev.evId
     });
     jQuery('#odaolanak').modal('show');
   }
 
   save() {
     if (this.odaOlanakAddForm.valid) {
       this.odaOlanak = { ...this.odaOlanakAddForm.value, evId: this.ev.evId };
   
       if (this.odaOlanak.odaOlanakId == 0)
         this.addOdaOlanak();
       else
         this.updateOdaOlanak();
     }
   }
   
 
   addOdaOlanak(){
 
     this.odaOlanakService.addOdaOlanak(this.odaOlanak).subscribe(data => {
       this.getOdaOlanakById(this.ev.evId);
       this.odaOlanak = new OdaOlanak();
       jQuery('#odaolanak').modal('hide');
       this.alertifyService.success(data);
       this.clearFormGroup(this.odaOlanakAddForm);
 
     })
 
   }
 
   updateOdaOlanak() {
     this.odaOlanakService.updateOdaOlanak(this.odaOlanak).subscribe(
       (data) => {
         const index = this.odaOlanaks.findIndex((x) => x.odaOlanakId === this.odaOlanak.odaOlanakId);
         this.odaOlanaks[index] = { ...this.odaOlanak }; // Güncellenen blogDetail nesnesini dizideki ilgili örnekle değiştirin
         this.dataSource = new MatTableDataSource(this.odaOlanaks);
         this.configDataTable();
         this.odaOlanak = new OdaOlanak();
         jQuery('#odaolanak').modal('hide');
         this.alertifyService.success(data);
         this.clearFormGroup(this.odaOlanakAddForm);
         this.getOdaOlanakById(this.ev.evId);
       },
       
       (error) => {
         console.error('Blog detayı güncelleme hatası:', error);
       }
     );
   }
   
 
   createOdaOlanakAddForm() {
     this.odaOlanakAddForm = this.formBuilder.group({		
			odaOlanakId : [0],
evId : [0],
baslik : [""],
icon : [""],
aciklama : [""],
sira : [0],
dil : [0]
     })
   }
 
   deleteOdaOlanak(odaOlanakId: number) {
     if (confirm('Blog detayını silmek istediğinize emin misiniz?')) {
       this.odaOlanakService.deleteOdaOlanak(odaOlanakId).subscribe(
         (data) => {
           this.alertifyService.success(data.toString());
           this.odaOlanaks = this.odaOlanaks.filter((x) => x.odaOlanakId !== odaOlanakId);
           this.dataSource = new MatTableDataSource(this.odaOlanaks);
           this.configDataTable();
         },
         (error) => {
           console.error('Blog detayı silme hatası:', error);
         }
       );
     }
   }
   
   //Burası farklı
  getOdaOlanakiById(odaEkServiceId:number){
    this.clearFormGroup(this.odaOlanakAddForm);
    this.odaOlanakService.getOdaOlanakById(odaEkServiceId).subscribe(data=>{
      this.odaOlanak=data;
      this.odaOlanakAddForm.patchValue(data);
    })
  }
 
 
   clearFormGroup(group: FormGroup) {
 
     group.markAsUntouched();
     group.reset();
 
     Object.keys(group.controls).forEach(key => {
       group.get(key).setErrors(null);
       if (key == 'odaOlanakId')
         group.get(key).setValue(0);
     });
   }
 
   checkClaim(claim:string):boolean{
     return this.authService.claimGuard(claim)
   }
 
   configDataTable(): void {
     this.dataSource.paginator = this.paginator;
     this.dataSource.sort = this.sort;
   }
 
   applyFilter(event: Event) {
     const filterValue = (event.target as HTMLInputElement).value;
     this.dataSource.filter = filterValue.trim().toLowerCase();
 
     if (this.dataSource.paginator) {
       this.dataSource.paginator.firstPage();
     }
   }


}
