import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { environment } from 'environments/environment';
import { EvService } from './services/ev.service';
import { Ev } from './models/Ev';
import { Router } from '@angular/router';

declare var jQuery: any;

@Component({
	selector: 'app-ev',
	templateUrl: './ev.component.html',
	styleUrls: ['./ev.component.scss']
})
export class EvComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['evId','baslik','url','photo','sira','dil', 'update','delete','file','search','searchh','searchhh','searchhhh'];

	evList:Ev[];
	ev:Ev=new Ev();

	evAddForm: FormGroup;
	photoForm: FormGroup;


	evId:number;
	odaEkServiceId:number;

	constructor(private router: Router, private evService:EvService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getEvList();
    }

	ngOnInit() {

		this.createEvAddForm();
	}

	navigateToRotaPages(evId: number) {
		this.router.navigate(['/evpages', evId]);
	  }

	  navigateToRotaPagesOdaEkService(evId: number) {
		this.router.navigate(['/evpagesodaekservice', evId]);
	  }

	  navigateToRotaPagesOdaOlanaklar(evId: number) {
		this.router.navigate(['/evpagesodaolanaklar', evId]);
	  }
	  navigateToRotaPagesGalary(evId: number) {
		this.router.navigate(['/evpagesgalary', evId]);
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
			formData.append('evId', this.photoForm.get('id').value);
			formData.append('file', this.photoForm.get('file').value);		
			// jQuery('#loginphoto').modal('hide');
		
	
		this.evService.addFile(formData).subscribe(data=>{
		jQuery('#photoModal').modal('hide');
					this.clearFormGroup(this.photoForm);
					this.getEvList();
					console.log(data);
					this.alertifyService.success(data);
		})
		}

	getEvList() {
		this.evService.getEvList().subscribe(data => {
			this.evList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.evAddForm.valid) {
			this.ev = Object.assign({}, this.evAddForm.value)

			if (this.ev.evId == 0)
				this.addEv();
			else
				this.updateEv();
		}

	}

	addEv(){

		this.evService.addEv(this.ev).subscribe(data => {
			this.getEvList();
			this.ev = new Ev();
			jQuery('#ev').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.evAddForm);

		})

	}

	updateEv(){

		this.evService.updateEv(this.ev).subscribe(data => {

			var index=this.evList.findIndex(x=>x.evId==this.ev.evId);
			this.evList[index]=this.ev;
			this.dataSource = new MatTableDataSource(this.evList);
            this.configDataTable();
			this.ev = new Ev();
			jQuery('#ev').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.evAddForm);

		})

	}

	createEvAddForm() {
		this.evAddForm = this.formBuilder.group({		
			evId : [0],
baslik : ["", Validators.required],
url : ["", Validators.required],
photo : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteEv(evId:number){
		this.evService.deleteEv(evId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.evList=this.evList.filter(x=> x.evId!=evId);
			this.dataSource = new MatTableDataSource(this.evList);
			this.configDataTable();
		})
	}

	getEvById(evId:number){
		this.clearFormGroup(this.evAddForm);
		this.evService.getEvById(evId).subscribe(data=>{
			this.ev=data;
			this.evAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'evId')
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
