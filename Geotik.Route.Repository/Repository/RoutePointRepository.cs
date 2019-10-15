using System.Collections.Generic;
using System.IO;
using Geotik.Route.Repository.Model;
using Newtonsoft.Json;

namespace Geotik.Route.Repository
{
    public class RoutePointRepository : IRoutePointRepository
    {
        private readonly string filePath;

        public RoutePointRepository(string filePath)
        {
            this.filePath = filePath;
        }

        public IList<RoutePoint> Read(int unitCode)
        {
            var fileName = $"{unitCode}_Points.json";
            var fileContent = File.ReadAllText(Path.Combine(filePath, fileName));
            var points = JsonConvert.DeserializeObject<List<RoutePoint>>(fileContent);

            return points;
        }
    }
}