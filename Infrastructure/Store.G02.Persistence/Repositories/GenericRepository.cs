using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repositories
{
    public class GenericRepository<TKey, TEntity>(StoreDbContext _context) : IGenericRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        //BussinesLogicLayer
        //Step 1: GenericRepository
        //Step 2: Unit Of Work

        ///?????
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool changeTracker = false)
        {
            //EF Core By Default Dosen't map the Naigtional Property So we need to make it map the Naigtional Property
            // if  your type is product get with U the navgiational property of brand and type
           
            if(typeof(TEntity) == typeof(Product))
            {
                return changeTracker ?
                await _context.Products.Include(P => P.Brand).Include(P => P.Type).ToListAsync() as IEnumerable<TEntity> //Against Product direct to get the navigational Property
                : await _context.Products.Include(P => P.Brand).Include(P => P.Type).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }


            return changeTracker ?
                 await _context.Set<TEntity>().ToListAsync()
                 : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }
        public async Task<TEntity?> GetAsync(TKey key)
        {
            if(typeof (TEntity) == typeof(Product))
            {
                //return await _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == key as int?) as TEntity;
                return await _context.Products.Include(P => P.Brand).Include(P => P.Type).Where(P => P.Id == key as int?).FirstOrDefaultAsync() as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(key);
        }
        public async Task AddAsync(TEntity entity)
        {
           await _context.AddAsync(entity);
        }
        public void Update(TEntity entity) //Void
        {
             _context.Update(entity);
        }
        public void Delete(TEntity entity) //Void
        {
            _context.Remove(entity);
        }

    }
}
