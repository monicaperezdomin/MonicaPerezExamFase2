using PVueling.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PVueling.Domain.Interface
{
    public interface IRepository
    {        
        Task<IEnumerable<Rate>> GetRate();
        Task<IEnumerable<Transaction>> GetTransac();
    }
}
