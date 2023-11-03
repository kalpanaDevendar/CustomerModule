using AutoMapper;
using CustomerModule.Models;
using System;

namespace CustomerModule.Extensions
{
    public class ApplicationMapper:Profile
    {
        //Created Constructor for ApplicationMapper
        public ApplicationMapper() { 
            CreateMap<Customer,ResultDTO>().ReverseMap();
            CreateMap<Order,ResultDTO>().ReverseMap();

        }  
    }
}
