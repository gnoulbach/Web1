using System;
using System.Collections.Generic;

namespace OnBuoi1.Models.EF
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public int Idc { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
