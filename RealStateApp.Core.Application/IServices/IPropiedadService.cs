using RealStateApp.Core.Application.ViewModels;
using RealStateApp.Core.Application.ViewModels.Mejora;
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
    public interface IPropiedadService: IGenericAppService<PropiedadViewModel, SavePropiedadViewModel, Propiedades>
    {
        Task<List<PropiedadViewModel>> GetPropertiesFiltro(FilterViewModel filter);
        Task<List<PropiedadViewModel>> GetAsyncJoin();
        Task Update(SavePropiedadViewModel propiedad, int id);
        Task<List<MejoraViewModel>> GetMejoras(SavePropiedadViewModel propiedad);
    }
}
