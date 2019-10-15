using System;

namespace Geotik.Route.Service.ValueObjects
{
    public class TraceRequestPoint
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public DateTime timestamp { get; set; }
        public double speed_mps { get; set; }
        public double speed_mph { get; set; }
        public double speed_kmh { get; set; }
        public double speed { get; set; }
        public double heading { get; set; }
    }
}