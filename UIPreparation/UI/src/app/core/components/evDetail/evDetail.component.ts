import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { EvDetail } from './models/EvDetail';
import { EvDetailService } from './services/EvDetail.service';
import { environment } from 'environments/environment';
import { EvService } from '../ev/services/ev.service';
import { Ev } from '../ev/models/Ev';

declare var jQuery: any;

@Component({
	selector: 'app-evDetail',
	templateUrl: './evDetail.component.html',
	styleUrls: ['./evDetail.component.scss']
})
export class EvDetailComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['evDetailId','evId','baslik','p','cocukBaslik','cocukP','editor','sira','dil', 'update','delete'];

	evDetailList:EvDetail[];
	evDetail:EvDetail=new EvDetail();

	evDetailAddForm: FormGroup;

	evList:Ev[];
	evDetailId:number;

	constructor(private evService:EvService, private evDetailService:EvDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.evService.getEvList().subscribe(data=>this.evList=data);
        this.getEvDetailList();
    }

	ngOnInit() {

		this.createEvDetailAddForm();
	}


	getEvDetailList() {
		this.evDetailService.getEvDetailList().subscribe(data => {
			this.evDetailList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.evDetailAddForm.valid) {
			this.evDetail = Object.assign({}, this.evDetailAddForm.value)

			if (this.evDetail.evDetailId == 0)
				this.addEvDetail();
			else
				this.updateEvDetail();
		}

	}

	addEvDetail(){

		this.evDetailService.addEvDetail(this.evDetail).subscribe(data => {
			this.getEvDetailList();
			this.evDetail = new EvDetail();
			jQuery('#evdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.evDetailAddForm);

		})

	}

	updateEvDetail(){

		this.evDetailService.updateEvDetail(this.evDetail).subscribe(data => {

			var index=this.evDetailList.findIndex(x=>x.evDetailId==this.evDetail.evDetailId);
			this.evDetailList[index]=this.evDetail;
			this.dataSource = new MatTableDataSource(this.evDetailList);
            this.configDataTable();
			this.evDetail = new EvDetail();
			jQuery('#evdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.evDetailAddForm);

		})

	}

	createEvDetailAddForm() {
		this.evDetailAddForm = this.formBuilder.group({		
			evDetailId : [0],
evId : [0, Validators.required],
baslik : ["", Validators.required],
p : ["", Validators.required],
cocukBaslik : ["", Validators.required],
cocukP : ["", Validators.required],
editor : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteEvDetail(evDetailId:number){
		this.evDetailService.deleteEvDetail(evDetailId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.evDetailList=this.evDetailList.filter(x=> x.evDetailId!=evDetailId);
			this.dataSource = new MatTableDataSource(this.evDetailList);
			this.configDataTable();
		})
	}

	getEvDetailById(evDetailId:number){
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
			if (key == 'evDetailId')
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
