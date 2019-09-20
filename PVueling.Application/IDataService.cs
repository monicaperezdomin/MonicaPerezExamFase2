using PVueling.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVueling.Application
{
   public interface IDataService
    {
        Task<IEnumerable<Rate>> GetRate();
        Task<IEnumerable<Transaction>> GetTransac();
        
    }
}
