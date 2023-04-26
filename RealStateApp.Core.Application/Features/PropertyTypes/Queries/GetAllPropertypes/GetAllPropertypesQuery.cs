using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Queries.GetAllPropertypesQuery
{
    public class GetAllPropertypesQuery : IRequest<Response<IEnumerable<PropertypeViewModel>>>
    {       
    }
    public class GetAllPropertypesQueryHandler : IRequestHandler<GetAllPropertypesQuery, Response<IEnumerable<PropertypeViewModel>>>
    {
        private readonly IPropertyTypeRepository propertypeRepository;
        private readonly IMapper mapper;

        public GetAllPropertypesQueryHandler(IPropertyTypeRepository propertypeRepository, IMapper mapper)
        {
            this.propertypeRepository = propertypeRepository;
            this.mapper = mapper;
        }
        public async Task<Response<IEnumerable<PropertypeViewModel>>> Handle(GetAllPropertypesQuery request, CancellationToken cancellationToken)
        {        
            var categoriesViewModel = await GetAllViewModelWithInclude();
            if (categoriesViewModel == null || categoriesViewModel.Count() == 0) throw new ApiExceptions("No Property Types Found", (int)HttpStatusCode.NotFound);
            return new Response<IEnumerable<PropertypeViewModel>>(categoriesViewModel);
        }

        private async Task<List<PropertypeViewModel>> GetAllViewModelWithInclude()
        {
            var properties = await propertypeRepository.GetAsync();

            return properties.Select(prop => new PropertypeViewModel
            {
                Nombre = prop.Nombre,
                Descripcion = prop.Descripcion,
                id = prop.id,
            }).ToList();
        }
    }
}
