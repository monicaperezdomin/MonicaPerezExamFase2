using PVueling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PVueling.Application
{
    public interface IFind
    {
       Task<IEnumerable<Transaction>> GetResultTransact(string sku);
    }
}
