using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Geotik.Route.Service.Model;
using Geotik.Route.Service.ValueObjects;
using Newtonsoft.Json;

namespace Geotik.Route.Service
{
    public class RouteMatchingAdapter : IRouteMatchingAdapter
    {
        private readonly RouteMatchingConfig routeMatchingConfig;
        private readonly IMapper mapper;
        private readonly ITracePointMapper tracePointMapper;
        private readonly ITracePointRequestFormatter tracePointRequestFormatter;

        public RouteMatchingAdapter(
            RouteMatchingConfig routeMatchingConfig,
            IMapper mapper, 
            ITracePointMapper tracePointMapper,
            ITracePointRequestFormatter tracePointRequestFormatter)
        {
            this.routeMatchingConfig = routeMatchingConfig;
            this.mapper = mapper;
            this.tracePointMapper = tracePointMapper;
            this.tracePointRequestFormatter = tracePointRequestFormatter;
        }

        public async Task<IList<RoutePointModel>> AdjustRoute(
            IList<RoutePointModel> routeList,
            RouteMatchingParams parameters)
        {
            var httpClient = new HttpClient();

            var routePoints = routeList
                .Select(mapper.Map<TraceRequestPoint>)
                .ToList();

            var fileContent = tracePointRequestFormatter.Format(routePoints);
            var content = new StringContent(fileContent);

            var requestUri = PrepareRequestUri(parameters, routeMatchingConfig);
            var tracePoints = await GetRouteMatchingResult(httpClient, requestUri, content);

            return tracePointMapper.ApplyTracing(tracePoints, routeList);
        }

        private static async Task<RouteMatchingResult> GetRouteMatchingResult(
            HttpClient httpClient, 
            Uri requestUri, 
            HttpContent content)
        {
            var response = await httpClient.PostAsync(requestUri, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new RouteMatchingException(
                    $"Route matching client returned wrong result: {response.StatusCode} {responseString}");
            }

            return JsonConvert.DeserializeObject<RouteMatchingResult>(responseString);
        }

        private Uri PrepareRequestUri(RouteMatchingParams parameters, RouteMatchingConfig request)
        {
            var requestQuery = string.Concat(
                $"?app_id={request.AppId}&app_code={request.AppCode}",
                $"&routeMode={parameters.RouteMode.ToString()}",
                "&filetype=CSV");

            return new Uri(string.Concat(
                routeMatchingConfig.MatchRouteUrl, 
                requestQuery));
        }
    }
}