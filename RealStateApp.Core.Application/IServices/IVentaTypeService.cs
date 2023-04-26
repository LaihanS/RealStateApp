using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.IServices
{
    public interface IVentaTypeService: IGenericAppService<VentaTypeViewModel, SaveVentaTypeViewModel, VentaType>
    {
    }
}
