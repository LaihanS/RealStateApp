using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RealStateApp.Core.Application.Features.VentaTypes.Commands.UpdateVentaType
{
    ///<summary>
    ///   Parametros para actualizar el tipo de venta
    ///</summary>
    public class UpdateVentaTypeCommand : IRequest<Response<UpdateVentaTypeResponse>>
    {
        ///<example>1</example>
        [SwaggerParameter(Description = "Id del tipo de venta")]
        public int id { get; set; }

        ///<example>Alquiler</example>
        [SwaggerParameter(Description = "Nombre del tipo de venta")]
        public string Nombre { get; set; }

        ///<example>Propiedad para rentar</example>
        [SwaggerParameter(Description = "Descripcion del tipo de venta")]
        public string Descripcion { get; set; }

    }


    public class UpdateVentaTypeCommandHandlers : IRequestHandler<UpdateVentaTypeCommand, Response<UpdateVentaTypeResponse>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IVentaTypeRepository ventaTypeRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public UpdateVentaTypeCommandHandlers(IVentaTypeRepository ventaTypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.ventaTypeRepository = ventaTypeRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<UpdateVentaTypeResponse>> Handle(UpdateVentaTypeCommand command, CancellationToken cancellation)
        {
            var category = await ventaTypeRepository.GetByidAsync(command.id);

            if (category == null)
            {
                throw new ApiExceptions("Property Type not found", (int)HttpStatusCode.NotFound);
            }
            else
            {
                category = mapper.Map<VentaType>(command);
                await ventaTypeRepository.EditAsync(category, category.id);
                var categoryVm = mapper.Map<UpdateVentaTypeResponse>(category);

                return new Response<UpdateVentaTypeResponse>(categoryVm);
            }
        }

    }
}
