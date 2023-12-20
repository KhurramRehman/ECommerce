using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Model;

namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfile: Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
