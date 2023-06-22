using System;
using System.Collections.Generic;

namespace QuanLyTrongNam.Models
{
    public partial class Farm
    {
        public Farm()
        {
            Farmers = new HashSet<Farmer>();
            Mushrooms = new HashSet<Mushroom>();
            Sensors = new HashSet<Sensor>();
        }

        public int FarmId { get; set; }
        public string? FarmName { get; set; }
        public string? FarmLocation { get; set; }
        public string? FarmPicture { get; set; }

        public virtual ICollection<Farmer> Farmers { get; set; }
        public virtual ICollection<Mushroom> Mushrooms { get; set; }
        public virtual ICollection<Sensor> Sensors { get; set; }
    }
}
