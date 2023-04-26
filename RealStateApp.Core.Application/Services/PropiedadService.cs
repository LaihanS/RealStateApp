using AutoMapper;
using Banking.Core.Application.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyImages;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.PropiedadMejora;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class PropiedadService: GenericAppService<PropiedadViewModel, SavePropiedadViewModel, Propiedades>, IPropiedadService
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public PropiedadService(IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository) :  base(mapper, propiedadRepository)
        {
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }


        public async Task<List<PropiedadViewModel>> GetPropertiesFiltro(FilterViewModel filter)
        {
            List<PropiedadViewModel> prop = mapper.Map<List<PropiedadViewModel>>(await propiedadRepository.GetAsyncWithJoin(new List<string>() { "Imagenes", "PropiedadType" }));
           
            if (filter.BañoCantidad != null)
            {
                prop = prop.Where(prope => prope.QuantityBaños == filter.BañoCantidad.Value).ToList();
            }
            else if (filter.HabiCantidad != null)
            {
                prop = prop.Where(proper => proper.QuantityHabitaciones == filter.HabiCantidad).ToList();
            }
            else if (filter.PropiedadTipoID != null)
            {
                prop = prop.Where(proper => proper.PropertyTypeId == filter.PropiedadTipoID).ToList();
            }
            else if (filter.PrecMax != null)
            {
                prop = prop.Where(proper => proper.Precio <= filter.PrecMax).ToList();
            }
            else if (filter.PrecMin != null)
            {
                prop = prop.Where(proper => proper.Precio >= filter.PrecMin).ToList();
            }
            else if (filter.PrecMin != null && filter.PrecMin != null)
            {
                prop = prop.Where(proper => proper.Precio >= filter.PrecMin && proper.Precio <= filter.PrecMax).ToList();
            }
            else if (filter.PropCodigo != null)
            {
                prop = prop.Where(proper => proper.UnicDigitSequence == filter.PropCodigo).ToList();
            }

            prop = prop.Where(prope =>
           (filter.BañoCantidad == null || prope.QuantityBaños == filter.BañoCantidad.Value) &&
           (filter.HabiCantidad == null || prope.QuantityHabitaciones == filter.HabiCantidad) &&
           (filter.PropiedadTipoID == null || prope.PropertyTypeId == filter.PropiedadTipoID) &&
           (filter.PrecMax == null || prope.Precio <= filter.PrecMax) &&
           (filter.PrecMin == null || prope.Precio >= filter.PrecMin)).ToList();

            return prop;
        }

        public override async Task<SavePropiedadViewModel> AddAsync(SavePropiedadViewModel propiedad)
        {
            List<Propiedades> prop =  await propiedadRepository.GetAsync();
            List<string> sequences = prop.Select(seq => seq.UnicDigitSequence).ToList();
            propiedad.UnicDigitSequence = sequence6Digit.Secuencia(sequences);
            List<PropiedadMejora> propiedadMejoras = await propiedadMejoraRepository.GetAsync();


            SavePropiedadViewModel savedprop = await base.AddAsync(propiedad);

            if (savedprop != null)
            {
                foreach (int item in propiedad.IdMejoras)
                {
                    PropiedadMejora prope = new();
                    prope.MejoraId = item;
                    prope.PropiedadId = savedprop.id;
                    PropiedadMejora property = propiedadMejoras.FirstOrDefault(p => p.MejoraId == prope.MejoraId && p.PropiedadId == prope.PropiedadId);
                    if (property == null)
                    {
                        await propiedadMejoraRepository.AddAsync(prope);
                    }
                }
            }
           
            return savedprop;
        }

        public async Task<List<PropiedadViewModel>> GetAsyncJoin()
        {
            List<UserViewModel> users = await userService.GetAllUsersAsyncJoined();
            users = users.Where(u => u.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString()))).ToList();

            List<PropiedadViewModel> prop = await propiedadRepository.GetAsyncWithJoinNoGeneric();
            List<PropiedadViewModel> propies = prop.Select(p => new PropiedadViewModel()
            {
                Agente = users.Find(u => u.Id == p.AgenteId),
                id = p.id,
                UnicDigitSequence = p.UnicDigitSequence,
                Tipo = p.Tipo,
                AgenteNomre = p.AgenteNomre,
                TipoVenta = p.TipoVenta,
                Precio = p.Precio,
                MtsTerrain = p.MtsTerrain,
                QuantityHabitaciones = p.QuantityHabitaciones,
                QuantityBaños = p.QuantityBaños,
                VentaTypeId = p.VentaTypeId,
                PropertyTypeId = p.PropertyTypeId,
                Descripcion = p.Descripcion,
                PropiedadMejoras = p.PropiedadMejoras,
                Imagenes = p.Imagenes,
                VentaType = p.VentaType,
                PropiedadType = p.PropiedadType,
              }).ToList();

            return propies;
        }

        public async Task<List<MejoraViewModel>> GetMejoras(SavePropiedadViewModel propiedad)
        {
            List<PropiedadMejora> mejoras = await propiedadMejoraRepository.GetAsyncWithJoin(new List<string>() { "Mejora" });
            mejoras = mejoras.Where(m => m.PropiedadId == propiedad.id).ToList();
            List<Mejora> empeoras = mejoras.Select(m => m.Mejora).ToList();

            return mapper.Map<List<MejoraViewModel>>(empeoras);
        }

        public  async Task Update(SavePropiedadViewModel propiedad, int id)
        {
            await propiedadRepository.EditAsync(mapper.Map<Propiedades>(propiedad), propiedad.id);
        }
        public override async Task<SavePropiedadViewModel> EditAsync(SavePropiedadViewModel propiedad, int id)
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
                }
            }

            await base.EditAsync(propiedad, propiedad.id);

            return propiedad;

        }

    }
}
