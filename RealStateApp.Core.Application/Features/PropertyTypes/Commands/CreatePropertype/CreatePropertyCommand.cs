using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropertypeCommand
{
    ///<summary>
    ///   Parametros para crear el tipo de propiedad
    ///</summary>
    public class CreatePropertypeCommand: IRequest<Response<int>>
    {

        ///<example>Apartamento</example>
        [SwaggerParameter(Description = "Nombre del tipo de propiedad")]
        public string Nombre { get; set; }


        ///<example>Espaciosa propiedad en el centro de la ciudad, con vista al mar</example>
        [SwaggerParameter(Description = "Descripcion del tipo de propiedad")]
        public string Descripcion { get; set; }

    }

    public class CreatePropertypeCommandHandlers : IRequestHandler<CreatePropertypeCommand, Response<int>>
    {

        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropertyTypeRepository propertypeRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

    public CreatePropertypeCommandHandlers(IPropertyTypeRepository propertypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
    {
        this.propertypeRepository = propertypeRepository;
        this.userService = userService;
        this.propiedadMejoraRepository = propiedadMejoraRepository;
        this.propiedadRepository = propiedadRepository;
        this.mapper = mapper;
    }

        public async Task<Response<int>> Handle(CreatePropertypeCommand request, CancellationToken cancellationToken)
        {
            var propiedad = mapper.Map<SavePropertypeViewModel>(request);
            propiedad = mapper.Map<SavePropertypeViewModel>(await propertypeRepository.AddAsync(mapper.Map<PropertyType>(propiedad)));
            return new Response<int>(propiedad.id);
        }


    }
}
