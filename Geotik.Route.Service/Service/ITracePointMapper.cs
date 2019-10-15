using System.Collections.Generic;
using Geotik.Route.Service.Model;
using Geotik.Route.Service.ValueObjects;

namespace Geotik.Route.Service
{
    public interface ITracePointMapper
    {
        IList<RoutePointModel> ApplyTracing(RouteMatchingResult routeMatchingResult,
            IList<RoutePointModel> routePointList);
    }
}