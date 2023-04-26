﻿using AutoMapper;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = RealStateApp.Infrastructure.Identity.Entities.User;

namespace RealStateApp.Infrastructure.Persistence.Mappings
{
    public class PersistenceProfile: Profile
    {
        public PersistenceProfile() {

            CreateMap<User, UserDto>()
                .ForMember(loginvm => loginvm.Password, action => action.Ignore())
                .ForMember(loginvm => loginvm.ConfirmPassword, action => action.Ignore())
                 //.ForMember(uservm => uservm.Roles, action => action.MapFrom(ac => ac.Roles.ToList()))
        .ReverseMap()
         //.ForMember(uservm => uservm.Roles, action => action.MapFrom(ac => ac.Roles.ToList()))
          .ForMember(loginvm => loginvm.TwoFactorEnabled, action => action.Ignore())
          .ForMember(loginvm => loginvm.SecurityStamp, action => action.Ignore())
            .ForMember(loginvm => loginvm.LockoutEnabled, action => action.Ignore())
             .ForMember(loginvm => loginvm.AccessFailedCount, action => action.Ignore())
              .ForMember(loginvm => loginvm.LockoutEnd, action => action.Ignore())
               .ForMember(loginvm => loginvm.NormalizedEmail, action => action.Ignore())
                .ForMember(loginvm => loginvm.NormalizedUserName, action => action.Ignore())
                .ForMember(loginvm => loginvm.PasswordHash, action => action.Ignore())
          .ForMember(loginvm => loginvm.PhoneNumberConfirmed, action => action.Ignore());

            CreateMap<RealStateApp.Core.Application.Dtos.ImportantDto.User, UserDto>()
               .ForMember(loginvm => loginvm.Password, action => action.Ignore())
               .ForMember(loginvm => loginvm.ConfirmPassword, action => action.Ignore())
       //.ForMember(uservm => uservm.Roles, action => action.MapFrom(ac => ac.Roles.ToList()))
       .ReverseMap()
         //.ForMember(uservm => uservm.Roles, action => action.MapFrom(ac => ac.Roles.ToList()))
         .ForMember(loginvm => loginvm.TwoFactorEnabled, action => action.Ignore())
         .ForMember(loginvm => loginvm.SecurityStamp, action => action.Ignore())
           .ForMember(loginvm => loginvm.LockoutEnabled, action => action.Ignore())
            .ForMember(loginvm => loginvm.AccessFailedCount, action => action.Ignore())
             .ForMember(loginvm => loginvm.LockoutEnd, action => action.Ignore())
              .ForMember(loginvm => loginvm.NormalizedEmail, action => action.Ignore())
               .ForMember(loginvm => loginvm.NormalizedUserName, action => action.Ignore())
               .ForMember(loginvm => loginvm.PasswordHash, action => action.Ignore())
         .ForMember(loginvm => loginvm.PhoneNumberConfirmed, action => action.Ignore());

            //     CreateMap<Products, Product>()
            //.ReverseMap();

            //     CreateMap<ProductDto, Product>()
            //  .ReverseMap();

            //     CreateMap<ProductDto, Products>()
            // .ReverseMap();

        }
    }
}
