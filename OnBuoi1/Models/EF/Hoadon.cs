using System;
using System.Collections.Generic;

namespace OnBuoi1.Models.EF
{
    public partial class Hoadon
    {
        public Hoadon()
        {
            Chitiethoadons = new HashSet<Chitiethoadon>();
        }

        public int Idh { get; set; }
        public int? Id { get; set; }
        public int? Idc { get; set; }
        public int? Total { get; set; }
        public DateTime? Date { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? Status { get; set; }

        public virtual Khachhang? IdcNavigation { get; set; }
        public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; }
    }
}
