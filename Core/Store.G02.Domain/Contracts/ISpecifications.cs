using Store.G02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface ISpecifications<Tkey,TEntity> where TEntity : BaseEntity<Tkey>
    {
        List<Expression<Func<TEntity, object>>> Includes { get; set; }

        Expression<Func<TEntity,bool>>? Criteria { get; set; }
        //Property For Filteration
        
        Expression<Func<TEntity,object>>? OrderBy { get; set; }
        Expression<Func<TEntity,object>>? OrderByDescending { get; set; }

         int Skip { get; set; } 
        //signature of property remove public word ?!
         int Take { get; set; }
         bool IsPagination { get; set; }

    }
}
