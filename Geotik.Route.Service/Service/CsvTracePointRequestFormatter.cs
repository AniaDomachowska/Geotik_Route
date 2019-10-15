using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Geotik.Route.Service.ValueObjects;

namespace Geotik.Route.Service
{
    public class CsvTracePointRequestFormatter : ITracePointRequestFormatter
    {
        public string Format(IEnumerable<TraceRequestPoint> routePoints)
        {
            var outputStream = new MemoryStream();

            using (var writer = new StreamWriter(outputStream))
            using (var csv = new CsvWriter(writer, new Configuration() {Delimiter = ",", ShouldQuote = (s, context) => false}))
            {
                var propertyMap = csv.Configuration.AutoMap<TraceRequestPoint>();
                propertyMap.Map(m => m.timestamp)
                    .TypeConverterOption.Format("s");
                csv.Configuration.CultureInfo = CultureInfo.InvariantCulture;
                
                csv.WriteRecords(routePoints);
            }

            return Encoding.UTF8.GetString(outputStream.GetBuffer());
        }
    }
}