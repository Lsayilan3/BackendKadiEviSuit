using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BlogDetail :IEntity
    {
        public int BlogDetailId { get; set; }
        public int BlogId { get; set; }
        public string Tarih { get; set; }
        public string Yer { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Editor { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

    }
}
