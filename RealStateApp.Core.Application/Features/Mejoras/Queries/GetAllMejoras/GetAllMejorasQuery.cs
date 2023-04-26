using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllMejorasQuery
{
    public class GetAllMejorasQuery : IRequest<Response<IEnumerable<MejoraViewModel>>>
    {
    }
    public class GetAllMejorasQueryHandler : IRequestHandler<GetAllMejorasQuery, Response<IEnumerable<MejoraViewModel>>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetAllMejorasQueryHandler(IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<Response<IEnumerable<MejoraViewModel>>> Handle(GetAllMejorasQuery request, CancellationToken cancellationToken)
        {
            var categoriesViewModel = await GetAllViewModelWithInclude();
            if (categoriesViewModel == null || categoriesViewModel.Count() == 0) throw new ApiExceptions("No Mejoras Found", (int)HttpStatusCode.NotFound);
            return new Response<IEnumerable<MejoraViewModel>>(categoriesViewModel);
        }

        private async Task<List<MejoraViewModel>> GetAllViewModelWithInclude()
        {
            var properties = await mejoraRepository.GetAsync();

            return properties.Select(prop => new MejoraViewModel
            {
                Nombre = prop.Nombre,
                Descripcion = prop.Descripcion,
                id = prop.id,
            }).ToList();
        }
    }
}
