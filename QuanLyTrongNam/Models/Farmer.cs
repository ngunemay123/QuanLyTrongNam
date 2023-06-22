using System;
using System.Collections.Generic;

namespace QuanLyTrongNam.Models
{
    public partial class Farmer
    {
        public int FarmerId { get; set; }
        public string? FarmerName { get; set; }
        public string? FarmerAddress { get; set; }
        public string? FarmerPhone { get; set; }
        public string? FarmerPicture { get; set; }
        public int? FarmId { get; set; }

        public virtual Farm? Farm { get; set; }
    }
}
