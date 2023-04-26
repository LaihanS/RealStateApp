using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Queries.GetVentaTypeById
{
    public class GetMejorasByIdQuery : IRequest<Response<MejoraViewModel>>
    {
        [SwaggerParameter(Description = "Id de la mejora")]
        public int id { get; set; }
    }
    public class GetMejorasByIdQueryHandler : IRequestHandler<GetMejorasByIdQuery, Response<MejoraViewModel>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetMejorasByIdQueryHandler(IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }
        public async Task<Response<MejoraViewModel>> Handle(GetMejorasByIdQuery query, CancellationToken cancellationToken)
        {
            var properties = await mejoraRepository.GetAsync();
            var property = properties.FirstOrDefault(w => w.id == query.id);
            if (property == null) throw new Exception($"Category Not Found.");
            var propertyvm = mapper.Map<MejoraViewModel>(property);

            return new Response<MejoraViewModel>(propertyvm);
        }
    }

}
