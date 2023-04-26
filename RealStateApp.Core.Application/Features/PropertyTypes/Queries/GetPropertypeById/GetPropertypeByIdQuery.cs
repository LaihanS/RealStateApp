using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp.Core.Application.Features.Categories.Queries.GetPropertypeByIdQuery
{
    ///<summary>
    ///   Parametros para obtener el tipo de propiedad
    ///</summary>
    public class GetPropertypeByIdQuery : IRequest<Response<PropertypeViewModel>>
    {

        ///<example>2</example>
        [SwaggerParameter(Description = "Id del tipo de propiedad")]
        public int id { get; set; }
    }
    public class GetPropertypeByIdQueryHandler : IRequestHandler<GetPropertypeByIdQuery, Response<PropertypeViewModel>>
    {
        private readonly IPropertyTypeRepository propertypeRepository;
        private readonly IMapper _mapper;

        public GetPropertypeByIdQueryHandler(IPropertyTypeRepository propertypeRepository, IMapper mapper)
        {
            this.propertypeRepository = propertypeRepository;
            _mapper = mapper;
        }

        public async Task<Response<PropertypeViewModel>> Handle(GetPropertypeByIdQuery query, CancellationToken cancellationToken)
        {
            var properties = await propertypeRepository.GetAsync();
            var property  = properties.FirstOrDefault(w => w.id == query.id);
            if (property == null) throw new ApiExceptions("Property Type not found", (int)HttpStatusCode.NotFound);
            var propertyvm = _mapper.Map<PropertypeViewModel>(property);
           
            return new Response<PropertypeViewModel>(propertyvm);
        }
    }

}
