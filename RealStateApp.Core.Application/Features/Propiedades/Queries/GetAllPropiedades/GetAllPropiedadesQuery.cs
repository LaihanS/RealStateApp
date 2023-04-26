using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.PropiedadMejora;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades
{
    ///<summary>
    /// Parametro para obtener un producto específico
    ///</summary>
    public class GetAllPropiedadesQuery: IRequest<Response<IList<PropiedadViewModel>>>
    {
        ///</example>234123<example>
        [SwaggerParameter(Description = "Código de la propiedad")]
        public string? PropertyCode { get; set; }

    }


    public class GetAllPropiedadesQueryHandlers : IRequestHandler<GetAllPropiedadesQuery, Response<IList<PropiedadViewModel>>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetAllPropiedadesQueryHandlers(IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<IList<PropiedadViewModel>>> Handle(GetAllPropiedadesQuery query, CancellationToken cancellation)
        {
            var propiedadParameter = mapper.Map<GetAllPropiedadParameters>(query);
            var propiedadList = await GetAsyncJoin(propiedadParameter);
            if (propiedadList == null || propiedadList.Count == 0) throw new ApiExceptions("Properties not found", (int)HttpStatusCode.NotFound);
            return new Response<IList<PropiedadViewModel>>(propiedadList);
        }

        private async Task<List<PropiedadViewModel>> GetAsyncJoin(GetAllPropiedadParameters filter)
        {
            List<UserViewModel> users = await userService.GetAllUsersAsyncJoined();
            users = users.Where(u => u.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString()))).ToList();

            List<PropiedadMejoraViewModel> mejoras = mapper.Map<List<PropiedadMejoraViewModel>>(await propiedadMejoraRepository.GetAsyncWithJoin(new List<string>() { "Mejora" }));
            //List<PropiedadViewModel> prop = await propiedadRepository.GetAsyncWithJoinNoGeneric();
            List<PropiedadViewModel> prop = mapper.Map<List<PropiedadViewModel>>(await propiedadRepository.GetAsync());
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

            if (filter.PropertyCode != null)
            {
                propies = prop.Where(p => p.UnicDigitSequence == filter.PropertyCode).ToList();
            }

            return propies;
        }

    }
}
