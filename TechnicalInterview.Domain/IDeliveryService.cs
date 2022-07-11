using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnicalInterview.Model.Delivery;
using TechnicalInterview.Model.Enums;

namespace TechnicalInterview.Domain
{
    public interface IDeliveryService
    {
        Task<List<DeliveryView>> GetDeliveries();

        Task<DeliveryView> GetDelivery(int id);

        Task InsertDelivery(Delivery delivery);

        Task<Delivery> UpdateDelivery(int id, DeliveryStatus deliveryStatus);      
    }
}