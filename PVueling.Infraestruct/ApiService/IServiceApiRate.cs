using PVueling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PVueling.Infraestruct.ApiService
{
    public interface IServiceApiRate
    {      
        Task<IEnumerable<Rate>> GetAsync();
    }
}
