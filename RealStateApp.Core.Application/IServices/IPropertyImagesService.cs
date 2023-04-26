using RealStateApp.Core.Application.ViewModels.PropertyImages;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.IServices
{
    public interface IPropertyImagesService : IGenericAppService<PropertyImagesViewModel, SavePropertyImagesViewModel, PropertyImages>
    {
    }
}
