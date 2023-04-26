using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.PropertyTypes.Commands.UpdatePropertypeResponse;
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

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropertypeCommand
{
    ///<summary>
    ///   Paramteros para actualizar un tipo de propiedad
    ///</summary>
    public class UpdatePropertypeCommand : IRequest<Response<UpdatePropertypeResponse>>
    {

        ///<example>2</example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int id { get; set; }

        ///<example>Apartamento</example>
        [SwaggerParameter(Description = "Nombre del tipo de propiedad")]
        public string Nombre { get; set; }

        ///<example>Espaciosa propiedad en el centro de la ciudad, con vista al mar</example>
        [SwaggerParameter(Description = "Descripcion de la propiedad")]
        public string Descripcion { get; set; }
    }


    public class UpdatePropertypeCommandHandlers : IRequestHandler<UpdatePropertypeCommand, Response<UpdatePropertypeResponse>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;
        private readonly IPropertyTypeRepository propertypeRepository;

        public UpdatePropertypeCommandHandlers(IPropertyTypeRepository propertypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.propertypeRepository = propertypeRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<UpdatePropertypeResponse>> Handle(UpdatePropertypeCommand command, CancellationToken cancellation)
        {
            var category = await propertypeRepository.GetByidAsync(command.id);

            if (category == null)
            {
                throw new ApiExceptions("Property Type not found", (int)HttpStatusCode.NotFound);
            }
            else
            {
                category = mapper.Map<PropertyType>(command);
                await propertypeRepository.EditAsync(category, category.id);
                var categoryVm = mapper.Map<UpdatePropertypeResponse>(category);

                return new Response<UpdatePropertypeResponse>(categoryVm);
            }
        }

    }
}
