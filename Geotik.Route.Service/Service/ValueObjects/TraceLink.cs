namespace Geotik.Route.Service.ValueObjects
{
    public class TraceLink
    {
        public decimal confidenceValue { get; set; }
        public int linkIdMatched { get; set; }
        public double matchDistance { get; set; }
        public int routeLinkSeqNrMatched { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public double elevation { get; set; }
        public double speedMps { get; set; }
        public double headingDegreeNorthClockwise { get; set; }
        public double latMatched { get; set; }
        public double lonMatched { get; set; }
        public long timestamp { get; set; }
        public double headingMatched { get; set; }
        public double minError { get; set; }
    }
}