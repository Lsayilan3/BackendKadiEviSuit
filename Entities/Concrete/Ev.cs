using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Ev :IEntity
    {
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string Url { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }
    }
}
