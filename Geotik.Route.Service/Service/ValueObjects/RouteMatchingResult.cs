using System.Collections;
using System.Collections.Generic;

namespace Geotik.Route.Service.ValueObjects
{
    public class RouteMatchingResult
    {
        public IList<RouteLink> RouteLinks { get; set; }
        public IList<TraceLink> TracePoints { get; set; }
    }
}