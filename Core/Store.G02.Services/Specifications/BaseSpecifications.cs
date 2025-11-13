using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Specifications
{
    //Build Query Dynamic Not Static 
    public class BaseSpecifications<Tkey, TEntity> : ISpecifications<Tkey, TEntity> where TEntity : BaseEntity<Tkey>
    {
        //Determine The parts of a Query
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }


        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression )
        {
            Criteria = expression;
        }

    }
}
