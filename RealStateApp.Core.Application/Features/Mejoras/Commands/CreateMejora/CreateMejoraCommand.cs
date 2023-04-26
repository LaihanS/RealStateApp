using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Commands.CreateMejoraCommand
{
    ///<summary>
    ///   Parametros para la creacion de una mejora
    ///</summary>
    public class CreateMejoraCommand : IRequest<Response<int>>
    {
  
        ///<example>Piscina</example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Nombre { get; set; }


        ///<example>Area de recreacion</example>
        [SwaggerParameter(Description = "Descripcion de la mejora")]
        public string Descripcion { get; set; }

    }

    public class CreateMejoraCommandCommandHandlers : IRequestHandler<CreateMejoraCommand, Response<int>>
    {

        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public CreateMejoraCommandCommandHandlers(IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateMejoraCommand request, CancellationToken cancellationToken)
        {
            var propiedad = mapper.Map<SaveMejoraViewModel>(request);
            propiedad = mapper.Map<SaveMejoraViewModel>(await mejoraRepository.AddAsync(mapper.Map<Mejora>(propiedad)));
            return new Response<int>(propiedad.id);
        }


    }
}
