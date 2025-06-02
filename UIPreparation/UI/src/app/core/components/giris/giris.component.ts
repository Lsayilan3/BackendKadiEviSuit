import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Giris } from './models/Giris';
import { GirisService } from './services/Giris.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-giris',
	templateUrl: './giris.component.html',
	styleUrls: ['./giris.component.scss']
})
export class GirisComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['girisId','baslik','pBir','pIki','photo','sira','dil', 'update','delete','file'];

	girisList:Giris[];
	giris:Giris=new Giris();

	girisAddForm: FormGroup;
	photoForm: FormGroup;


	girisId:number;

	constructor(private girisService:GirisService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getGirisList();
    }

	ngOnInit() {

		this.createGirisAddForm();
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
			formData.append('girisId', this.photoForm.get('id').value);
			formData.append('file', this.photoForm.get('file').value);		
			// jQuery('#loginphoto').modal('hide');
		
	
		this.girisService.addFile(formData).subscribe(data=>{
		jQuery('#photoModal').modal('hide');
					this.clearFormGroup(this.photoForm);
					this.getGirisList();
					console.log(data);
					this.alertifyService.success(data);
		})
		}


	getGirisList() {
		this.girisService.getGirisList().subscribe(data => {
			this.girisList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.girisAddForm.valid) {
			this.giris = Object.assign({}, this.girisAddForm.value)

			if (this.giris.girisId == 0)
				this.addGiris();
			else
				this.updateGiris();
		}

	}

	addGiris(){

		this.girisService.addGiris(this.giris).subscribe(data => {
			this.getGirisList();
			this.giris = new Giris();
			jQuery('#giris').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.girisAddForm);

		})

	}

	updateGiris(){

		this.girisService.updateGiris(this.giris).subscribe(data => {

			var index=this.girisList.findIndex(x=>x.girisId==this.giris.girisId);
			this.girisList[index]=this.giris;
			this.dataSource = new MatTableDataSource(this.girisList);
            this.configDataTable();
			this.giris = new Giris();
			jQuery('#giris').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.girisAddForm);

		})

	}

	createGirisAddForm() {
		this.girisAddForm = this.formBuilder.group({		
			girisId : [0],
baslik : ["", Validators.required],
pBir : ["", Validators.required],
pIki : ["", Validators.required],
photo : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteGiris(girisId:number){
		this.girisService.deleteGiris(girisId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.girisList=this.girisList.filter(x=> x.girisId!=girisId);
			this.dataSource = new MatTableDataSource(this.girisList);
			this.configDataTable();
		})
	}

	getGirisById(girisId:number){
		this.clearFormGroup(this.girisAddForm);
		this.girisService.getGirisById(girisId).subscribe(data=>{
			this.giris=data;
			this.girisAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'girisId')
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
