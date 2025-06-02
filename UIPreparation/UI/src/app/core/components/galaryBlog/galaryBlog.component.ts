import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { GalaryBlog } from './models/GalaryBlog';
import { GalaryBlogService } from './services/GalaryBlog.service';
import { environment } from 'environments/environment';
import { BlogService } from '../blog/services/blog.service';
import { Blog } from '../blog/models/Blog';

declare var jQuery: any;

@Component({
	selector: 'app-galaryBlog',
	templateUrl: './galaryBlog.component.html',
	styleUrls: ['./galaryBlog.component.scss']
})
export class GalaryBlogComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['galaryBlogId','blogId','photo','baslik','aciklama', 'update','delete','file'];

	galaryBlogList:GalaryBlog[];
	galaryBlog:GalaryBlog=new GalaryBlog();

	galaryBlogAddForm: FormGroup;
	photoForm: FormGroup;

	blogList:Blog[];
	galaryBlogId:number;

	constructor(private blogService: BlogService, private galaryBlogService:GalaryBlogService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.blogService.getBlogList().subscribe(data=>this.blogList=data);
        this.getGalaryBlogList();
    }

	ngOnInit() {
		this.blogService.getBlogList().subscribe(data=>this.blogList=data);
		this.createGalaryBlogAddForm();
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
			formData.append('galaryBlogId', this.photoForm.get('id').value);
			formData.append('file', this.photoForm.get('file').value);		
			// jQuery('#loginphoto').modal('hide');
		
	
		this.galaryBlogService.addFile(formData).subscribe(data=>{
		jQuery('#photoModal').modal('hide');
					this.clearFormGroup(this.photoForm);
					this.getGalaryBlogList();
					console.log(data);
					this.alertifyService.success(data);
		})
		}


	getGalaryBlogList() {
		this.galaryBlogService.getGalaryBlogList().subscribe(data => {
			this.galaryBlogList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.galaryBlogAddForm.valid) {
			this.galaryBlog = Object.assign({}, this.galaryBlogAddForm.value)

			if (this.galaryBlog.galaryBlogId == 0)
				this.addGalaryBlog();
			else
				this.updateGalaryBlog();
		}

	}

	addGalaryBlog(){

		this.galaryBlogService.addGalaryBlog(this.galaryBlog).subscribe(data => {
			this.getGalaryBlogList();
			this.galaryBlog = new GalaryBlog();
			jQuery('#galaryblog').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.galaryBlogAddForm);

		})

	}

	updateGalaryBlog(){

		this.galaryBlogService.updateGalaryBlog(this.galaryBlog).subscribe(data => {

			var index=this.galaryBlogList.findIndex(x=>x.galaryBlogId==this.galaryBlog.galaryBlogId);
			this.galaryBlogList[index]=this.galaryBlog;
			this.dataSource = new MatTableDataSource(this.galaryBlogList);
            this.configDataTable();
			this.galaryBlog = new GalaryBlog();
			jQuery('#galaryblog').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.galaryBlogAddForm);

		})

	}

	createGalaryBlogAddForm() {
		this.galaryBlogAddForm = this.formBuilder.group({		
			galaryBlogId : [0],
blogId : [0, Validators.required],
photo : ["", Validators.required],
baslik : ["", Validators.required],
aciklama : ["", Validators.required]
		})
	}

	deleteGalaryBlog(galaryBlogId:number){
		this.galaryBlogService.deleteGalaryBlog(galaryBlogId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.galaryBlogList=this.galaryBlogList.filter(x=> x.galaryBlogId!=galaryBlogId);
			this.dataSource = new MatTableDataSource(this.galaryBlogList);
			this.configDataTable();
		})
	}

	getGalaryBlogById(galaryBlogId:number){
		this.clearFormGroup(this.galaryBlogAddForm);
		this.galaryBlogService.getGalaryBlogById(galaryBlogId).subscribe(data=>{
			this.galaryBlog=data;
			this.galaryBlogAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'galaryBlogId')
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
