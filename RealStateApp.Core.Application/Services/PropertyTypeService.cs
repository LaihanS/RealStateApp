using AutoMapper;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class PropertyTypeService : GenericAppService<PropertypeViewModel, SavePropertypeViewModel, PropertyType>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository property;
        private readonly IMapper mapper;
        public PropertyTypeService(IPropertyTypeRepository property, IMapper mapper) : base(mapper, property)
        {
            this.property = property;
            this.mapper = mapper;
        }
    }
}
