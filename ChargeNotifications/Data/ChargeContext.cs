using Microsoft.EntityFrameworkCore;
using ChargeNotifications.Models;

namespace ChargeNotifications.Data
{
    public class ChargeContext : DbContext
    {
        public ChargeContext(DbContextOptions<ChargeContext> options) : base(options)
        {

        }

        public DbSet<Charge> Charge { get; set; }

        public static implicit operator ChargeContext(Charge v)
        {
            throw new NotImplementedException();
        }
    }
}
