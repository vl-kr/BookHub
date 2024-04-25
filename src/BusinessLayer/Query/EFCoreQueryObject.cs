using System.Linq.Expressions;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Query;

public class EFCoreQueryObject<TEntity> : QueryObject<TEntity>
    where TEntity : class, new()
{
    public EFCoreQueryObject(DbContext dbContext)
    {
        Query = dbContext.Set<TEntity>().AsQueryable();
    }

    public EFCoreQueryObject<TEntity> Include<TProperty>(
        Expression<Func<TEntity, TProperty>> navigationPropertyPath
    )
    {
        Query = Query.Include(navigationPropertyPath);
        return this;
    }

    public EFCoreQueryObject<TEntity> Include(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includeMethod
    )
    {
        Query = includeMethod(Query);
        return this;
    }

    public override async Task<IEnumerable<TEntity>> ExecuteAsync()
    {
        return await Query.ToListAsync();
    }

    public async Task<PaginationObject<TEntity>> GetPagedResultAsync(int page, int pageSize)
    {
        var totalItems = await Query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var items = await Query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        //var items = this.Page(page, pageSize);

        return new PaginationObject<TEntity>
        {
            Page = page,
            TotalPages = totalPages,
            TotalItems = totalItems,
            Items = items
        };
    }

    public EFCoreQueryObject<TEntity> OrderBy(
        string? sortColumn,
        string? sortOrder,
        bool caseInsensitive = false
    )
    {
        var entityType = typeof(TEntity);
        var property = entityType
            .GetProperties()
            .FirstOrDefault(p =>
                p.Name.Equals(
                    sortColumn ?? "Id",
                    caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal
                )
            );

        if (property != null)
            OrderBy(s => EF.Property<object>(s, property.Name), sortOrder != "desc");
        return this;
    }
}
