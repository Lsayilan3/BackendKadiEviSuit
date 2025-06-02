using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Service :IEntity
    {
        public int ServiceId { get; set; }
        public string Photo { get; set; }
    }
}
