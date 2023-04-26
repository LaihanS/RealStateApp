using AutoMapper;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejoraCommand;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejorasCommand;
using RealStateApp.Core.Application.Features.PropertyTypes.Commands.UpdatePropertypeResponse;
using RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropertypeCommand;
using RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropertypeCommand;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad;
using RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.CreateVentaType;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.UpdateVentaType;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyImages;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.PropiedadMejora;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Mappings
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile() {

            #region ParametersQueries
            CreateMap<GetAllPropiedadesQuery, GetAllPropiedadParameters>()
                 .ReverseMap();

            #endregion

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
                 .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
           .ReverseMap();
            //.ForMember(loginvm => loginvm, action => action.Ignore());

            CreateMap<UserViewModel, SaveUserViewModel>()
             .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
              .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
        .ReverseMap();
          //.ForMember(loginvm => loginvm.Productos, action => action.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(loginvm => loginvm.Password, action => action.Ignore())
                .ForMember(loginvm => loginvm.ConfirmPassword, action => action.Ignore())
        .ReverseMap()
          .ForMember(loginvm => loginvm.TwoFactorEnabled, action => action.Ignore())
          .ForMember(loginvm => loginvm.SecurityStamp, action => action.Ignore())
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

            // CreateMap<ProductDto, SaveProductViewModel>()
            //.ReverseMap()
            // .ForMember(loginvm => loginvm.Usuario, action => action.Ignore());


            #endregion


            #region PropiedadMappings
            CreateMap<PropertyImages, SavePropertyImagesViewModel>()
       .ReverseMap()
        .ForMember(loginvm => loginvm.created, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
        .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<PropertyImages, PropertyImagesViewModel>()
          .ReverseMap()
           .ForMember(loginvm => loginvm.created, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
           .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<PropertyImagesViewModel, SavePropertyImagesViewModel>()
          .ReverseMap();
            #endregion

            #region PropiedadMappings
            CreateMap<Propiedades, SavePropiedadViewModel>()
       .ReverseMap()
        .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
        .ForMember(loginvm => loginvm.VentaType, action => action.Ignore())
        .ForMember(loginvm => loginvm.PropiedadType, action => action.Ignore())
        //.ForMember(loginvm => loginvm.Mejoras, action => action.Ignore())
        .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

          CreateMap<CreatePropertyCommand, SavePropiedadViewModel>()
         . ForMember(loginvm => loginvm.PropiedadMejoras, action => action.Ignore())
         .ForMember(loginvm => loginvm.TiposVenta, action => action.Ignore())
         .ForMember(loginvm => loginvm.TiposPropiedad, action => action.Ignore())
         .ForMember(loginvm => loginvm.Mejoras, action => action.Ignore())
         .ForMember(loginvm => loginvm.file1, action => action.Ignore())
         .ForMember(loginvm => loginvm.file2, action => action.Ignore())
         .ForMember(loginvm => loginvm.file3, action => action.Ignore())
         .ForMember(loginvm => loginvm.file4, action => action.Ignore())
         .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
         .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
         .ReverseMap();

            CreateMap<UpdatePropiedadCommand, SavePropiedadViewModel>()
        .ForMember(loginvm => loginvm.PropiedadMejoras, action => action.Ignore())
        .ForMember(loginvm => loginvm.TiposVenta, action => action.Ignore())
        .ForMember(loginvm => loginvm.TiposPropiedad, action => action.Ignore())
        .ForMember(loginvm => loginvm.Mejoras, action => action.Ignore())
        .ForMember(loginvm => loginvm.file1, action => action.Ignore())
        .ForMember(loginvm => loginvm.file2, action => action.Ignore())
        .ForMember(loginvm => loginvm.file3, action => action.Ignore())
        .ForMember(loginvm => loginvm.file4, action => action.Ignore())
        .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
        .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
        .ReverseMap();

            CreateMap<PropiedadUpdateResponse, SavePropiedadViewModel>()
      .ForMember(loginvm => loginvm.PropiedadMejoras, action => action.Ignore())
      .ForMember(loginvm => loginvm.TiposVenta, action => action.Ignore())
      .ForMember(loginvm => loginvm.TiposPropiedad, action => action.Ignore())
      .ForMember(loginvm => loginvm.Mejoras, action => action.Ignore())
      .ForMember(loginvm => loginvm.file1, action => action.Ignore())
      .ForMember(loginvm => loginvm.file2, action => action.Ignore())
      .ForMember(loginvm => loginvm.file3, action => action.Ignore())
      .ForMember(loginvm => loginvm.file4, action => action.Ignore())
      .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
      .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
      .ReverseMap();

            CreateMap<PropiedadMejora, PropiedadMejoraViewModel>()
   .ReverseMap()
    .ForMember(loginvm => loginvm.created, action => action.Ignore())
    .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
    .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
    .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<Propiedades, PropiedadViewModel>()
          .ReverseMap()
           .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
           .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<PropiedadViewModel, SavePropiedadViewModel>()
          .ReverseMap()
                  .ForMember(loginvm => loginvm.VentaType, action => action.Ignore())
        .ForMember(loginvm => loginvm.PropiedadType, action => action.Ignore());
        //.ForMember(loginvm => loginvm.Mejoras, action => action.Ignore());
            #endregion

            #region MejoraMappings
            CreateMap<Mejora, SaveMejoraViewModel>()
       .ReverseMap()
        .ForMember(loginvm => loginvm.created, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
        //.ForMember(loginvm => loginvm.Propiedad, action => action.Ignore())
        .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<Mejora, UpdateMejorasResponse>()
     .ReverseMap()
      .ForMember(loginvm => loginvm.created, action => action.Ignore())
      .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
      .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
      //.ForMember(loginvm => loginvm.Propiedad, action => action.Ignore())
      .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<Mejora, UpdateMejorasCommand>()
    .ReverseMap()
     .ForMember(loginvm => loginvm.created, action => action.Ignore())
     .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
     .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
     //.ForMember(loginvm => loginvm.Propiedad, action => action.Ignore())
     .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<CreateMejoraCommand, SaveMejoraViewModel>()
       .ReverseMap();

            CreateMap<Mejora, MejoraViewModel>()
          .ReverseMap()
           .ForMember(loginvm => loginvm.created, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
           .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<MejoraViewModel, SaveMejoraViewModel>()
          .ReverseMap()
          .ForMember(loginvm => loginvm.Propiedad, action => action.Ignore());
            #endregion

            #region VentaMappings
            CreateMap<VentaType, SaveVentaTypeViewModel>()
       .ReverseMap()
        .ForMember(loginvm => loginvm.created, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
        .ForMember(loginvm => loginvm.Propiedades, action => action.Ignore())
        .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<VentaType, UpdateVentaTypeCommand>()
    .ReverseMap()
     .ForMember(loginvm => loginvm.created, action => action.Ignore())
     .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
     .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
     .ForMember(loginvm => loginvm.Propiedades, action => action.Ignore())
     .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<VentaType, UpdateVentaTypeResponse>()
.ReverseMap()
.ForMember(loginvm => loginvm.created, action => action.Ignore())
.ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
.ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
.ForMember(loginvm => loginvm.Propiedades, action => action.Ignore())
.ForMember(loginvm => loginvm.createdBy, action => action.Ignore());



            CreateMap<CreateVentatTypeCommand, SaveVentaTypeViewModel>()
     .ReverseMap();

            CreateMap<UpdateVentaTypeCommand, SaveVentaTypeViewModel>()
  .ReverseMap();

            CreateMap<VentaType, VentaTypeViewModel>()
          .ReverseMap()
           .ForMember(loginvm => loginvm.created, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
           .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<VentaTypeViewModel, SaveVentaTypeViewModel>()
          .ReverseMap()
          .ForMember(loginvm => loginvm.Propiedades, action => action.Ignore());
            #endregion

            #region ProperTypeMappings
            CreateMap<PropertyType, SavePropertypeViewModel>()
       .ReverseMap()
        .ForMember(loginvm => loginvm.created, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
        .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
        .ForMember(loginvm => loginvm.Propiedades, action => action.Ignore())
        .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<CreatePropertypeCommand, SavePropertypeViewModel>()
            .ForMember(loginvm => loginvm.HasError, action => action.Ignore())
            .ForMember(loginvm => loginvm.ErrorDetails, action => action.Ignore())
       .ReverseMap();

            CreateMap<PropertyType, PropertypeViewModel>()
          .ReverseMap()
           .ForMember(loginvm => loginvm.created, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
           .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
           .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<PropertyType, UpdatePropertypeCommand>()
         .ReverseMap()
          .ForMember(loginvm => loginvm.created, action => action.Ignore())
          .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
          .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
          .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<PropertyType, PropiedadUpdateResponse>()
        .ReverseMap()
         .ForMember(loginvm => loginvm.created, action => action.Ignore())
         .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
         .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
         .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<PropertyType, UpdatePropertypeResponse>()
      .ReverseMap()
       .ForMember(loginvm => loginvm.created, action => action.Ignore())
       .ForMember(loginvm => loginvm.modifiedBy, action => action.Ignore())
       .ForMember(loginvm => loginvm.modifiedAt, action => action.Ignore())
       .ForMember(loginvm => loginvm.createdBy, action => action.Ignore());

            CreateMap<UpdatePropertypeCommand, UpdatePropertypeResponse>()
       .ReverseMap();

            CreateMap<PropertypeViewModel, SavePropertypeViewModel>()
          .ReverseMap()
          .ForMember(loginvm => loginvm.Propiedades, action => action.Ignore());
            #endregion
        }
    }
}
