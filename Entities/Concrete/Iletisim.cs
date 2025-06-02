using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Iletisim : IEntity
    {
        public int IletisimId { get; set; }
        public string Isim { get; set; }
        public string SoyIsim { get; set; }
        public string Mail { get; set; }
        public string Soru { get; set; }
        public string Mesaj { get; set; }
        public DateTime CraeteDate { get; set; } = DateTime.UtcNow;

    }
}
