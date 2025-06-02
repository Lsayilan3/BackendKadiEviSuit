import { EvDetail } from '../evDetail/models/EvDetail';
import { Component, OnInit,ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { Ev } from '../ev/models/Ev';
import { EvDetailService } from '../evDetail/services/EvDetail.service';
import { EvService } from '../ev/services/ev.service';
import { ActivatedRoute, Router } from '@angular/router';
declare var jQuery: any;


@Component({
  selector: 'app-evPages',
  templateUrl: './evPages.component.html',
  styleUrls: ['./evPages.component.scss']
})
export class EvPagesComponent implements OnInit {

  dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;

  evDetails: EvDetail[] = [];
  evDetailList:EvDetail[];
  evDetail:EvDetail=new EvDetail();
  evDetailAddForm: FormGroup;
  
  displayedColumns: string[] = ['evDetailId','evId','baslik','p','cocukBaslik','cocukP','editor','sira','dil', 'update','delete'];

  photoForm: FormGroup;
  ev: Ev = new Ev();
  evId: number;
  
  
  
  evList:Ev[];
  
  modalBaslik: string = '';
  updateForm: FormGroup;

  constructor(private route: ActivatedRoute, private router: Router,private evService:EvService, private evDetailService:EvDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }


 ngAfterViewInit(): void {
    this.getEvDetailById(this.ev.evId);
    this.evService.getEvList().subscribe(data=>this.evList=data);

}

ngOnInit() {
  this.getEvDetailById(this.ev.evId);

  this.getEvDetailById(this.ev.evId);
  this.route.params.subscribe((params) => {
    const evId = params['evId'];
    this.evId = +evId;
    this.getEvById(evId);

    this.getEvDetailById(evId);
  });
  this.createEvDetailAddForm();
}


  navigateToRotaPages(evId: number) {
    this.router.navigate(['/evpages', evId]);
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

  
  getEvDetailById(evId: number) {
    this.evService.getEvDetailById(evId).subscribe(
      (evDetails: EvDetail[]) => {
        this.evDetails = evDetails;
        this.dataSource = new MatTableDataSource(evDetails);
        this.configDataTable();
      },
      (error) => {
        console.error(error);
        this.evDetails = []; 
      }
    );
  }



  getEvDetailList() {
    this.evDetailService.getEvDetailList().subscribe(data => {
      this.evDetailList = data;
      this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
    });
  }

  openModall() {
    this.evDetail = new EvDetail();
    this.evDetailAddForm.patchValue({
      evId: this.ev.evId
    });
    jQuery('#evdetail').modal('show');
  }

  save() {
    if (this.evDetailAddForm.valid) {
      this.evDetail = { ...this.evDetailAddForm.value, evId: this.ev.evId };
  
      if (this.evDetail.evDetailId == 0)
        this.addEvDetail();
      else
        this.updateEvDetail();
    }
  }
  

  addEvDetail(){

    this.evDetailService.addEvDetail(this.evDetail).subscribe(data => {
      this.getEvDetailById(this.ev.evId);
      this.evDetail = new EvDetail();
      jQuery('#evdetail').modal('hide');
      this.alertifyService.success(data);
      this.clearFormGroup(this.evDetailAddForm);

    })

  }

  updateEvDetail() {
    this.evDetailService.updateEvDetail(this.evDetail).subscribe(
      (data) => {
        const index = this.evDetails.findIndex((x) => x.evDetailId === this.evDetail.evDetailId);
        this.evDetails[index] = { ...this.evDetail }; // Güncellenen blogDetail nesnesini dizideki ilgili örnekle değiştirin
        this.dataSource = new MatTableDataSource(this.evDetails);
        this.configDataTable();
        this.evDetail = new EvDetail();
        jQuery('#evdetail').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.evDetailAddForm);
        this.getEvDetailById(this.ev.evId);
      },
      
      (error) => {
        console.error('Ev detayı güncelleme hatası:', error);
      }
    );
  }
  

  createEvDetailAddForm() {
    this.evDetailAddForm = this.formBuilder.group({		
      evDetailId : [0],
      evId : [0],
      baslik : [""],
      p : [""],
      cocukBaslik : [""],
      cocukP : [""],
      editor : [""],
      sira : [0],
      dil : [0],
    })
  }

  deleteEvDetail(evDetailId: number) {
    if (confirm('Blog detayını silmek istediğinize emin misiniz?')) {
      this.evDetailService.deleteEvDetail(evDetailId).subscribe(
        (data) => {
          this.alertifyService.success(data.toString());
          this.evDetails = this.evDetails.filter((x) => x.evDetailId !== evDetailId);
          this.dataSource = new MatTableDataSource(this.evDetails);
          this.configDataTable();
        },
        (error) => {
          console.error('Blog detayı silme hatası:', error);
        }
      );
    }
  }
  
  //Burası farklı
  getEvDetailiById(evDetailId:number){
    this.clearFormGroup(this.evDetailAddForm);
    this.evDetailService.getEvDetailById(evDetailId).subscribe(data=>{
      this.evDetail=data;
      this.evDetailAddForm.patchValue(data);
    })
  }


  clearFormGroup(group: FormGroup) {

    group.markAsUntouched();
    group.reset();

    Object.keys(group.controls).forEach(key => {
      group.get(key).setErrors(null);
      if (key == 'evdetailId')
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
