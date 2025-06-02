using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Olanaklar :IEntity
    {
        public int OlanaklarId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }
    }
}
