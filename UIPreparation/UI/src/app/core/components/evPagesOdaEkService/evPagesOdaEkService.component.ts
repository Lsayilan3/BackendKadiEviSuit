import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { environment } from 'environments/environment';
import { OdaEkServiceService } from '../odaEkService/services/OdaEkService.service';
import { OdaEkService } from '../odaEkService/models/OdaEkService';
import { Ev } from '../ev/models/Ev';
import { EvService } from '../ev/services/ev.service';
import { ActivatedRoute, Router } from '@angular/router';

declare var jQuery: any;
@Component({
  selector: 'app-evPagesOdaEkService',
  templateUrl: './evPagesOdaEkService.component.html',
  styleUrls: ['./evPagesOdaEkService.component.scss']
})
export class EvPagesOdaEkServiceComponent implements OnInit {

  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  displayedColumns: string[] = ['odaEkServiceId','evId','baslik','icon','aciklama','sira','dil', 'update','delete'];

  odaEkServices: OdaEkService[] = [];
  odaEkServiceList:OdaEkService[];


  odaEkServiceAddForm: FormGroup;
  photoForm: FormGroup;
  odaEkService: OdaEkService = new OdaEkService();
  odaEkServiceId: number;

  evId: number;
  evList:Ev[];
    ev: Ev = new Ev();

  modalBaslik: string = '';
  updateForm: FormGroup;

  constructor( private route: ActivatedRoute,  private router: Router, private evService:EvService, private odaEkServiceService:OdaEkServiceService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }



  ngAfterViewInit(): void {
    this.getOdaEkServiceById(this.ev.evId);
    this.evService.getEvList().subscribe(data=>this.evList=data);

}

  ngOnInit() {
    this.getOdaEkServiceById(this.ev.evId);
    this.updateForm = this.formBuilder.group({
      odaEkServiceId : [0],
      evId : [0],
      baslik : [""],
      icon : [""],
      aciklama : [""],
      sira : [0],
      dil : [0]
    });
    this.getOdaEkServiceById(this.ev.evId);
    this.route.params.subscribe((params) => {
      const evId = params['evId'];
      this.evId = +evId;
      this.getEvById(evId);

      this.getOdaEkServiceById(evId);
    });
    this.createOdaEkServiceAddForm();
  }

  navigateToRotaPages(evId: number) {
    this.router.navigate(['/evPagesOdaEkService', evId]);
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

  
  getOdaEkServiceById(evId: number) {
    this.evService.getOdaEkServiceById(evId).subscribe(
      (odaEkServices: OdaEkService[]) => {
        this.odaEkServices = odaEkServices;
        this.dataSource = new MatTableDataSource(odaEkServices);
        this.configDataTable();
      },
      (error) => {
        console.error(error);
        this.odaEkServices = []; 
      }
    );
  }



  getOdaServiceList() {
    this.odaEkServiceService.getOdaEkServiceList().subscribe(data => {
      this.odaEkServiceList = data;
      this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
    });
  }

  openModall() {
    this.odaEkService = new OdaEkService();
    this.odaEkServiceAddForm.patchValue({
      evId: this.ev.evId
    });
    jQuery('#odaekservice').modal('show');
  }

  save() {
    if (this.odaEkServiceAddForm.valid) {
      this.odaEkService = { ...this.odaEkServiceAddForm.value, evId: this.ev.evId };
  
      if (this.odaEkService.odaEkServiceId == 0)
        this.addOdaEkService();
      else
        this.updateOdaEkservice();
    }
  }
  

  addOdaEkService(){

    this.odaEkServiceService.addOdaEkService(this.odaEkService).subscribe(data => {
      this.getOdaEkServiceById(this.ev.evId);
      this.odaEkService = new OdaEkService();
      jQuery('#odaekservice').modal('hide');
      this.alertifyService.success(data);
      this.clearFormGroup(this.odaEkServiceAddForm);

    })

  }

  updateOdaEkservice() {
    this.odaEkServiceService.updateOdaEkService(this.odaEkService).subscribe(
      (data) => {
        const index = this.odaEkServices.findIndex((x) => x.odaEkServiceId === this.odaEkService.odaEkServiceId);
        this.odaEkServices[index] = { ...this.odaEkService }; // Güncellenen blogDetail nesnesini dizideki ilgili örnekle değiştirin
        this.dataSource = new MatTableDataSource(this.odaEkServices);
        this.configDataTable();
        this.odaEkService = new OdaEkService();
        jQuery('#odaekservice').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.odaEkServiceAddForm);
        this.getOdaEkServiceById(this.ev.evId);
      },
      
      (error) => {
        console.error('Blog detayı güncelleme hatası:', error);
      }
    );
  }
  

  createOdaEkServiceAddForm() {
    this.odaEkServiceAddForm = this.formBuilder.group({		
      odaEkServiceId : [0],
      evId : [0],
      baslik : [""],
      icon : [""],
      aciklama : [""],
      sira : [0],
      dil : [0]
    })
  }

  deleteOdaEkService(odaEkServiceId: number) {
    if (confirm('Blog detayını silmek istediğinize emin misiniz?')) {
      this.odaEkServiceService.deleteOdaEkService(odaEkServiceId).subscribe(
        (data) => {
          this.alertifyService.success(data.toString());
          this.odaEkServices = this.odaEkServices.filter((x) => x.odaEkServiceId !== odaEkServiceId);
          this.dataSource = new MatTableDataSource(this.odaEkServices);
          this.configDataTable();
        },
        (error) => {
          console.error('Blog detayı silme hatası:', error);
        }
      );
    }
  }
  
  //Burası farklı
	getOdaEkServiceiById(odaEkServiceId:number){
		this.clearFormGroup(this.odaEkServiceAddForm);
		this.odaEkServiceService.getOdaEkServiceById(odaEkServiceId).subscribe(data=>{
			this.odaEkService=data;
			this.odaEkServiceAddForm.patchValue(data);
		})
	}


  clearFormGroup(group: FormGroup) {

    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach(key => {
      group.get(key).setErrors(null);
      if (key == 'odaEkServiceId')
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
