using Store.G02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface IUnitOfWork
    {
        //???????

        // 2 Methods :

        // 1- Generate Repository from any type
        IGenericRepository<Tkey, TEntity> GetRepository<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>;

        // 2- Save Changes
        Task<int> SaveChangesAsync();

    }
}
