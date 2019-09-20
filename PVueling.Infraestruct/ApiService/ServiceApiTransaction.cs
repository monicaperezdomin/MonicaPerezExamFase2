using Newtonsoft.Json;
using PVueling.Domain.Entities;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace PVueling.Infraestruct.ApiService
{
    public class ServiceApiTransaction: IServiceApiTransaction
    {
      
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        public ServiceApiTransaction(ILogger<ServiceApiTransaction> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Transaction>> GetAsync()
        {
            List<Transaction> ListTransac = new List<Transaction>();
            try
            {
                var client = _httpClientFactory.CreateClient("httpTransactions");
                var result = await client.GetStringAsync("");
                ListTransac = JsonConvert.DeserializeObject<List<Transaction>>(result);
                return ListTransac;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return (null);
        }    
    }
}
