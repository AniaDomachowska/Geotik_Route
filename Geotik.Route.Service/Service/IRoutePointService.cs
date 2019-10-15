using System.Collections.Generic;
using Geotik.Route.Service.Model;

namespace Geotik.Route.Service
{
    public interface IRoutePointService
    {
        IList<RoutePointModel> Get(RoutePointFilter filter);
    }
}