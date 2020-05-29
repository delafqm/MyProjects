using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperForWebApp2._2.AutoMapper
{
    public class UserProfile : Profile, IProfile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }

    public class UserProfile1 : Profile, IProfile
    {
        public UserProfile1()
        {
            CreateMap<UserDto1, User1>();
            CreateMap<User1, UserDto1>();
        }
    }
}
