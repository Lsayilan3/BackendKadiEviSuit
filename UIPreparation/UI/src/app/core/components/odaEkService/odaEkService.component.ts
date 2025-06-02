import { EvService } from './../ev/services/ev.service';
import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { OdaEkService } from './models/OdaEkService';
import { OdaEkServiceService } from './services/OdaEkService.service';
import { environment } from 'environments/environment';
import { Ev } from '../ev/models/Ev';

declare var jQuery: any;

@Component({
	selector: 'app-odaEkService',
	templateUrl: './odaEkService.component.html',
	styleUrls: ['./odaEkService.component.scss']
})
export class OdaEkServiceComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['odaEkServiceId','evId','baslik','icon','aciklama','sira','dil', 'update','delete'];

	odaEkServiceList:OdaEkService[];
	odaEkService:OdaEkService=new OdaEkService();

	odaEkServiceAddForm: FormGroup;

	evList:Ev[];


	odaEkServiceId:number;

	constructor(private evService:EvService, private odaEkServiceService:OdaEkServiceService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.evService.getEvList().subscribe(data=>this.evList=data);
        this.getOdaEkServiceList();
    }

	ngOnInit() {

		this.createOdaEkServiceAddForm();
	}


	getOdaEkServiceList() {
		this.odaEkServiceService.getOdaEkServiceList().subscribe(data => {
			this.odaEkServiceList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.odaEkServiceAddForm.valid) {
			this.odaEkService = Object.assign({}, this.odaEkServiceAddForm.value)

			if (this.odaEkService.odaEkServiceId == 0)
				this.addOdaEkService();
			else
				this.updateOdaEkService();
		}

	}

	addOdaEkService(){

		this.odaEkServiceService.addOdaEkService(this.odaEkService).subscribe(data => {
			this.getOdaEkServiceList();
			this.odaEkService = new OdaEkService();
			jQuery('#odaekservice').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.odaEkServiceAddForm);

		})

	}

	updateOdaEkService(){

		this.odaEkServiceService.updateOdaEkService(this.odaEkService).subscribe(data => {

			var index=this.odaEkServiceList.findIndex(x=>x.odaEkServiceId==this.odaEkService.odaEkServiceId);
			this.odaEkServiceList[index]=this.odaEkService;
			this.dataSource = new MatTableDataSource(this.odaEkServiceList);
            this.configDataTable();
			this.odaEkService = new OdaEkService();
			jQuery('#odaekservice').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.odaEkServiceAddForm);

		})

	}

	createOdaEkServiceAddForm() {
		this.odaEkServiceAddForm = this.formBuilder.group({		
			odaEkServiceId : [0],
evId : [0, Validators.required],
baslik : ["", Validators.required],
icon : ["", Validators.required],
aciklama : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteOdaEkService(odaEkServiceId:number){
		this.odaEkServiceService.deleteOdaEkService(odaEkServiceId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.odaEkServiceList=this.odaEkServiceList.filter(x=> x.odaEkServiceId!=odaEkServiceId);
			this.dataSource = new MatTableDataSource(this.odaEkServiceList);
			this.configDataTable();
		})
	}

	getOdaEkServiceById(odaEkServiceId:number){
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
