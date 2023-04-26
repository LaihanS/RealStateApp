using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.CreatePropiedad
{
    public class CreatePropertyCommand: IRequest<int>
    {
        public string UnicDigitSequence { get; set; }
        public string Tipo { get; set; }
        public string AgenteNombre { get; set; }
        public string TipoVenta { get; set; }
        public int Precio { get; set; }
        public int MtsTerrain { get; set; }
        public int QuantityHabitaciones { get; set; }
        public int QuantityBaños { get; set; }
        public int VentaTypeId { get; set; }
        public string UserClientId { get; set; }

        public int PropertyTypeId { get; set; }
        public string AgenteId { get; set; }
        public List<int>? IdMejoras { get; set; }

   }

    public class CreatePropertyCommandHandlers : IRequestHandler<CreatePropertyCommand, int>
    {

        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

    public CreatePropertyCommandHandlers(IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
    {
        this.userService = userService;
        this.propiedadMejoraRepository = propiedadMejoraRepository;
        this.propiedadRepository = propiedadRepository;
        this.mapper = mapper;
    }

        public async Task<int> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var propiedad = mapper.Map<SavePropiedadViewModel>(request);
            propiedad = await AddAsync(propiedad);
            return propiedad.id;
        }

        private async Task<SavePropiedadViewModel> AddAsync(SavePropiedadViewModel propiedad)
        {
            List<Domain.Entities.Propiedades> prop = await propiedadRepository.GetAsync();
            List<string> sequences = prop.Select(seq => seq.UnicDigitSequence).ToList();
            propiedad.UnicDigitSequence = sequence6Digit.Secuencia(sequences);
            List<PropiedadMejora> propiedadMejoras = await propiedadMejoraRepository.GetAsync();

           SavePropiedadViewModel savedprop = mapper.Map<SavePropiedadViewModel>(await propiedadRepository.AddAsync(mapper.Map<Domain.Entities.Propiedades>(propiedad)));

            if (savedprop != null)
            {
                foreach (int item in propiedad.IdMejoras)
                {
                    PropiedadMejora prope = new();
                    prope.MejoraId = item;
                    prope.PropiedadId = savedprop.id;
                    PropiedadMejora property = propiedadMejoras.FirstOrDefault(p => p.MejoraId == prope.MejoraId && p.PropiedadId == prope.PropiedadId);
                    if (property != null)
                    {
                        await propiedadMejoraRepository.AddAsync(prope);
                    }
                }
            }

            return savedprop;
        }

    }
}
