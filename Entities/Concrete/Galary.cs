using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Galary : IEntity
    {
        public int GalaryId { get; set; }
        public int EvId { get; set; }
        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int ResimTipiId { get; set; }
    }
}
