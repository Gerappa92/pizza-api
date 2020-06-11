using gerappa_test01_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace gerappa_test01_api.Data
{
    public interface IRepository<T> where T : CosmoEntity
    {
        public Task<T> Add(T entity);
        public Task<T> Get(string id);
        public Task<IEnumerable<T>> GetAll();
        public Task Update(T entity);
        public Task Delete(string id);
        public Task<IEnumerable<T>> Where(Expression<Func<T, bool>> expression);
    }
}
