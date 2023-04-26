using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.DeletePropiedadById
{
    ///<summary>
    /// Parametro para borrar un producto específico
    ///</summary>
    public class DeletePropiedadByID : IRequest<Response<int>>
    {
        ///<example>234123</example>
        [SwaggerParameter(Description = "Id de la propiedad")]
        public int id { get; set; }

    }


    public class DeletePropiedadByIDCommandHandlers : IRequestHandler<DeletePropiedadByID, Response<int>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public DeletePropiedadByIDCommandHandlers(IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeletePropiedadByID query, CancellationToken cancellation)
        {
            Domain.Entities.Propiedades propiedades = await propiedadRepository.GetByidAsync(query.id);
            if (propiedades == null) throw new ApiExceptions("Property not found", (int)HttpStatusCode.NotFound);
            await propiedadRepository.DeleteAsync(propiedades);
            return new Response<int>(propiedades.id);
        }

    }
}
