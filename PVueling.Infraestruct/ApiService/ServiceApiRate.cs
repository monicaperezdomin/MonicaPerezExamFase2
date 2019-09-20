using Newtonsoft.Json;
using PVueling.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;

namespace PVueling.Infraestruct.ApiService
{
    public class ServiceApiRate : IServiceApiRate
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public ServiceApiRate(ILogger<ServiceApiRate> logger, IHttpClientFactory httpClientFactory)
        {            
            _logger = logger;
            _httpClientFactory=httpClientFactory;
        }


        public async Task<IEnumerable<Rate>> GetAsync()
        {
            List<Rate> ListRate = new List<Rate>();
            try
            {
               
                var client = _httpClientFactory.CreateClient("httpRates");
                var result = await client.GetStringAsync("");
                ListRate = JsonConvert.DeserializeObject<List<Rate>>(result);
                return (ListRate);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return (null);
        }
    }
}
