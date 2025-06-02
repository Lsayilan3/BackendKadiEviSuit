import { Component, OnInit,ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { Blog } from '../blog/models/Blog';
import { BlogDetail } from '../blogDetail/models/BlogDetail';
import { BlogService } from '../blog/services/blog.service';
import { BlogDetailService } from '../blogDetail/services/BlogDetail.service';
import { Galary } from '../galary/models/Galary';
import { GalaryBlog } from '../galaryBlog/models/GalaryBlog';
import { GalaryBlogService } from '../galaryBlog/services/GalaryBlog.service';

declare var jQuery: any;

@Component({
  selector: 'app-blogPages',
  templateUrl: './blogPages.component.html',
  styleUrls: ['./blogPages.component.scss']
})
export class BlogPagesComponent implements OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['blogDetailId','blogId','tarih','yer','aciklama','editor','sira','dil','photo','baslik', 'update','delete','file'];

  blogDetails: BlogDetail[] = [];
  blogDetailList:BlogDetail[];
  blogDetail:BlogDetail=new BlogDetail();

  GaleriAddForm: FormGroup;
  Galeris: Galary[] = [];

  galaryBlog: GalaryBlog | null = null; // Diziden tek objeye çevrildi

  

  blogDetailAddForm: FormGroup;
  photoForm: FormGroup;
  blog: Blog = new Blog();
  blogId: number;


  galleryBlog: GalaryBlog = new GalaryBlog();

  blogList:Blog[];

  modalBaslik: string = '';
  updateForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private blogService: BlogService,
    private authService: AuthService,
    private galaryBlogService: GalaryBlogService,
     private blogDetailService:BlogDetailService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, 
   
  ) {}

  ngAfterViewInit(): void {
    this.getBlogDetailById(this.blog.blogId);
    this.blogService.getBlogList().subscribe(data=>this.blogList=data);

}

  ngOnInit() {
    this.getBlogDetailById(this.blog.blogId);
    this.updateForm = this.formBuilder.group({
      GalaryBlogId: [0],
      BlogId: [0],
      Photo: [""],
      baslik: [""],
      aciklama: [""],
      resimTipiId: [0]
    });
    this.getBlogDetailById(this.blog.blogId);
    this.route.params.subscribe((params) => {
      const blogId = params['blogId'];
      this.blogId = +blogId;
      this.getBlogById(blogId);

      this.getBlogDetailById(blogId);
      this.getGalaryBlogById(this.blog.blogId);
    });
    this.createblogDetailAddForm();
  }

  navigateToRotaPages(blogId: number) {
    this.router.navigate(['/blogpages', blogId]);
  }


  getGalaryBlogById(blogId: number) {
    this.galaryBlogService.getGalaryBlogById(blogId).subscribe(
      (galaryBlog: GalaryBlog) => {  // Tek bir obje dönecek
        this.galaryBlog = galaryBlog;
      },
      (error) => {
        console.error('Hata oluştu:', error);
      }
    );
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
		formData.append('blogDetailId', this.photoForm.get('id').value);
		formData.append('file', this.photoForm.get('file').value);		
		// jQuery('#loginphoto').modal('hide');
	

	this.blogDetailService.addFile(formData).subscribe(data=>{
	jQuery('#photoModal').modal('hide');
				this.clearFormGroup(this.photoForm);
				this.getBlogDetailById(this.blog.blogId);
				console.log(data);
				this.alertifyService.success(data);
	})
	}

  getBlogById(blogId: number) {
    this.blogService.getBlogById(blogId).subscribe(
      (blog: Blog) => {
        this.blog = blog;
      },
      (error) => {
        // Hata yönetimi
      }
    );
  }

  
  getBlogDetailById(blogId: number) {
    this.blogService.getBlogDetailById(blogId).subscribe(
      (blogDetails: BlogDetail[]) => {
        this.blogDetails = blogDetails;
        this.dataSource = new MatTableDataSource(blogDetails);
        this.configDataTable();
      },
      (error) => {
        console.error(error);
        this.blogDetails = []; 
      }
    );
  }



	getBlogDetailList() {
		this.blogDetailService.getBlogDetailList().subscribe(data => {
			this.blogDetailList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

  openModall() {
    this.blogDetail = new BlogDetail();
    this.blogDetailAddForm.patchValue({
      blogId: this.blog.blogId
    });
    jQuery('#blogdetail').modal('show');
  }

  save() {
    if (this.blogDetailAddForm.valid) {
      this.blogDetail = { ...this.blogDetailAddForm.value, blogId: this.blog.blogId };
  
      if (this.blogDetail.blogDetailId == 0)
        this.addBlogDetail();
      else
        this.updateBlogDetail();
    }
  }
  

	addBlogDetail(){

		this.blogDetailService.addBlogDetail(this.blogDetail).subscribe(data => {
      this.getBlogDetailById(this.blog.blogId);
			this.blogDetail = new BlogDetail();
			jQuery('#blogdetail').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.blogDetailAddForm);

		})

	}

  updateBlogDetail() {
    this.blogDetailService.updateBlogDetail(this.blogDetail).subscribe(
      (data) => {
        const index = this.blogDetails.findIndex((x) => x.blogDetailId === this.blogDetail.blogDetailId);
        this.blogDetails[index] = { ...this.blogDetail }; // Güncellenen blogDetail nesnesini dizideki ilgili örnekle değiştirin
        this.dataSource = new MatTableDataSource(this.blogDetails);
        this.configDataTable();
        this.blogDetail = new BlogDetail();
        jQuery('#blogdetail').modal('hide');
        this.alertifyService.success(data);
        this.clearFormGroup(this.blogDetailAddForm);
        this.getBlogDetailById(this.blog.blogId);
      },
      
      (error) => {
        console.error('Blog detayı güncelleme hatası:', error);
      }
    );
  }
  

	createblogDetailAddForm() {
		this.blogDetailAddForm = this.formBuilder.group({		
			blogDetailId : [0],
      blogId : [0],
      tarih : [""],
      yer : [""],
      baslik : [""],
      aciklama : [""],
      editor : [""],
      sira : [""],
      dil : [0],
		})
	}

  deleteBlogDetail(blogDetailId: number) {
    if (confirm('Blog detayını silmek istediğinize emin misiniz?')) {
      this.blogDetailService.deleteBlogDetail(blogDetailId).subscribe(
        (data) => {
          this.alertifyService.success(data.toString());
          this.blogDetails = this.blogDetails.filter((x) => x.blogDetailId !== blogDetailId);
          this.dataSource = new MatTableDataSource(this.blogDetails);
          this.configDataTable();
        },
        (error) => {
          console.error('Blog detayı silme hatası:', error);
        }
      );
    }
  }
  
  //Burası farklı
	getBlogDetailiById(blogDetailId:number){
		this.clearFormGroup(this.blogDetailAddForm);
		this.blogDetailService.getBlogDetailiById(blogDetailId).subscribe(data=>{
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



  // Blog GALERİ 


}
