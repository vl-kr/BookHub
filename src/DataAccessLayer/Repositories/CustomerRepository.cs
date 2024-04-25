using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Extensions;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(BookHubDbContext context)
        : base(context) { }

    public async Task<IEnumerable<Customer>> GetAllWithAllRelatedDataAsync()
    {
        return await Context.Customers.IncludeAllRelatedData().ToListAsync();
    }

    public async Task<Customer?> FindByIdWithAllRelatedDataAsync(int id)
    {
        return await Context.Customers.IncludeAllRelatedData().FirstOrDefaultAsync(b => b.Id == id);
    }
}
