using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Sculptor.BLL.Contracts;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;
using Sculptor.BLL.Implementations;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Sculptor.PL.Mapping;
using Sculptor.Common.Models.Order;

namespace Sculptor.UnitTests;

public class UnitTests
{
    private SculptorDbContext dbContext;
    private IMapper mapper;
    private IOrderService orderService;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SculptorDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        this.dbContext = new SculptorDbContext(options);

        this.mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }).CreateMapper();

        this.orderService = new OrderService(mapper, dbContext);
    }

    [Test]
    public async Task TestCreateOrderAsync()
    {
        // Arrange
        string clientFirstName = "Ivan";
        string clientLastName = "Ivanov";
        string clientEmail = "i.ivanov@yahoo.com";
        string clientAddress = "Ivan Vazov 5";
        string clientArea = "Slaveikov";
        List<int> products = new List<int>() { 1 };

        var orderIM = new OrderIM
        {
            ClientFirstName = clientFirstName,
            ClientLastName = clientLastName,
            ClientEmail = clientEmail,
            ClientAddress = clientAddress,
            ClientArea = clientArea,
            ProductsIds = products
        };

        // Act
        await this.orderService.CreateOrderAsync(orderIM);

        // Assert
        Assert.True(this.dbContext.Orders.ToList().Count == 1);
    }

    [Test]
    public async Task TestCheckIfOrderExistsByIdAsync()
    {
        // Arrange
        int orderId = 1;
        var orders = new List<Order> { new Order { Id = 1 }, new Order { Id = 2 } };
        await this.dbContext.AddRangeAsync(orders);
        await this.dbContext.SaveChangesAsync();

        // Act
        var result = this.orderService.CheckIfOrderExistsById(orderId);

        // Assert
        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public async Task TestCheckUpdateOrderById()
    {
        // Arrange
        int orderId = 0;

        var orderUM = new OrderUM { IsDelivered = true };

        // Act
        var order = await this.orderService.UpdateOrderAsync(orderId, orderUM);

        // Assert
        Assert.That(order.IsDelivered, Is.EqualTo("true"));
    }
}