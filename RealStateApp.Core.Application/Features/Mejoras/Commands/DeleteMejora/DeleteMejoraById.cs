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

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.DeleteMejoraById
{
    ///<example>
    ///   Parametros para borrar mejora
    ///<example>

    public class DeleteMejoraById : IRequest<Response<int>>
    {

        ///<example>2</example>
        [SwaggerParameter(Description = "Id de la mejora")]
        public int id { get; set; }

    }


    public class DeleteMejoraByIdCommandHandlers : IRequestHandler<DeleteMejoraById, Response<int>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public DeleteMejoraByIdCommandHandlers(IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteMejoraById query, CancellationToken cancellation)
        {
            Mejora propiedades = await mejoraRepository.GetByidAsync(query.id);
            if (propiedades == null) throw new ApiExceptions("Mejora not found", (int)HttpStatusCode.NotFound);
            await mejoraRepository.DeleteAsync(propiedades);
            return new Response<int>(propiedades.id);
        }

    }
}
