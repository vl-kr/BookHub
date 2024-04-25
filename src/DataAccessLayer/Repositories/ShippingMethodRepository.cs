using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class ShippingMethodRepository : GenericRepository<ShippingMethod>, IShippingMethodRepository
{
    public ShippingMethodRepository(BookHubDbContext context)
        : base(context) { }
}
