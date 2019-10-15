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

            var request = new RouteMatchingRequest
            {
                app_id = routeMatchingConfig.AppId,
                app_code = routeMatchingConfig.AppCode,
                routemode = parameters.RouteMode.ToString(),
                filetype = "CSV"
            };

            var fileContent = tracePointRequestFormatter.Format(routePoints);
            var content = new StringContent(fileContent);

            var requestUri = PrepareRequestUri(parameters, request);
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
                throw new RouteMatchingException(string.Format("Route matching client returned wrong result: {0} {1}",
                    response.StatusCode,
                    responseString));
            }
            
            var result = JsonConvert.DeserializeObject<RouteMatchingResult>(responseString);
            return result;
        }

        private Uri PrepareRequestUri(RouteMatchingParams parameters, RouteMatchingRequest request)
        {
            var requestQuery = $"?app_id={request.app_id}&app_code={request.app_code}" +
                               $"&routeMode={parameters.RouteMode.ToString()}" +
                               "&filetype=CSV";

            var requestUri = new Uri(string.Concat(routeMatchingConfig.MatchRouteUrl, requestQuery));
            return requestUri;
        }
    }

    public class RouteMatchingException : Exception
    {
        public RouteMatchingException(string message) : base(message)
        {
        }
    }
}