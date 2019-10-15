using System.Collections.Generic;
using Geotik.Route.Service.ValueObjects;

namespace Geotik.Route.Service
{
    public interface ITracePointRequestFormatter
    {
        string Format(IEnumerable<TraceRequestPoint> routePoints);
    }
}