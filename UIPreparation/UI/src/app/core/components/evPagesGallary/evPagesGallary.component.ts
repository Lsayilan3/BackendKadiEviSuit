// import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { MatPaginator } from '@angular/material/paginator';
// import { MatSort } from '@angular/material/sort';
// import { MatTableDataSource } from '@angular/material/table';
// import { AlertifyService } from 'app/core/services/alertify.service';
// import { LookUpService } from 'app/core/services/lookUp.service';
// import { AuthService } from 'app/core/components/admin/login/services/auth.service';
// import { environment } from 'environments/environment';
// import { Galary } from '../galary/models/Galary';
// import { GalaryService } from '../galary/services/Galary.service';
// import { ResimTipi } from '../resimTipi/models/ResimTipi';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ResimTipiService } from '../resimTipi/services/ResimTipi.service';
// import { Ev } from '../ev/models/Ev';
// import { EvService } from '../ev/services/ev.service';

// declare var jQuery: any;
// @Component({
//   selector: 'app-evPagesGallary',
//   templateUrl: './evPagesGallary.component.html',
//   styleUrls: ['./evPagesGallary.component.scss']
// })
// export class EvPagesGallaryComponent implements OnInit {

// dataSource: MatTableDataSource<any>;
//   @ViewChild(MatPaginator) paginator: MatPaginator;
//   @ViewChild(MatSort) sort: MatSort;
//   displayedColumns: string[] = ['galaryId','evId','photo','baslik','aciklama','resimTipiId', 'update','delete','file'];

//   galaryList:Galary[];
//   galary:Galary=new Galary();
//   galeris: Galary[] = [];
//   galaryAddForm: FormGroup;
//   photoForm: FormGroup;

//   galaryId:number;


//   modalBaslik: string = '';
//   updateForm: FormGroup;


//   ev: Ev = new Ev();
//   evId: number; // RotaId özelliğini tanımlayın

//   resimTipiList:ResimTipi[];
// 	resimTipi:ResimTipi=new ResimTipi();

//   constructor(
//     private router: Router,
//     private route: ActivatedRoute,
//     private resimTipiService:ResimTipiService,
//     private evService:EvService,
//     private galaryService:GalaryService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

//     ngAfterViewInit(): void {
//       this.resimTipiService.getResimTipiList().subscribe(data=>this.resimTipiList=data);
//         // this.getGalaryList();
//     }

//   ngOnInit() {
//     this.updateForm = this.formBuilder.group({
// 			galaryId : [0],
// evId : [0, Validators.required],
// photo : ["", Validators.required],
// baslik : ["", Validators.required],
// aciklama : ["", Validators.required],
// resimTipiId : [0, Validators.required]
//     });
//     this.upFile(this.galary.galaryId);
//     this.getGalaryById(this.galary.evId);
//     this.route.params.subscribe((params) => {
//       const evId = params['evId'];
//       // this.getEvById(evId);
//       this.getGalaryById(evId);
//     });
//     this.createGalaryAddForm();
//   }


//   getGalaryById(evId: number) {
//     this.evService.getGalaryById(evId).subscribe(
//       (galeris: Galary[]) => {
//         this.galeris = galeris;
//       },
//       (error) => {
//         // Hata yönetimi
//       }
//     );
//   }

//   openModalll() {
//     this.galary = new Galary();
//     this.galaryAddForm.patchValue({
//       gallary: this.ev.evId
//     });
//     jQuery('#rotadetayi').modal('show');
//   }
  

//   savee() {
//     if (this.galaryAddForm.valid) {
//       const formData = {
//         ...this.galaryAddForm.value,
//         evId: this.ev.evId
//       };
//       this.galaryService.addGalary(formData).subscribe(
//         (response) => {
//           // Ekleme işlemi başarılı
//           this.alertifyService.success('Rota galerisi başarıyla eklendi.');
//           this.clearFormGroup(this.galaryAddForm);
//           jQuery('#rotadetayi').modal('hide');
//           this.getGalaryById(this.ev.evId);
//         },
//         (error) => {
//           // Ekleme işlemi sırasında hata oluştu
//           this.alertifyService.error('Rota galerisi eklenirken bir hata oluştu.');
//         }
//       );
//     } else {
//       // Form geçerli değil, gerekli alanları doldurun
//       this.alertifyService.error('Lütfen tüm gerekli alanları doldurun.');
//     }
//   }
  

//   deleteResim(galary: Galary) {
//     if (confirm('Resmi silmek istediğinize emin misiniz?')) {
//       this.galaryService.deleteGalary(galary.galaryId).subscribe(
//         (response) => {
//           // İstek başarılı olduğunda yapılacak işlemler
//           console.log('Resim silme başarılı.');
//           // Silinen resmi güncellemek için yeniden çekme işlemi
//           this.getGalaryById(this.ev.evId);
//         },
//         (error) => {
//           // İstek hatalı olduğunda yapılacak işlemler
//           console.error('Resim silme hatası:', error);
//         }
//       );
//     }
//   }

//   createGalaryAddForm() {
//     this.galaryAddForm = this.formBuilder.group({
// 			galaryId : [0],
// evId : [0, Validators.required],
// photo : ["", Validators.required],
// baslik : ["", Validators.required],
// aciklama : ["", Validators.required],
// resimTipiId : [0, Validators.required]
//     });
//   }

  
// // ..........................................



// // Component dosyanızın içinde bulunan ilgili sınıfın içindeki fonksiyonu güncelleyin

// uploadFile(event) {
//   const file = (event.target as HTMLInputElement).files[0];
//   this.photoForm.patchValue({
//     file: file,
//   });
//   this.photoForm.get('file').updateValueAndValidity();
  
//   }

// upFile( id : number){

//   console.log(id);
//   this.photoForm = this.formBuilder.group({		
//     id : [id],
// file : ["", Validators.required]
//   })
// }

// addPhotoSave(){
//   var formData: any = new FormData();
//   formData.append('galaryId', this.photoForm.get('id').value);
//   formData.append('file', this.photoForm.get('file').value);		
//   // jQuery('#loginphoto').modal('hide');


// this.galaryService.addFile(formData).subscribe(data=>{
// jQuery('#photoModall').modal('hide');
//       this.clearFormGroup(this.photoForm);
//       this.getGalaryById(this.ev.evId);
//       console.log(data);
//       this.alertifyService.success(data);
// })
// }
// // GÜNCELLEME 


// // consoleLog(rotaGaleriId: number) {
// //   console.log(rotaGaleriId);
// // }



// openModal(galary: Galary) {
//   this.updateForm.patchValue({
//     galaryId: galary.galaryId,
//     evId: galary.evId,
//     photo: galary.photo,
//     baslik: galary.baslik,
//     aciklama: galary.aciklama,
//     resimTipiId: galary.resimTipiId
//   });
//   jQuery('#rotagaleriedit').modal('show');
// }


// updateGalary(): void {
//   const galaryId = this.updateForm.get('galaryId').value;

//   const updateGalary: Galary = {
//     galaryId: galaryId,
//     evId: this.updateForm.get('evId').value,
//     photo: this.updateForm.get('photo').value,
//     baslik: this.updateForm.get('baslik').value,
//     aciklama: this.updateForm.get('aciklama').value,
//     resimTipiId: this.updateForm.get('resimTipiId').value
//   };
//   const index = this.galaryList.findIndex(x => x.galaryId == galaryId);
//   this.galaryList[index] = updateGalary;
//   this.galary = new Galary();
//   jQuery('#rotagaleriedit').modal('hide');

//   this.clearFormGroup(this.galaryAddForm);

//   this.galaryService.updateGalary(updateGalary).subscribe(
//     response => {
//       // Güncelleme işlemi başarılı
//       console.log('Güncelleme işlemi başarılı:', response);

//       // Güncellemeden sonra verileri yenilemek için
//       this.getGalaryById(this.ev.evId);
//     },
//     error => {
//       // Güncelleme işlemi hatası
//       console.error('Güncelleme işlemi hatası:', error);
//     }
//   );
// }


//   clearFormGroup(group: FormGroup) {

//     group.markAsUntouched();
//     group.reset();

//     Object.keys(group.controls).forEach(key => {
//       group.get(key).setErrors(null);
//       if (key == 'galaryId')
//         group.get(key).setValue(0);
//     });
//   }

//   checkClaim(claim:string):boolean{
//     return this.authService.claimGuard(claim)
//   }

//   configDataTable(): void {
//     this.dataSource.paginator = this.paginator;
//     this.dataSource.sort = this.sort;
//   }

//   applyFilter(event: Event) {
//     const filterValue = (event.target as HTMLInputElement).value;
//     this.dataSource.filter = filterValue.trim().toLowerCase();

//     if (this.dataSource.paginator) {
//       this.dataSource.paginator.firstPage();
//     }
//   }

// }
