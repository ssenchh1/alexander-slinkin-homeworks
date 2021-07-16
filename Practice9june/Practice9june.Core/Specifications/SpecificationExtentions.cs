using System;
using System.Linq.Expressions;
using Practice9june.Core.Models;

namespace Practice9june.Core.Specifications
{
    public static class SpecificationExtentions
    {
        public static Specification<T> Or<T>(this Specification<T> left, Specification<T> right) where T : IEntity
        {
            var leftExpression = left.Expression;
            var rightExpression = right.Expression;
            var leftParam = leftExpression.Parameters[0];
            var rightParam = rightExpression.Parameters[0];

            return new Specification<T>(Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    leftExpression.Body,
                    new ParameterReplacer(rightParam, leftParam).Visit(rightExpression.Body)), 
                leftParam));
        }

        public static Specification<T> And<T>(this Specification<T> left, Specification<T> right) where T : IEntity
        {
            var leftExpression = left.Expression;
            var rightExpression = right.Expression;
            var leftParam = leftExpression.Parameters[0];
            var rightParam = rightExpression.Parameters[0];

            return new Specification<T>(Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    leftExpression.Body,
                    new ParameterReplacer(rightParam, leftParam).Visit(rightExpression.Body)), 
                leftParam));
        }

        public static Specification<T> Not<T>(this Specification<T> specification) where T : IEntity
        {
            return new Specification<T>(
                Expression.Lambda<Func<T, bool>>(
                    Expression.Not(specification.Expression.Body)));
        }
    }
}
