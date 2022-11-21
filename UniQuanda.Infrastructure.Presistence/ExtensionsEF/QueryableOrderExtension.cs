

using System.Linq.Expressions;
using UniQuanda.Core.Domain.Enums;

namespace UniQuanda.Infrastructure.Presistence.ExtensionsEF
{
    public static class QueryableOrderExtension
    {
        public static IQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, OrderDirectionEnum orderDirection)
        {
            switch (orderDirection)
            {
                case OrderDirectionEnum.Ascending:
                    return source.OrderBy(keySelector);
                case OrderDirectionEnum.Descending:
                    return source.OrderByDescending(keySelector);
                default:
                    return source;
            }
        }
    }
}
