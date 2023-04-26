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

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.DeletePropertypeById
{
    ///<summary>
    ///   Parametros para eliminar tipo de propiedad
    ///</summary>
    public class DeletePropertypeById : IRequest<Response<int>>
    {

        ///<example>2</example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int id { get; set; }

    }


    public class DeletePropertypeByIdCommandHandlers : IRequestHandler<DeletePropertypeById, Response<int>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;
        private readonly IPropertyTypeRepository propertypeRepository;

        public DeletePropertypeByIdCommandHandlers(IPropertyTypeRepository propertypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.propertypeRepository = propertypeRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeletePropertypeById query, CancellationToken cancellation)
        {
            PropertyType propiedades = await propertypeRepository.GetByidAsync(query.id);
            if (propiedades == null) throw new ApiExceptions("Property Type not found", (int)HttpStatusCode.NotFound);
            await propertypeRepository.DeleteAsync(propiedades);
            return new Response<int>(propiedades.id);
        }

    }
}
