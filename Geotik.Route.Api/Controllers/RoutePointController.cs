using System.Collections.Generic;
using System.Threading.Tasks;
using Geotik.Route.Service;
using Geotik.Route.Service.Model;
using Geotik.Route.Service.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Geotik.Route.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutePointController : ControllerBase
    {
        private readonly IRoutePointService routePointService;
        private readonly IRouteMatchingAdapter routeMatchingAdapter;

        public RoutePointController(
            IRoutePointService routePointService, 
            IRouteMatchingAdapter routeMatchingAdapter)
        {
            this.routePointService = routePointService;
            this.routeMatchingAdapter = routeMatchingAdapter;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoutePointModel>>> Get(RoutePointFilter filter)
        {
            if (filter == null)
            {
                return BadRequest("Filter not provided");
            }
            
            var routePointModels = routePointService.Get(filter);

            if (filter.AdjustToNearestRoute)
            {
                await routeMatchingAdapter.AdjustRoute(routePointModels, new RouteMatchingParams()
                {
                    RouteMode = RouteModeEnum.Car
                });
            }
            return new ObjectResult(routePointModels);
        }
    }
}