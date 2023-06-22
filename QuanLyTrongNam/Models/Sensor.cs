using System;
using System.Collections.Generic;

namespace QuanLyTrongNam.Models
{
    public partial class Sensor
    {
        public int SensorId { get; set; }
        public string? SensorName { get; set; }
        public string? SensorType { get; set; }
        public int? FarmId { get; set; }

        public virtual Farm? Farm { get; set; }
    }
}
