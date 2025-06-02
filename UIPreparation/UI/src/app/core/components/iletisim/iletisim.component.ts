import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Iletisim } from './models/Iletisim';
import { IletisimService } from './services/Iletisim.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-iletisim',
	templateUrl: './iletisim.component.html',
	styleUrls: ['./iletisim.component.scss']
})
export class IletisimComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['iletisimId','isim','soyIsim','mail','soru','mesaj','craeteDate', 'update','delete'];

	iletisimList:Iletisim[];
	iletisim:Iletisim=new Iletisim();

	iletisimAddForm: FormGroup;


	iletisimId:number;

	constructor(private iletisimService:IletisimService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getIletisimList();
    }

	ngOnInit() {

		this.createIletisimAddForm();
	}


	getIletisimList() {
		this.iletisimService.getIletisimList().subscribe(data => {
			this.iletisimList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.iletisimAddForm.valid) {
			this.iletisim = Object.assign({}, this.iletisimAddForm.value)

			if (this.iletisim.iletisimId == 0)
				this.addIletisim();
			else
				this.updateIletisim();
		}

	}

	addIletisim(){

		this.iletisimService.addIletisim(this.iletisim).subscribe(data => {
			this.getIletisimList();
			this.iletisim = new Iletisim();
			jQuery('#iletisim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.iletisimAddForm);

		})

	}

	updateIletisim(){

		this.iletisimService.updateIletisim(this.iletisim).subscribe(data => {

			var index=this.iletisimList.findIndex(x=>x.iletisimId==this.iletisim.iletisimId);
			this.iletisimList[index]=this.iletisim;
			this.dataSource = new MatTableDataSource(this.iletisimList);
            this.configDataTable();
			this.iletisim = new Iletisim();
			jQuery('#iletisim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.iletisimAddForm);

		})

	}

	createIletisimAddForm() {
		this.iletisimAddForm = this.formBuilder.group({		
			iletisimId : [0],
isim : ["", Validators.required],
soyIsim : ["", Validators.required],
mail : ["", Validators.required],
soru : ["", Validators.required],
mesaj : ["", Validators.required],
craeteDate : [null, Validators.required]
		})
	}

	deleteIletisim(iletisimId:number){
		this.iletisimService.deleteIletisim(iletisimId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.iletisimList=this.iletisimList.filter(x=> x.iletisimId!=iletisimId);
			this.dataSource = new MatTableDataSource(this.iletisimList);
			this.configDataTable();
		})
	}

	getIletisimById(iletisimId:number){
		this.clearFormGroup(this.iletisimAddForm);
		this.iletisimService.getIletisimById(iletisimId).subscribe(data=>{
			this.iletisim=data;
			this.iletisimAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'iletisimId')
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
