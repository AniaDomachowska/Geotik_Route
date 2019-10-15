using System.Collections.Generic;
using Geotik.Route.Service.ValueObjects;

namespace Geotik.Route.Service
{
    public class RouteMatchingRequest
    {
        public string app_id { get; set; }
        public string app_code { get; set; }
        public string routemode { get; set; }
        public string file { get; set; }
        public string filetype;
    }
}