using Microsoft.EntityFrameworkCore;
using ChargeNotifications.Models;

namespace ChargeNotifications.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {

        }

        public DbSet<Charge> Customer { get; set; }

      
    }
}
