import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Service } from './models/Service';
import { ServiceService } from './services/Service.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-service',
	templateUrl: './service.component.html',
	styleUrls: ['./service.component.scss']
})
export class ServiceComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['serviceId','photo', 'update','delete','file'];

	serviceList:Service[];
	service:Service=new Service();

	serviceAddForm: FormGroup;
	photoForm: FormGroup;


	serviceId:number;

	constructor(private serviceService:ServiceService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getServiceList();
    }

	ngOnInit() {

		this.createServiceAddForm();
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
			formData.append('serviceId', this.photoForm.get('id').value);
			formData.append('file', this.photoForm.get('file').value);		
			// jQuery('#loginphoto').modal('hide');
		
	
		this.serviceService.addFile(formData).subscribe(data=>{
		jQuery('#photoModal').modal('hide');
					this.clearFormGroup(this.photoForm);
					this.getServiceList();
					console.log(data);
					this.alertifyService.success(data);
		})
		}


	getServiceList() {
		this.serviceService.getServiceList().subscribe(data => {
			this.serviceList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.serviceAddForm.valid) {
			this.service = Object.assign({}, this.serviceAddForm.value)

			if (this.service.serviceId == 0)
				this.addService();
			else
				this.updateService();
		}

	}

	addService(){

		this.serviceService.addService(this.service).subscribe(data => {
			this.getServiceList();
			this.service = new Service();
			jQuery('#service').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.serviceAddForm);

		})

	}

	updateService(){

		this.serviceService.updateService(this.service).subscribe(data => {

			var index=this.serviceList.findIndex(x=>x.serviceId==this.service.serviceId);
			this.serviceList[index]=this.service;
			this.dataSource = new MatTableDataSource(this.serviceList);
            this.configDataTable();
			this.service = new Service();
			jQuery('#service').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.serviceAddForm);

		})

	}

	createServiceAddForm() {
		this.serviceAddForm = this.formBuilder.group({		
			serviceId : [0],
photo : ["", Validators.required]
		})
	}

	deleteService(serviceId:number){
		this.serviceService.deleteService(serviceId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.serviceList=this.serviceList.filter(x=> x.serviceId!=serviceId);
			this.dataSource = new MatTableDataSource(this.serviceList);
			this.configDataTable();
		})
	}

	getServiceById(serviceId:number){
		this.clearFormGroup(this.serviceAddForm);
		this.serviceService.getServiceById(serviceId).subscribe(data=>{
			this.service=data;
			this.serviceAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'serviceId')
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
