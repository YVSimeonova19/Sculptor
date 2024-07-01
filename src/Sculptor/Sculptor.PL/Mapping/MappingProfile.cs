using AutoMapper;
using Sculptor.Common.Models.Order;
using Sculptor.Common.Models.Product;
using Sculptor.Common.Models.Timetable;
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
        this.CreateMap<OrderIM, Order>()
            .ForMember(d => d.ClientInfo, cfg => cfg.MapFrom(s => new ClientInfo() { 
                FirstName = s.ClientFirstName,
                LastName = s.ClientLastName,
                Email = s.ClientEmail,
                Address = s.ClientAddress,
                Area = s.ClientArea
            }));

        // Map order to the order view model
        this.CreateMap<Order, OrderVM>()
            .ForMember(
                dest => dest.Products,
                cfg => cfg.MapFrom(s => s.Products)
            );

        //
        this.CreateMap<Product, ProductVM>();

        //
        this.CreateMap<ProductIM, Product>();

        //
        this.CreateMap<Timetable, TimetableVM>();
    }
}
