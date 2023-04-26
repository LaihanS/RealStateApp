using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.VentaTypes.Commands.UpdateVentaType;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.VentaTypes.Queries.GetAllVentaTypes
{
    public class GetAllVentaTypesQuery : IRequest<Response<IEnumerable<VentaTypeViewModel>>>
    {
    }
    public class GetAllVentaTypesQueryHandler : IRequestHandler<GetAllVentaTypesQuery, Response<IEnumerable<VentaTypeViewModel>>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IVentaTypeRepository ventaTypeRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetAllVentaTypesQueryHandler(IVentaTypeRepository ventaTypeRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.ventaTypeRepository = ventaTypeRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<IEnumerable<VentaTypeViewModel>>> Handle(GetAllVentaTypesQuery request, CancellationToken cancellationToken)
        {
            var categoriesViewModel = await GetAllViewModelWithInclude();
            if (categoriesViewModel == null || categoriesViewModel.Count() ==0) throw new ApiExceptions("Ventas not found", (int)HttpStatusCode.NotFound);
            return new Response<IEnumerable<VentaTypeViewModel>>(categoriesViewModel);
        }

        private async Task<List<VentaTypeViewModel>> GetAllViewModelWithInclude()
        {
            var properties = await ventaTypeRepository.GetAsync();

            return properties.Select(prop => new VentaTypeViewModel
            {
                Nombre = prop.Nombre,
                Descripcion = prop.Descripcion,
                id = prop.id,
            }).ToList();
        }
    }
}
