import { ResimTipiService } from './../resimTipi/services/resimtipi.service';
import { ResimTipi } from './../resimTipi/models/resimtipi';
import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Galary } from './models/Galary';
import { GalaryService } from './services/Galary.service';
import { environment } from 'environments/environment';
import { EvService } from '../ev/services/ev.service';
import { Ev } from '../ev/models/Ev';

declare var jQuery: any;

@Component({
	selector: 'app-galary',
	templateUrl: './galary.component.html',
	styleUrls: ['./galary.component.scss']
})
export class GalaryComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['galaryId','evId','photo','baslik','aciklama','resimTipiId', 'update','delete','file'];

	galaryList:Galary[];
	galary:Galary=new Galary();

	galaryAddForm: FormGroup;
	photoForm: FormGroup;

	galaryId:number;
	evList:Ev[];
	resimTipiList:ResimTipi[];
	constructor(private resimTipiService: ResimTipiService, private evService:EvService, private galaryService:GalaryService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.evService.getEvList().subscribe(data=>this.evList=data);
		this.resimTipiService.getResimTipiList().subscribe(data=>this.resimTipiList=data);
        this.getGalaryList();
    }

	ngOnInit() {
		this.evService.getEvList().subscribe(data=>this.evList=data);
		this.createGalaryAddForm();
	}

	uploadFile(event) {
			const file = (event.target as HTMLInputElement).files[0];
			this.photoForm.patchValue({
			  file: file,
			});
			this.photoForm.get('file').updateValueAndValidity();
			
		  }
	
		upFile( id : number){
			this.photoForm = this.formBuilder.group({		
				id : [id],
		file : ["", Validators.required]
			})
		}
	
		addPhotoSave(){
			var formData: any = new FormData();
			formData.append('galaryId', this.photoForm.get('id').value);
			formData.append('file', this.photoForm.get('file').value);		
			// jQuery('#loginphoto').modal('hide');
		
	
		this.galaryService.addFile(formData).subscribe(data=>{
		jQuery('#photoModal').modal('hide');
					this.clearFormGroup(this.photoForm);
					this.getGalaryList();
					console.log(data);
					this.alertifyService.success(data);
		})
		}


	getGalaryList() {
		this.galaryService.getGalaryList().subscribe(data => {
			this.galaryList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.galaryAddForm.valid) {
			this.galary = Object.assign({}, this.galaryAddForm.value)

			if (this.galary.galaryId == 0)
				this.addGalary();
			else
				this.updateGalary();
		}

	}

	addGalary(){

		this.galaryService.addGalary(this.galary).subscribe(data => {
			this.getGalaryList();
			this.galary = new Galary();
			jQuery('#galary').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.galaryAddForm);

		})

	}

	updateGalary(){

		this.galaryService.updateGalary(this.galary).subscribe(data => {

			var index=this.galaryList.findIndex(x=>x.galaryId==this.galary.galaryId);
			this.galaryList[index]=this.galary;
			this.dataSource = new MatTableDataSource(this.galaryList);
            this.configDataTable();
			this.galary = new Galary();
			jQuery('#galary').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.galaryAddForm);

		})

	}

	createGalaryAddForm() {
		this.galaryAddForm = this.formBuilder.group({		
			galaryId : [0],
evId : [0, Validators.required],
photo : ["", Validators.required],
baslik : ["", Validators.required],
aciklama : ["", Validators.required],
resimTipiId : [0, Validators.required]
		})
	}

	deleteGalary(galaryId:number){
		this.galaryService.deleteGalary(galaryId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.galaryList=this.galaryList.filter(x=> x.galaryId!=galaryId);
			this.dataSource = new MatTableDataSource(this.galaryList);
			this.configDataTable();
		})
	}

	getGalaryById(galaryId:number){
		this.clearFormGroup(this.galaryAddForm);
		this.galaryService.getGalaryById(galaryId).subscribe(data=>{
			this.galary=data;
			this.galaryAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'galaryId')
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
