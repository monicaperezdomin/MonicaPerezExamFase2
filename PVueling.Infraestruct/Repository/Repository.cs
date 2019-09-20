using PVueling.Domain.Entities;
using PVueling.Domain.Interface;
using PVueling.Infraestruct.ApiService;
using PVueling.Infraestruct.RepositoryDB;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using PVueling.Infraestruct.Data;

namespace PVueling.Infraestruct.Repository
{

   public class Repository:IRepository
    {
        IServiceApiRate _serviceRate;
        IServiceApiTransaction _serviceTransac;
        IGenericRepository<Rate> _serviceDbR;
       MyDbContextRateTransac _mydbContextDB;
        private readonly ILogger _logger;

        public Repository(IServiceApiRate serviceRate, IServiceApiTransaction serviceTransac,ILogger<Repository> logger, MyDbContextRateTransac mydbContextDB, IGenericRepository<Rate> _serviceDbRate, IGenericRepository<Transaction> _serviceDbTransac)
        {
            _logger = logger;       
            _serviceRate = serviceRate;
            _serviceTransac = serviceTransac;
            _serviceRate = serviceRate;
            _mydbContextDB = mydbContextDB;

        }
        public IEnumerable<Rate> ListRate = null;
        public IEnumerable<Rate> ResultT = null;
        public IEnumerable<Transaction> ListTransac = null;
      
        public async Task<IEnumerable<Rate>> GetRate()
        {
            try
            {
                ListRate = await _serviceRate.GetAsync();
                if (ListRate!=null){
                 
                    await _mydbContextDB.AddRangeAsync(ListRate);
                    _mydbContextDB.SaveChanges();
                   
                }
             
                return ListRate;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return null;

            }
          

        
        }
        public async Task<IEnumerable<Transaction>> GetTransac()
        {
            ListTransac = await _serviceTransac.GetAsync();
            _mydbContextDB.Set<Transaction>().RemoveRange(ListTransac);
            await _mydbContextDB.AddRangeAsync(ListTransac);
            _mydbContextDB.SaveChanges();
            return ListTransac;
        }


    }
}
