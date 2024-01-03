using System;
using System.Collections.Generic;

namespace OnBuoi1.Models.EF
{
    public partial class Sanpham
    {
        public int Idp { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public string? Image { get; set; }
        public int? Quantity { get; set; }
    }
}
