using TechnicalInterview.Data.Context;
using TechnicalInterview.Model.Delivery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalInterview.Data
{
    public class DataGenerator
    {
        public static List<Delivery> GetDeliveries()
        {
            return new List<Delivery>
            {
                new Delivery
                {
                    Id = 1,                    
                    Endtime = DateTime.Now.AddHours(2),                  
                    Order = GetOrders()[0],  
                    Recepient = GetRecepients()[0]
                },
                new Delivery
                {
                    Id = 2,
                    Endtime = DateTime.Now.AddHours(2),                   
                    Order = GetOrders()[1],                  
                    Recepient = GetRecepients()[1]
                }
            };
        }

        public static List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    OrderNumber = new Guid(),                   
                    Sender = "test"
                },
                new Order
                {
                    OrderNumber = new Guid(),                  
                    Sender = "Ikea"
                }
            };
        }

        public static List<Recepient> GetRecepients()
        {           
            return new List<Recepient>
            {
                new Recepient
                {       
                    Id = 1,
                    FirstName = "P",
                    LastName ="Kumar",
                    EmailAddress = "test@test.com",
                    PhoneNumber = "1234567890",
                    Address = "1, test road, sutton, sm5 3ks"
                },
                new Recepient
                {        
                    Id = 2,
                    FirstName = "Patrick",
                    LastName ="Star",
                    EmailAddress = "test@test.com",
                    PhoneNumber = "1234567890",
                    Address = "2, test road, sutton, sm5 3ks"
                }
            };
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DeliveryContext(
                serviceProvider.GetRequiredService<DbContextOptions<DeliveryContext>>()))
            {
                if (context.Deliveries.Any())
                {
                    return;   // Data was already seeded
                }               

                var deliveries = GetDeliveries();

                context.Recepients.AddRange(GetRecepients());
                context.Orders.AddRange(GetOrders());
                context.Deliveries.AddRange(deliveries);

                context.SaveChanges();
            }
        }
    }
}
