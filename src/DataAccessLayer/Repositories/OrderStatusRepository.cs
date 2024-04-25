using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace DataAccessLayer.Repositories;

public class OrderStatusRepository : GenericRepository<OrderStatus>, IOrderStatusRepository
{
    public OrderStatusRepository(BookHubDbContext context)
        : base(context) { }
}
