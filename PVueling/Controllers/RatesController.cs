using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PVueling.Application;
using PVueling.Domain.Entities;

namespace PVueling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger _logger;

        public RatesController(IDataService dataService, ILogger<RatesController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get()
        {
            IEnumerable<Rate> ListRate = await _dataService.GetRate();
            return JsonConvert.SerializeObject(ListRate); 
        }
    }
}