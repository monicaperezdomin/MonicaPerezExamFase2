using PVueling.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PVueling.Domain.Interface;

using Microsoft.Extensions.Logging;

namespace PVueling.Application
{

    public class DataService:IDataService,IFind
    {
   
        private IEnumerable<Rate> ListRate = null;
        private IEnumerable<Transaction> ListTransac = null;
        private IEnumerable<Transaction> ListResult = null;
        private readonly ILogger _logger;

        IRepository _serviceRepository;
        public DataService(IRepository serviceRepository, ILogger<DataService> logger)
        {
            _serviceRepository = serviceRepository;
            _logger = logger;

    }        
        public async Task <IEnumerable<Rate>> GetRate()
        {                  
            ListRate = await _serviceRepository.GetRate();
            return ListRate;
        }

        public async Task<IEnumerable<Transaction>> GetTransac()
        {
            ListTransac = await _serviceRepository.GetTransac();
            return ListTransac;
        }
     
        public async Task < IEnumerable<Transaction>> GetResultTransact(string sku)
        {
            ListTransac = await GetTransac();
            ListResult = ListTransac.Where(x => x.sku == sku);
            return ListResult;
        }
    }
}
