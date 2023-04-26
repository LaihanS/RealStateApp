using AutoMapper;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class MejoraService: GenericAppService<MejoraViewModel, SaveMejoraViewModel, Mejora>, IMejoraService
    {
        private readonly IMejoraRepository mejoraRepository;
        private readonly IMapper mapper;
        public MejoraService(IMejoraRepository mejoraRepository, IMapper mapper) : base(mapper, mejoraRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.mapper = mapper;
        }
    }
}
