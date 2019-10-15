using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Geotik.Route.Repository;
using Geotik.Route.Service.Model;

namespace Geotik.Route.Service
{
    public class RoutePointService : IRoutePointService
    {
        private readonly IMapper mapper;
        private readonly IRoutePointRepository repository;

        public RoutePointService(IRoutePointRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public IList<RoutePointModel> Get(RoutePointFilter filter)
        {
            return repository
                .Read(filter.UnitCode)
                .Select(element => mapper.Map<RoutePointModel>(element))
                .ToList();
        }
    }
}