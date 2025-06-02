using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class EvDil : IEntity
    {
        public int EvDilId { get; set; }
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public int Dil { get; set; }
    }
}
