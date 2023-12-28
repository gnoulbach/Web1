using System;
using System.Collections.Generic;

namespace OnBuoi1.Models.EF
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Address { get; set; }
    }
}
