using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Anasayfa :IEntity
    {
        public int AnasayfaId { get; set; }
        public string Aciklama { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }
    }
}
