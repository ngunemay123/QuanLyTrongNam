using System;
using System.Collections.Generic;

namespace QuanLyTrongNam.Models
{
    public partial class Mushroom
    {
        public int MushroomId { get; set; }
        public string? MushroomName { get; set; }
        public string? MushroomDescription { get; set; }
        public decimal? MushroomPrice { get; set; }
        public string? MushroomPicture { get; set; }
        public int? FarmId { get; set; }

        public virtual Farm? Farm { get; set; }
    }
}
