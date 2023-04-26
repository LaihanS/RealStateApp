using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora;
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

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejorasCommand
{
    ///<summary>
    ///   Parametros para actualizar un producto
    ///</summary>
    public class UpdateMejorasCommand : IRequest<Response<UpdateMejorasResponse>>
    {

        ///<example>1</example>
        [SwaggerParameter(Description = "Id de la mejora")]
        public int id { get; set; }

        ///<example>Piscina</example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Nombre { get; set; }

        ///<example>Area de recreacion</example>
        [SwaggerParameter(Description = "Descripcion de la mejora")]
        public string Descripcion { get; set; }

    }


    public class UpdateMejorasCommandHandlers : IRequestHandler<UpdateMejorasCommand, Response<UpdateMejorasResponse>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public UpdateMejorasCommandHandlers(IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }
        public async Task<Response<UpdateMejorasResponse>> Handle(UpdateMejorasCommand command, CancellationToken cancellation)
        {
            var category = await mejoraRepository.GetByidAsync(command.id);

            if (category == null)
            {
                throw new ApiExceptions("Mejora not found", (int)HttpStatusCode.NotFound);
            }
            else
            {
                category = mapper.Map<Mejora>(command);
                await mejoraRepository.EditAsync(category, category.id);
                var categoryVm = mapper.Map<UpdateMejorasResponse>(category);

                return new Response<UpdateMejorasResponse>(categoryVm);
            }
        }

    }
}
