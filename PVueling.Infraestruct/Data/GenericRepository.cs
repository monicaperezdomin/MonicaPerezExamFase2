using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PVueling.Infraestruct.RepositoryDB;

namespace PVueling.Infraestruct.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        MyDbContextRateTransac _myDbContext;
        private readonly ILogger _logger;

        public GenericRepository(MyDbContextRateTransac context,  ILogger logger)
        {
            _myDbContext = context;
            _logger = logger;
        }

        public void Add(T entity)
        {
            _myDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _myDbContext.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            _myDbContext.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _myDbContext.Set<T>().Where(predicate);
            return query;
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _myDbContext.Set<T>();
            return query;
        }

        public void Save()
        {
            try
            {
                _myDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
