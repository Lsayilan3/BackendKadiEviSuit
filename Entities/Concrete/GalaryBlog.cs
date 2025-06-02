using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class GalaryBlog :IEntity
    {
        public int GalaryBlogId { get; set; }
        public int BlogId { get; set; }
        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
    }
}
