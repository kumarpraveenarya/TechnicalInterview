using Moq;
using NUnit.Framework;
using TechnicalInterview.Domain;
using TechnicalInterview.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using TechnicalInterview.Model.Delivery;
using TechnicalInterview.Model.Enums;

namespace TechnicalInterview.Tests.Controller
{
    [TestFixture]
    public class DeliveryControllerTests
    {
        private Mock<IDeliveryService> deliveryService;
        private DeliveryController deliveryController;

        [SetUp]
        public void Init()
        {
            deliveryService = new Mock<IDeliveryService>();

            deliveryController = new DeliveryController(deliveryService.Object);             
        }        

        [Test]
        public async Task GetDeliveries_Should_Get_List_Of_Deliveries()
        {
            var deliveries = GetDeliveries();

            deliveryService.Setup(x => x.GetDeliveries()).ReturnsAsync(deliveries);

            var result = await deliveryController.GetDeliveries();

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetDelivery_Should_Get_Delivery_If_ID_Passed()
        {
            var deliveries = GetDeliveries();           

            deliveryService.Setup(x => x.GetDelivery(It.IsAny<int>())).ReturnsAsync(deliveries[0]);

            var result = await deliveryController.GetDelivery(It.IsAny<int>());

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetDelivery_Should_Get_404_If_Id_Passed_but_not_Exist()
        {
            var deliveries = GetDeliveries();

            deliveryService.Setup(x => x.GetDelivery(It.IsAny<int>())).ReturnsAsync(deliveries[0]);

            var result = await deliveryController.GetDelivery(It.IsAny<int>());
            var statusCodeResult = (IStatusCodeActionResult)result.Result;

            Assert.IsNull(result.Value);
            Assert.AreEqual(statusCodeResult.StatusCode, StatusCodes.Status404NotFound);            
        }

        [Test]
        public async Task PostSupplier_Should_Return_Bad_Request()
        {
            var delivery = GetDeliveries()[1];

            deliveryController.ModelState.AddModelError("", "Invalid Date");

            var result = await deliveryController.PostDelivery(delivery);
            var statusCodeResult = (IStatusCodeActionResult)result.Result;

            Assert.IsNull(result.Value);
            Assert.AreEqual(statusCodeResult.StatusCode, StatusCodes.Status400BadRequest);
        }       

        [Test]
        public async Task PostSupplier_Should_Insert_And_Return_Suppliers()
        {
            var delivery = GetDeliveries()[1];

            deliveryService.Setup(x => x.InsertDelivery(delivery));

            var result = await deliveryController.PostDelivery(delivery);
            var statusCodeResult = (IStatusCodeActionResult)result.Result;

            Assert.IsNotNull(result.Result);
            Assert.AreEqual(statusCodeResult.StatusCode, StatusCodes.Status201Created);
        }

        [Test]
        public async Task UpdateDelivery_Should_Return_UpdatedDelivery()
        {
            var delivery = GetDeliveries()[1];

            deliveryService.Setup(x => x.UpdateDelivery(It.IsAny<int>(), It.IsAny<DeliveryStatus>())).ReturnsAsync(delivery);

            var result = await deliveryController.UpdateDelivery(delivery.Id, DeliveryStatus.Approved);
            var statusCodeResult = (IStatusCodeActionResult)result.Result;

            Assert.IsNotNull(result.Value);            
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
