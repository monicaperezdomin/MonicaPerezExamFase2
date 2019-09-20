
using PVueling.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVueling.Infraestruct.ApiService
{
    public interface IServiceApiTransaction
    {  
        Task<IEnumerable<Transaction>>GetAsync();
    }
}
