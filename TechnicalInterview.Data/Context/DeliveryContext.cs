using Microsoft.EntityFrameworkCore;
using TechnicalInterview.Model.Delivery;

namespace TechnicalInterview.Data.Context
{
    public class DeliveryContext : DbContext
    {   
        public DeliveryContext(){
        }

        public DeliveryContext (DbContextOptions<DeliveryContext> options)
            : base(options)
        {
        }

        public DbSet<Recepient> Recepients { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
