using System;
using System.Linq.Expressions;

namespace Domain.Specification
{
    public class Specification<T>
        where T : class
    {
        public Expression<Func<T, bool>> Expression { get; }

        public Func<T, bool> Func => this.Expression.Compile();

        public Specification(Expression<Func<T, bool>> expression)
        {
            this.Expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return this.Func(entity);
        }
    }
}
