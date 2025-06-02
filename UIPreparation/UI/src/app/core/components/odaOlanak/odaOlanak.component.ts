import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OdaOlanak } from './models/OdaOlanak';
import { environment } from 'environments/environment';
import { OdaOlanakService } from './services/OdaOlanak.service';

declare var jQuery: any;

@Component({
	selector: 'app-odaOlanak',
	templateUrl: './odaOlanak.component.html',
	styleUrls: ['./odaOlanak.component.scss']
})
export class OdaOlanakComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['odaOlanakId','evId','baslik','icon','aciklama','sira','dil', 'update','delete'];

	odaOlanakList:OdaOlanak[];
	odaOlanak:OdaOlanak=new OdaOlanak();

	odaOlanakAddForm: FormGroup;


	odaOlanakId:number;

	constructor(private odaOlanakService:OdaOlanakService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getOdaOlanakList();
    }

	ngOnInit() {

		this.createOdaOlanakAddForm();
	}


	getOdaOlanakList() {
		this.odaOlanakService.getOdaOlanakList().subscribe(data => {
			this.odaOlanakList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.odaOlanakAddForm.valid) {
			this.odaOlanak = Object.assign({}, this.odaOlanakAddForm.value)

			if (this.odaOlanak.odaOlanakId == 0)
				this.addOdaOlanak();
			else
				this.updateOdaOlanak();
		}

	}

	addOdaOlanak(){

		this.odaOlanakService.addOdaOlanak(this.odaOlanak).subscribe(data => {
			this.getOdaOlanakList();
			this.odaOlanak = new OdaOlanak();
			jQuery('#odaolanak').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.odaOlanakAddForm);

		})

	}

	updateOdaOlanak(){

		this.odaOlanakService.updateOdaOlanak(this.odaOlanak).subscribe(data => {

			var index=this.odaOlanakList.findIndex(x=>x.odaOlanakId==this.odaOlanak.odaOlanakId);
			this.odaOlanakList[index]=this.odaOlanak;
			this.dataSource = new MatTableDataSource(this.odaOlanakList);
            this.configDataTable();
			this.odaOlanak = new OdaOlanak();
			jQuery('#odaolanak').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.odaOlanakAddForm);

		})

	}

	createOdaOlanakAddForm() {
		this.odaOlanakAddForm = this.formBuilder.group({		
			odaOlanakId : [0],
evId : [0, Validators.required],
baslik : ["", Validators.required],
icon : ["", Validators.required],
aciklama : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteOdaOlanak(odaOlanakId:number){
		this.odaOlanakService.deleteOdaOlanak(odaOlanakId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.odaOlanakList=this.odaOlanakList.filter(x=> x.odaOlanakId!=odaOlanakId);
			this.dataSource = new MatTableDataSource(this.odaOlanakList);
			this.configDataTable();
		})
	}

	getOdaOlanakById(odaOlanakId:number){
		this.clearFormGroup(this.odaOlanakAddForm);
		this.odaOlanakService.getOdaOlanakById(odaOlanakId).subscribe(data=>{
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
