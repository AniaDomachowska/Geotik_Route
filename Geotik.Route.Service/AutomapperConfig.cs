using AutoMapper;
using Geotik.Route.Repository.Model;
using Geotik.Route.Service.Model;
using Geotik.Route.Service.ValueObjects;

namespace Geotik.Route.Service
{
    public class AutoMapperConfig
    {
        public static IMapper Install()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoutePointModel, TraceRequestPoint>()
                    .ForMember(dest => dest.longitude, conf => conf.MapFrom(src => src.Longitude))
                    .ForMember(dest => dest.latitude, conf => conf.MapFrom(src => src.Latitude))
                    .ForMember(dest => dest.speed, conf => conf.MapFrom(src => src.Speed))
                    .ForMember(dest => dest.timestamp, conf => conf.MapFrom(src => src.TimeDate));
                cfg.CreateMap<RoutePoint, RoutePointModel>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}