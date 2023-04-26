using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Propiedades.Commands.UpdatePropiedad
{
    public class UpdatePropiedadCommand : IRequest<PropiedadUpdateResponse>
    {
        public int id { get; set; }
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
        public List<int>? EditIdMejoras { get; set; }

    }

    public class UpdatePropiedadCommandHandlers : IRequestHandler<UpdatePropiedadCommand, PropiedadUpdateResponse>
    {

        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public UpdatePropiedadCommandHandlers(IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public async Task<PropiedadUpdateResponse> Handle(UpdatePropiedadCommand request, CancellationToken cancellationToken)
        {
            var propiedad = mapper.Map<SavePropiedadViewModel>(request);
            if (propiedad == null) throw new Exception("Property not found");
            propiedad = await EditAsync(propiedad, propiedad.id);
            return mapper.Map<PropiedadUpdateResponse>(request); 
        }

        public async Task<SavePropiedadViewModel> EditAsync(SavePropiedadViewModel propiedad, int id)
        {
            List<PropiedadMejora> mejoras = await propiedadMejoraRepository.GetAsyncWithJoin(new List<string>() { "Mejora" });

            if (propiedad.EditIdMejoras != null)
            {
                foreach (int idmejora in propiedad.EditIdMejoras)
                {
                    PropiedadMejora mej = mejoras.Find(m => m.Mejora.id == idmejora && m.PropiedadId == propiedad.id);
                    await propiedadMejoraRepository.DeleteAsync(mej);
                }
            }
            else if (propiedad.IdMejoras != null || propiedad.IdMejoras.Count() != 0)
            {
                foreach (int item in propiedad.IdMejoras)
                {
                    PropiedadMejora prope = new();
                    prope.MejoraId = item;
                    prope.PropiedadId = propiedad.id;
                    await propiedadMejoraRepository.AddAsync(prope);
                    PropiedadMejora property = mejoras.FirstOrDefault(p => p.MejoraId == prope.MejoraId && p.PropiedadId == prope.PropiedadId);
                    if (property != null)
                    {
                        await propiedadMejoraRepository.AddAsync(prope);
                    }
                }
            }

            await propiedadRepository.EditAsync(mapper.Map<Domain.Entities.Propiedades>(propiedad), propiedad.id);

            return propiedad;

        }

    }
}
