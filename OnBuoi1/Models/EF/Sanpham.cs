using System;
using System.Collections.Generic;

namespace OnBuoi1.Models.EF
{
    public partial class Sanpham
    {
        public Sanpham()
        {
            Chitiethoadons = new HashSet<Chitiethoadon>();
        }

        public int Idp { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? Image { get; set; }
        public int? Quantity { get; set; }
        public string? Info { get; set; }

        public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; }
    }
}
