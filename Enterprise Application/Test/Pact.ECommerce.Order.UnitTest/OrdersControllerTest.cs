using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Packt.Ecommerce.DTO.Models;
using Packt.Ecommerce.Order.Contracts;
using Packt.Ecommerce.Order.Controllers;
using Xunit;

namespace Pact.ECommerce.Order.UnitTest
{
    public class OrdersControllerTest
    {
        [Fact]
        public async Task Create_Object_OfType_OrdersController()
        {
            OrdersController testObject = new OrdersController(null);
            Assert.NotNull(testObject);
        }

        [Fact]
        public async Task When_GetOrdersAsync_with_ExistingOrder_receive_OkObjectResult()
        {
            var stub = new Mock<IOrderService>();
            stub.Setup(x => x.GetOrderByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<OrderDetailsViewModel>(new OrderDetailsViewModel { Id = "1" }));
            OrdersController testObject = new OrdersController(stub.Object);

            var order = await testObject.GetOrderById("1").ConfigureAwait(false);
            Assert.IsType<OkObjectResult>(order);
        }

        [Fact]
        public async Task When_GetOrdersAsync_with_No_ExistingOrder_receive_NotFoundResult()
        {
            var stub = new Mock<IOrderService>();
            stub.Setup(x => x.GetOrderByIdAsync(It.IsAny<string>())).Returns(Task.FromResult<OrderDetailsViewModel>(null));
            OrdersController testObject = new OrdersController(stub.Object);

            var order = await testObject.GetOrderById("1").ConfigureAwait(false);
            Assert.IsType<NotFoundResult>(order);
        }
    }
}