using TechnicalInterview.Data;
using TechnicalInterview.Data.Context;
using TechnicalInterview.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalInterview.Model.Delivery;
using TechnicalInterview.Model.Enums;

namespace TechnicalInterview.Tests.ServiceTest
{
    [TestFixture]
    public class ServiceTests
    {
        private IDeliveryService deliveryService;
        private DeliveryContext context;      

        [SetUp]
        public void Init()
        {
            context = GetContextWithData();       

            deliveryService = new DeliveryService(context);
        }

        [Test]
        public async Task GetDeliverys_Should_Get_List_Of_Deliverys()
        {            
            var result = await deliveryService.GetDeliveries();

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetDelivery_Should_Get_Delivery()
        {           
            var result = await deliveryService.GetDelivery(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task InsertDelivery_Should_Insert_Delivery()
        {
            var delivery = GetDeliveries()[0];

            await deliveryService.InsertDelivery(delivery);

            var deliverys = await deliveryService.GetDeliveries();

            Assert.IsTrue(deliverys.Count == 3);
        }
       

        [Test]
        public async Task Deletedeliverys_Should_Throw_Exception_If_Activation_Date_is_not_Null()
        {
            var result = await deliveryService.UpdateDelivery(1, DeliveryStatus.Approved);

            Assert.IsNotNull(result);
        }

        private DeliveryContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<DeliveryContext>()
                            .UseInMemoryDatabase(Guid.NewGuid().ToString())
                            .Options;

            var context = new DeliveryContext(options);

            var deliverys = DataGenerator.GetDeliveries();
            context.Deliveries.AddRange(deliverys);

            context.SaveChanges();

            return context;

        }

        private List<DeliveryView> GetDeliveries()
        {
            var deliveries = new List<DeliveryView>()
            {
                new DeliveryView
                {
                    Id = 1,
                    AccessWindow = new DeliveryTime
                    {
                        EndTime = DateTime.Now.AddHours(2)
                    },
                    Order = new Order
                    {
                        OrderNumber = new Guid(),
                        Sender = "test"
                    },
                    Recepient = new Recepient
                    {
                        Address = "Test",
                        EmailAddress = "test@gmail.com",
                        FirstName = "Test",
                        LastName = "Test",
                        PhoneNumber = "1234567890"
                    }
                },
                new DeliveryView
                {
                    Id = 2,
                    AccessWindow = new DeliveryTime
                    {
                        EndTime = DateTime.Now.AddHours(2)
                    },
                    Order = new Order
                    {
                        OrderNumber = new Guid(),
                        Sender = "test"
                    },
                    Recepient = new Recepient
                    {
                        Address = "Test",
                        EmailAddress = "test@gmail.com",
                        FirstName = "Test",
                        LastName = "Test",
                        PhoneNumber = "1234567890"
                    }
                }
            };

            return deliveries;
        }
    }
}
