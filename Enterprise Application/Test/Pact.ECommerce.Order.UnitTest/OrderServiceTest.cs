﻿namespace Pact.ECommerce.Order.UnitTest
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.Extensions.Options;
    using Moq;
    using Newtonsoft.Json;
    using Packt.Ecommerce.Caching.Interfaces;
    using Packt.Ecommerce.Common.Options;
    using Packt.Ecommerce.Order;
    using Packt.Ecommerce.Order.Services;
    using Pact.ECommerce.Order.UnitTest.Helper;
    using Xunit;

    public class OrderServiceTest
    {
        [Fact]
        public async Task When_GetOrderByIdAsync_with_ExistingOrder_receive_Order()
        {
            Packt.Ecommerce.Data.Models.Order orderResponse = new Packt.Ecommerce.Data.Models.Order { Id = "1" };
            var httpClientFactory = new MockHttpClientFactory()
            {
                ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(orderResponse))
                }
            };

            IOptions<ApplicationSettings> mockOptions = Options.Create<ApplicationSettings>(new ApplicationSettings { DataStoreEndpoint = "https://orderStore.pact.com" });
            Mock<IDistributedCacheService> mockCacheService = new Mock<IDistributedCacheService>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();


            OrdersService testObject = new OrdersService(httpClientFactory, mockOptions, mapper, mockCacheService.Object);
            var order = testObject.GetOrderByIdAsync("1");

            Assert.Equal(1, order.Id);
        }

        [Theory]
        [InlineData(999, 19.98)]
        [InlineData(2000, 100)]
        public void When_ComputeTotalDiscount_with_OrderTotalAmount(double number, double expectedResult)
        {
            Packt.Ecommerce.Data.Models.Order orderResponse = new Packt.Ecommerce.Data.Models.Order { Id = "1" };
            var httpClientFactory = new MockHttpClientFactory()
            {
                ResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(orderResponse))
                }
            };

            IOptions<ApplicationSettings> mockOptions = Options.Create<ApplicationSettings>(new ApplicationSettings { DataStoreEndpoint = "https://orderStore.pact.com" });
            Mock<IDistributedCacheService> mockCacheService = new Mock<IDistributedCacheService>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            OrdersService testObject = new OrdersService(httpClientFactory, mockOptions, mapper, mockCacheService.Object);
            var result = testObject.ComputeTotalDiscount(number);
            Assert.Equal(result, expectedResult);
        }
    }
}
