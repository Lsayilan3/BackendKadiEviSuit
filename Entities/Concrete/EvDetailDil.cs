using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class EvDetailDil : IEntity
    {
        public int EvDetailDilId { get; set; }
        public int EvDetailId { get; set; }
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string P { get; set; }
        public string CocukBaslik { get; set; }
        public string CocukP { get; set; }
        public string Editor { get; set; }
        public int Dil { get; set; }
    }
}
