using System.Collections.Generic;
using System.Threading.Tasks;
using Geotik.Route.Service.Model;
using Geotik.Route.Service.ValueObjects;

namespace Geotik.Route.Service
{
    public interface IRouteMatchingAdapter
    {
        Task<IList<RoutePointModel>> AdjustRoute(
            IList<RoutePointModel> routeList,
            RouteMatchingParams parameters);
    }
}