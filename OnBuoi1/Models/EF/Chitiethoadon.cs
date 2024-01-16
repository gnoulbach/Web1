using System;
using System.Collections.Generic;

namespace OnBuoi1.Models.EF
{
    public partial class Chitiethoadon
    {
        public int Idct { get; set; }
        public int? Idp { get; set; }
        public int? Idh { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }

        public virtual Hoadon? IdhNavigation { get; set; }
        public virtual Sanpham? IdpNavigation { get; set; }
    }
}
