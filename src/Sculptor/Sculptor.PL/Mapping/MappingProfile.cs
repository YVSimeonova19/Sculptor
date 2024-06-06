using AutoMapper;
using Sculptor.Common.Models.Order;
using Sculptor.Common.Models.User;
using Sculptor.DAL.Models;

namespace Sculptor.PL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        // Map user to the user view model
        this.CreateMap<User, UserVM>();

        // Map order input model to order
        this.CreateMap<OrderIM, Order>();

        // Map order to the order view model
        this.CreateMap<Order, OrderVM>();
    }
}
