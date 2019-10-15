using System;

namespace Geotik.Route.Repository.Model
{
    public class RoutePoint
    {
        public int UnitId { get; set; }
        public DateTime TimeDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Ignition { get; set; }
        public bool Engine { get; set; }
        public double Speed { get; set; }
        public bool PositionError { get; set; }
        public int Rpm { get; set; }
        public int Direction { get; set; }
        public int Distance { get; set; }
    }
}
