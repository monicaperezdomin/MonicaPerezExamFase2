using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PVueling.Application;
using PVueling.Domain.Entities;

namespace PVueling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IFind _dataFind;
        private readonly ILogger _logger;

        public TransactionsController(IDataService dataService, IFind dataFind, ILogger<TransactionsController> logger)
        {
            _dataFind = dataFind;
            _dataService = dataService;
            _logger = logger;
        }


    [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            IEnumerable<Transaction> ListRate = await _dataService.GetTransac();
            return JsonConvert.SerializeObject(ListRate);
        }

     [HttpGet("{sku}")]
        public async Task<ActionResult<object>> Get(string id) { 
      
            IEnumerable<Transaction> ListRate = await _dataFind.GetResultTransact(id);
            return JsonConvert.SerializeObject(ListRate);
        }
        
    }
}