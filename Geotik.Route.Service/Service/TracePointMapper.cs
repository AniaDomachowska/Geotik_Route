using System;
using System.Collections.Generic;
using System.Linq;
using Geotik.Route.Service.Model;
using Geotik.Route.Service.ValueObjects;

namespace Geotik.Route.Service
{
    public class TracePointMapper : ITracePointMapper
    {
        public IList<RoutePointModel> ApplyTracing(
            RouteMatchingResult routeMatchingResult,
            IList<RoutePointModel> routePointList)
        {
            foreach (var routePointModel in routePointList)
            {
                var tracedPoint = routeMatchingResult
                    .TracePoints
                    .FirstOrDefault(element =>
                        Math.Abs(element.lat - routePointModel.Latitude) < 0.000000001 && 
                        Math.Abs(element.lon - routePointModel.Longitude) < 0.000000001);

                if (tracedPoint != null)
                {
                    routePointModel.Latitude = tracedPoint.latMatched;
                    routePointModel.Longitude = tracedPoint.lonMatched;
                }
            }

            return routePointList;
        }
    }
}