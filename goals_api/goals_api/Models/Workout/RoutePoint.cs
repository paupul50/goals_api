using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace goals_api.Models
{
    public class RoutePoint
    {
        [Key]
        public int Id { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public double Radius { get; set; }
        public string FillColour { get; set; }
        public bool CircleDraggable { get; set; }
        public bool Editable { get; set; }
        public int Index { get; set; }
        public Workout Workout { get; set; }
    }
}
