using System;
using System.Collections.Generic;

namespace OnBuoi1.Models.EF
{
    public partial class Hoadon
    {
        public int Idh { get; set; }
        public int? Id { get; set; }
        public int? Idc { get; set; }
        public int? Total { get; set; }
        public DateTime? Date { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}
