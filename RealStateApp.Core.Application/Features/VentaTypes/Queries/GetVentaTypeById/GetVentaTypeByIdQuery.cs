using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.VentaTypes.Queries.GetVentaTypeById
{
    ///<summary>
    ///   Parametros para obtener el tipo de venta
    ///</summary>

    public class GetVentaTypeByIdQuery : IRequest<Response<VentaTypeViewModel>>
    {
        ///<example>1</example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int id { get; set; }
    }
    public class GetVentaTypeByIdQueryHandler : IRequestHandler<GetVentaTypeByIdQuery, Response<VentaTypeViewModel>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IVentaTypeRepository ventaTypeRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetVentaTypeByIdQueryHandler(IVentaTypeRepository ventaTypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.ventaTypeRepository = ventaTypeRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }
        public async Task<Response<VentaTypeViewModel>> Handle(GetVentaTypeByIdQuery query, CancellationToken cancellationToken)
        {
            var properties = await ventaTypeRepository.GetAsync();
            var property = properties.FirstOrDefault(w => w.id == query.id);
            if (property == null) throw new ApiExceptions("Venta Type not found", (int)HttpStatusCode.NotFound);
            var propertyvm = mapper.Map<VentaTypeViewModel>(property);

            return new Response<VentaTypeViewModel>(propertyvm);
        }
    }

}
