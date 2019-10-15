using System;

namespace Geotik.Route.Service
{
    public class RouteMatchingException : Exception
    {
        public RouteMatchingException(string message) : base(message)
        {
        }
    }
}