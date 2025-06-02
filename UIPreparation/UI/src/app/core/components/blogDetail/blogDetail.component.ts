import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { BlogDetail } from './models/BlogDetail';
import { environment } from 'environments/environment';
import { Blog } from '../blog/models/Blog';
import { BlogService } from '../blog/services/blog.service';
import { BlogDetailService } from './services/BlogDetail.service';

declare var jQuery: any;

@Component({
	selector: 'app-blogDetail',
	templateUrl: './blogDetail.component.html',
	styleUrls: ['./blogDetail.component.scss']
})
export class BlogDetailComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['blogDetailId','blogId','tarih','yer','baslik','aciklama','editor','sira','dil', 'update','delete'];

	blogDetailList:BlogDetail[];
	blogDetail:BlogDetail=new BlogDetail();

	blogDetailAddForm: FormGroup;

	blogList:Blog[];
	blogDetailId:number;

	constructor(private blogService: BlogService,private blogDetailService:BlogDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
		this.blogService.getBlogList().subscribe(data=>this.blogList=data);
        this.getBlogDetailList();
    }

	ngOnInit() {
		this.blogService.getBlogList().subscribe(data=>this.blogList=data);
		this.createBlogDetailAddForm();
	}


	getBlogDetailList() {
		this.blogDetailService.getBlogDetailList().subscribe(data => {
			this.blogDetailList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.blogDetailAddForm.valid) {
			this.blogDetail = Object.assign({}, this.blogDetailAddForm.value)

			if (this.blogDetail.blogDetailId == 0)
				this.addBlogDetail();
			else
				this.updateBlogDetail();
		}

	}

	addBlogDetail(){

		this.blogDetailService.addBlogDetail(this.blogDetail).subscribe(data => {
			this.getBlogDetailList();
			this.blogDetail = new BlogDetail();
			jQuery('#blogdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.blogDetailAddForm);

		})

	}

	updateBlogDetail(){

		this.blogDetailService.updateBlogDetail(this.blogDetail).subscribe(data => {

			var index=this.blogDetailList.findIndex(x=>x.blogDetailId==this.blogDetail.blogDetailId);
			this.blogDetailList[index]=this.blogDetail;
			this.dataSource = new MatTableDataSource(this.blogDetailList);
            this.configDataTable();
			this.blogDetail = new BlogDetail();
			jQuery('#blogdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.blogDetailAddForm);

		})

	}

	createBlogDetailAddForm() {
		this.blogDetailAddForm = this.formBuilder.group({		
			blogDetailId : [0],
blogId : [0, Validators.required],
tarih : ["", Validators.required],
yer : ["", Validators.required],
baslik : ["", Validators.required],
aciklama : ["", Validators.required],
editor : ["", Validators.required],
sira : [0, Validators.required],
dil : [0, Validators.required]
		})
	}

	deleteBlogDetail(blogDetailId:number){
		this.blogDetailService.deleteBlogDetail(blogDetailId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.blogDetailList=this.blogDetailList.filter(x=> x.blogDetailId!=blogDetailId);
			this.dataSource = new MatTableDataSource(this.blogDetailList);
			this.configDataTable();
		})
	}

	getBlogDetailById(blogDetailId:number){
		this.clearFormGroup(this.blogDetailAddForm);
		this.blogDetailService.getBlogDetailById(blogDetailId).subscribe(data=>{
			this.blogDetail=data;
			this.blogDetailAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'blogDetailId')
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
