using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OdaOlanak :IEntity
    {
        public int OdaOlanakId { get; set; }
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string Icon { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


    }
}
