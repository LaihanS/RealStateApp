using AutoMapper;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User = RealStateApp.Infrastructure.Identity.Entities.User;

namespace RealStateApp.Infrastructure.Identity.Mappings
{
    public class IdentityProfile: Profile
    {
        public IdentityProfile() {


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


             #region UserMappings
                CreateMap<AuthenticationRequest, LoginViewModel>().
                   ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
                    .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
                   .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
                    .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
                    .ForMember(loginvm => loginvm.Id, action => action.Ignore())
                  .ReverseMap();

            CreateMap<ForgotPassworRequest, ForgotPasswordViewModel>()
                .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
                    .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
               .ReverseMap();

            CreateMap<UserDto, UserViewModel>()
                 .ForMember(uservm => uservm.RoleList, action => action.MapFrom(ac => ac.RoleList.ToList()))
            .ReverseMap()
             .ForMember(uservm => uservm.RoleList, action => action.MapFrom(ac => ac.RoleList.ToList()));

            CreateMap<UserDto, SaveUserViewModel>()
                .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
                 .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
           .ReverseMap();
            //.ForMember(loginvm => loginvm, action => action.Ignore());

            CreateMap<UserViewModel, SaveUserViewModel>()
             .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
              .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
        .ReverseMap();
          //.ForMember(loginvm => loginvm.Productos, action => action.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(loginvm => loginvm.Password, action => action.Ignore())
                .ForMember(loginvm => loginvm.ConfirmPassword, action => action.Ignore())
        .ReverseMap()
          .ForMember(loginvm => loginvm.TwoFactorEnabled, action => action.Ignore())
          .ForMember(loginvm => loginvm.SecurityStamp, action => action.Ignore())
           .ForMember(loginvm => loginvm.PhoneNumber, action => action.Ignore())
            .ForMember(loginvm => loginvm.LockoutEnabled, action => action.Ignore())
             .ForMember(loginvm => loginvm.AccessFailedCount, action => action.Ignore())
              .ForMember(loginvm => loginvm.LockoutEnd, action => action.Ignore())
               .ForMember(loginvm => loginvm.NormalizedEmail, action => action.Ignore())
                .ForMember(loginvm => loginvm.NormalizedUserName, action => action.Ignore())
                .ForMember(loginvm => loginvm.PasswordHash, action => action.Ignore())
          .ForMember(loginvm => loginvm.PhoneNumberConfirmed, action => action.Ignore());

            CreateMap<User, UserViewModel>()
               .ForMember(loginvm => loginvm.Password, action => action.Ignore())
       .ReverseMap()
         .ForMember(loginvm => loginvm.TwoFactorEnabled, action => action.Ignore())
         .ForMember(loginvm => loginvm.SecurityStamp, action => action.Ignore())
          .ForMember(loginvm => loginvm.PhoneNumber, action => action.Ignore())
           .ForMember(loginvm => loginvm.LockoutEnabled, action => action.Ignore())
            .ForMember(loginvm => loginvm.AccessFailedCount, action => action.Ignore())
             .ForMember(loginvm => loginvm.LockoutEnd, action => action.Ignore())
              .ForMember(loginvm => loginvm.NormalizedEmail, action => action.Ignore())
               .ForMember(loginvm => loginvm.NormalizedUserName, action => action.Ignore())
               .ForMember(loginvm => loginvm.PasswordHash, action => action.Ignore())
         .ForMember(loginvm => loginvm.PhoneNumberConfirmed, action => action.Ignore());


            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
            .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
                .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
           .ReverseMap();


            #endregion

          
        }
    }
}
