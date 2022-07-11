using TechnicalInterview.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalInterview.Model.Delivery;
using TechnicalInterview.Model.Enums;

namespace TechnicalInterview.Domain
{
    public class DeliveryService : IDeliveryService
    {
        private readonly DeliveryContext _context;

        public DeliveryService(DeliveryContext context)
        {
            _context = context;
        }           

        public async Task<List<DeliveryView>> GetDeliveries()
        {            
            return await _context.Deliveries
                .Select(s => new DeliveryView
                {
                    Status = s.Endtime < DateTime.Now ? 
                        Enum.GetName(typeof(DeliveryStatus), 4) : 
                        Enum.GetName(typeof(DeliveryStatus), s.GetStatus()),
                    AccessWindow = new DeliveryTime
                    {
                        StartTime = s.StartTime,
                        EndTime = s.Endtime
                    },
                    Order = new Order
                    {
                        OrderNumber = s.Order.OrderNumber,
                        Sender = s.Order.Sender
                    },
                    Recepient = new Recepient
                    {
                        Id = s.Recepient.Id,
                        FirstName = s.Recepient.FirstName,
                        LastName = s.Recepient.LastName,
                        Address = s.Recepient.Address,
                        EmailAddress = s.Recepient.EmailAddress,
                        PhoneNumber = s.Recepient.PhoneNumber
                    }
                }).ToListAsync();
        }

        public async Task<DeliveryView> GetDelivery(int id)
        {
            return await _context.Deliveries
                .Where(s => s.Id == id)
                .Select(s => new DeliveryView
                {
                    Status = s.Endtime < DateTime.Now ? 
                        Enum.GetName(typeof(DeliveryStatus), 4) : 
                        Enum.GetName(typeof(DeliveryStatus), s.GetStatus()),
                    AccessWindow = new DeliveryTime
                    {
                        StartTime = s.StartTime,
                        EndTime = s.Endtime
                    },
                    Order = new Order 
                    {
                        OrderNumber = s.Order.OrderNumber,
                        Sender = s.Order.Sender
                    },
                    Recepient = new Recepient
                    {
                        Id = s.Recepient.Id,
                        FirstName = s.Recepient.FirstName,
                        LastName = s.Recepient.LastName,
                        Address = s.Recepient.Address,
                        EmailAddress = s.Recepient.EmailAddress,
                        PhoneNumber = s.Recepient.PhoneNumber
                    }
                }).SingleOrDefaultAsync();
        }

        public async Task InsertDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
        }       

        public async Task<Delivery> UpdateDelivery(int id, DeliveryStatus deliveryStatus)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery != null)
            {
                delivery.SetStatus(deliveryStatus);
            }

            return delivery;
        }        
    }
}
