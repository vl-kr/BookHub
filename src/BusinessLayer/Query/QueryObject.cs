using System.Linq.Expressions;

namespace BusinessLayer.Query;

public abstract class QueryObject<TEntity> : IQueryObject<TEntity>
    where TEntity : class, new()
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 10;
    protected IQueryable<TEntity> Query;

    public QueryObject<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
    {
        Query = Query.Where(predicate);
        return this;
    }

    public QueryObject<TEntity> Page(int? page, int? pageSize)
    {
        var currentPage = page ?? DefaultPage;
        var currentPageSize = pageSize ?? DefaultPageSize;

        Query = Query.Skip((currentPage - 1) * currentPageSize).Take(currentPageSize);
        return this;
    }

    public QueryObject<TEntity> OrderBy<TKey>(
        Expression<Func<TEntity, TKey>> selector,
        bool ascending = true
    )
    {
        Query = ascending switch
        {
            true => Query.OrderBy(selector),
            false => Query.OrderByDescending(selector)
        };
        return this;
    }

    public abstract Task<IEnumerable<TEntity>> ExecuteAsync();
}
