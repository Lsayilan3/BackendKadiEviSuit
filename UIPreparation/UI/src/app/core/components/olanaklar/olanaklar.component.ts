import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Olanaklar } from './models/Olanaklar';
import { OlanaklarService } from './services/Olanaklar.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-olanaklar',
	templateUrl: './olanaklar.component.html',
	styleUrls: ['./olanaklar.component.scss']
})
export class OlanaklarComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['olanaklarId','baslik','aciklama','photo','sira','dil', 'update','delete','file'];

	olanaklarList:Olanaklar[];
	olanaklar:Olanaklar=new Olanaklar();

	olanaklarAddForm: FormGroup;
	photoForm: FormGroup;


	olanaklarId:number;

	constructor(private olanaklarService:OlanaklarService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOlanaklarList();
    }

	ngOnInit() {

		this.createOlanaklarAddForm();
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
			formData.append('olanaklarId', this.photoForm.get('id').value);
			formData.append('file', this.photoForm.get('file').value);		
			// jQuery('#loginphoto').modal('hide');
		
	
		this.olanaklarService.addFile(formData).subscribe(data=>{
		jQuery('#photoModal').modal('hide');
					this.clearFormGroup(this.photoForm);
					this.getOlanaklarList();
					console.log(data);
					this.alertifyService.success(data);
		})
		}


	getOlanaklarList() {
		this.olanaklarService.getOlanaklarList().subscribe(data => {
			this.olanaklarList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.olanaklarAddForm.valid) {
			this.olanaklar = Object.assign({}, this.olanaklarAddForm.value)

			if (this.olanaklar.olanaklarId == 0)
				this.addOlanaklar();
			else
				this.updateOlanaklar();
		}

	}

	addOlanaklar(){

		this.olanaklarService.addOlanaklar(this.olanaklar).subscribe(data => {
			this.getOlanaklarList();
			this.olanaklar = new Olanaklar();
			jQuery('#olanaklar').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.olanaklarAddForm);

		})

	}

	updateOlanaklar(){

		this.olanaklarService.updateOlanaklar(this.olanaklar).subscribe(data => {

			var index=this.olanaklarList.findIndex(x=>x.olanaklarId==this.olanaklar.olanaklarId);
			this.olanaklarList[index]=this.olanaklar;
			this.dataSource = new MatTableDataSource(this.olanaklarList);
            this.configDataTable();
			this.olanaklar = new Olanaklar();
			jQuery('#olanaklar').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.olanaklarAddForm);

		})

	}

	createOlanaklarAddForm() {
		this.olanaklarAddForm = this.formBuilder.group({		
			olanaklarId : [0],
baslik : ["", Validators.required],
aciklama : ["", Validators.required],
photo : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteOlanaklar(olanaklarId:number){
		this.olanaklarService.deleteOlanaklar(olanaklarId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.olanaklarList=this.olanaklarList.filter(x=> x.olanaklarId!=olanaklarId);
			this.dataSource = new MatTableDataSource(this.olanaklarList);
			this.configDataTable();
		})
	}

	getOlanaklarById(olanaklarId:number){
		this.clearFormGroup(this.olanaklarAddForm);
		this.olanaklarService.getOlanaklarById(olanaklarId).subscribe(data=>{
			this.olanaklar=data;
			this.olanaklarAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'olanaklarId')
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
