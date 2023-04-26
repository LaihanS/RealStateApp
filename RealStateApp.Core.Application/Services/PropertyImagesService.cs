using AutoMapper;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.PropertyImages;
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
    public class PropertyImagesService : GenericAppService<PropertyImagesViewModel, SavePropertyImagesViewModel, PropertyImages>, IPropertyImagesService
    {
        private readonly IPropertyImagesRepository property;
        private readonly IMapper mapper;
        public PropertyImagesService(IPropertyImagesRepository property, IMapper mapper) : base(mapper, property)
        {
            this.property = property;
            this.mapper = mapper;
        }
    }
}
