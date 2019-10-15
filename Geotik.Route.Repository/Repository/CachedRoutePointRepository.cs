using System.Collections.Concurrent;
using System.Collections.Generic;
using Geotik.Route.Repository.Model;

namespace Geotik.Route.Repository
{
    //public class CachedRoutePointRepository : IRoutePointRepository
    //{
    //    private readonly IRoutePointRepository innerRepository;
    //    private readonly IDictionary<int, IList<RoutePoint>> routePoints;


    //    public CachedRoutePointRepository(IRoutePointRepository innerRepository)
    //    {
    //        this.innerRepository = innerRepository;
    //        this.routePoints = new ConcurrentDictionary<int, IList<RoutePoint>>();
    //    }

    //    public IList<RoutePoint> Read(RoutePointFilter filter)
    //    {
    //        if (routePoints.ContainsKey(filter.UnitCode))
    //        {
    //            return routePoints[filter.UnitCode];
    //        }

    //        return (this.routePoints[filter.UnitCode] = innerRepository.Read(filter));
    //    }
    //}
}