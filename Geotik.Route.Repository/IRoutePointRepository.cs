using System.Collections.Generic;
using Geotik.Route.Repository.Model;

namespace Geotik.Route.Repository
{
    public interface IRoutePointRepository
    {
        IList<RoutePoint> Read(int unitCode);
    }
}