using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class EvDetail :IEntity
    {
        public int EvDetailId { get; set; }
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string P { get; set; }
        public string CocukBaslik { get; set; }
        public string CocukP { get; set; }
        public string Editor { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

    }
}
