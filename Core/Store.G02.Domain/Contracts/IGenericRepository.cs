using Store.G02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface IGenericRepository<TKey,TEntity> where TEntity : BaseEntity<TKey>
    {
        //??????
        Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey,TEntity> spec,bool changeTracker = false); // For Dynamic Query
        Task<TEntity?> GetAsync(TKey key);
        Task<TEntity?> GetAsync(ISpecifications<TKey,TEntity> spec); // For Dynamic Query
        Task<int> CountAsync(ISpecifications<TKey,TEntity> spec); // Calculate the count of products without the pagination
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


    }
}
