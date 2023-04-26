using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadesByID
{
    using AutoMapper;
    using Banking.Core.Application.Helpers;
    using global::RealStateApp.Core.Application.Enums;
    using global::RealStateApp.Core.Application.Exceptions;
    using global::RealStateApp.Core.Application.IRepositories;
    using global::RealStateApp.Core.Application.IServices;
    using global::RealStateApp.Core.Application.ViewModels.Propiedades;
    using global::RealStateApp.Core.Application.ViewModels.PropiedadMejora;
    using global::RealStateApp.Core.Application.ViewModels.User;
    using global::RealStateApp.Core.Application.Wrappers;
    using global::RealStateApp.Core.Domain.Entities;
    using MediatR;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades
    {   
        ///<summary>
         ///   Parametros para obtener propiedad
         ///</summary>
        public class GetPropiedadByIdQuery : IRequest<Response<PropiedadViewModel>>
        {

            ///<example>1</example>
            [SwaggerParameter(Description = "Id de la propiedad")]
            public int id { get; set; }

        }


        public class GetPropiedadByIdQueryHandlers : IRequestHandler<GetPropiedadByIdQuery, Response<PropiedadViewModel>>
        {
            private readonly IMapper mapper;
            Sequence6Digit sequence6Digit = new();
            private readonly IPropiedadRepository propiedadRepository;
            private readonly IUserService userService;
            private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

            public GetPropiedadByIdQueryHandlers(IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
            {
                this.userService = userService;
                this.propiedadMejoraRepository = propiedadMejoraRepository;
                this.propiedadRepository = propiedadRepository;
                this.mapper = mapper;
            }

            public async Task<Response<PropiedadViewModel>> Handle(GetPropiedadByIdQuery query, CancellationToken cancellation)
            {
                var propiedadReturn = await GetAsyncJoin(query.id);
                if (propiedadReturn == null) throw new ApiExceptions("Property not found", (int)HttpStatusCode.NotFound);
                return new Response<PropiedadViewModel>(propiedadReturn);
            }

            private async Task<PropiedadViewModel> GetAsyncJoin(int id)
            {
                List<UserViewModel> users = await userService.GetAllUsersAsyncJoined();
                users = users.Where(u => u.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString()))).ToList();

                List<PropiedadViewModel> prop = mapper.Map<List<PropiedadViewModel>>(await propiedadRepository.GetAsync());
                List<PropiedadMejoraViewModel> mejoras = mapper.Map<List<PropiedadMejoraViewModel>>(await propiedadMejoraRepository.GetAsyncWithJoin(new List<string>() {"Mejora"}));
                List<PropiedadViewModel> propies = prop.Select(p => new PropiedadViewModel()
                {
                    Agente = users.Find(u => u.Id == p.AgenteId),
                    id = p.id,
                    UnicDigitSequence = p.UnicDigitSequence,
                    Tipo = p.Tipo,
                    AgenteNomre = p.AgenteNomre,
                    TipoVenta = p.TipoVenta,
                    Precio = p.Precio,
                    MtsTerrain = p.MtsTerrain,
                    QuantityHabitaciones = p.QuantityHabitaciones,
                    QuantityBaños = p.QuantityBaños,
                    VentaTypeId = p.VentaTypeId,
                    PropertyTypeId = p.PropertyTypeId,
                    Descripcion = p.Descripcion,
                    PropiedadMejoras = mejoras.Where(u => u.PropiedadId == p.id).ToList(),
                    Imagenes = p.Imagenes,
                    VentaType = p.VentaType,
                    PropiedadType = p.PropiedadType,
                    
                }).ToList();


                PropiedadViewModel returnpropie = propies.FirstOrDefault(p => p.id == id);
                return returnpropie;
            }

        }
    }
}
