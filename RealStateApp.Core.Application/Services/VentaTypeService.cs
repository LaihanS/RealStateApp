using AutoMapper;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class VentaTypeService: GenericAppService<VentaTypeViewModel, SaveVentaTypeViewModel, VentaType>, IVentaTypeService
    {
        private readonly IVentaTypeRepository ventaType;
        private readonly IMapper mapper;
        public VentaTypeService(IVentaTypeRepository ventaType, IMapper mapper) : base(mapper, ventaType)
        {
            this.ventaType = ventaType;
            this.mapper = mapper;
        }
    }
}
