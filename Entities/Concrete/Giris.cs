using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Giris : IEntity
    {
        public int GirisId { get; set; }
        public string Baslik { get; set; }
        public string PBir { get; set; }
        public string PIki { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }
    }
}
