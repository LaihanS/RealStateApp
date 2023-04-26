using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.VentaTypes.Commands.DeleteVentaType
{
    ///<summary>
    ///   Parametros para eliminar el tipo de venta
    ///</summary>
    public class DeleteVentaTypeById : IRequest<Response<int>>
    {
        ///<example>1</example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int id { get; set; }

    }


    public class DeleteVentaTypeByIdCommandHandlers : IRequestHandler<DeleteVentaTypeById, Response<int>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IVentaTypeRepository ventaTypeRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public DeleteVentaTypeByIdCommandHandlers(IVentaTypeRepository ventaTypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.ventaTypeRepository = ventaTypeRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteVentaTypeById query, CancellationToken cancellation)
        {
            VentaType propiedades = await ventaTypeRepository.GetByidAsync(query.id);
            if (propiedades == null) throw new ApiExceptions("Property Type not found", (int)HttpStatusCode.NotFound);
            await ventaTypeRepository.DeleteAsync(propiedades);
            return new Response<int>(propiedades.id);
        }

    }
}
