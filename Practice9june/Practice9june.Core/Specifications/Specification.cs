using System;
using System.Linq.Expressions;
using Practice9june.Core.Models;

namespace Practice9june.Core.Specifications
{
    public class Specification<T> where T : IEntity
    {
        public Expression<Func<T,bool>> Expression { get; }

        public Func<T, bool> Func => this.Expression.Compile();

        public Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return Func(entity);
        }
    }
}
