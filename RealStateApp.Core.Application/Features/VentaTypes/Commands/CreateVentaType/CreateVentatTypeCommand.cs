using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
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

namespace RealStateApp.Core.Application.Features.VentaTypes.Commands.CreateVentaType
{
    ///<summary>
    ///  Parametros del tipo de venta
    ///</summary>
    public class CreateVentatTypeCommand : IRequest<Response<int>>
    {

        ///<example>Alquiler</example>
        [SwaggerParameter(Description = "Nombre del tipo de venta")]
        public string Nombre { get; set; }

        ///<example>Propiedad para rentar</example>
        [SwaggerParameter(Description = "Descripcion del tipo de venta")]
        public string Descripcion { get; set; }

    }

    public class CreateVentatTypeCommandHandlers : IRequestHandler<CreateVentatTypeCommand, Response<int>>
    {

        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IVentaTypeRepository ventaTypeRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public CreateVentatTypeCommandHandlers(IVentaTypeRepository ventaTypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.ventaTypeRepository = ventaTypeRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateVentatTypeCommand request, CancellationToken cancellationToken)
        {
            var propiedad = mapper.Map<SaveVentaTypeViewModel>(request);
            propiedad = mapper.Map<SaveVentaTypeViewModel>(await ventaTypeRepository.AddAsync(mapper.Map<VentaType>(propiedad)));
            return new Response<int>(propiedad.id);
        }


    }
}
