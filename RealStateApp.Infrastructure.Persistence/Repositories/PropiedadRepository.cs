using AutoMapper;
using Banking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Identity.Contexts;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropiedadRepository : GenericAppRepository<Propiedades>, IPropiedadRepository
    {
        public readonly ApplicationContext identityccontext;

        private readonly IMapper mapper;

        public PropiedadRepository(IMapper mapper, ApplicationContext context) : base(context)
        {
            identityccontext = context;
            this.mapper = mapper;
        }

        public virtual async Task<List<PropiedadViewModel>> GetAsyncWithJoinNoGeneric()
        {

          var props = await identityccontext.Propiedades
          .Include(p => p.Imagenes)
          .Include(p => p.PropiedadType)
          .Include(p => p.VentaType)
          .Include(p => p.PropiedadMejoras) 
          .ThenInclude(b => b.Mejora)
          //.Where(b => b. == user.id)
          .ToListAsync();
            List<PropiedadViewModel> users = mapper.Map<List<PropiedadViewModel>>(props);
            return users;
        }


        public async Task<UserDto> GetUserAccount(string iduser)
        {
            Identity.Entities.User user = await identityccontext.
                Set<Identity.Entities.User>().FirstOrDefaultAsync(p => p.Id == iduser);

            if (user == null)
            {
                return new UserDto();
            }
            else
            {
                return mapper.Map<UserDto>(user);
            }
        }


    }

}
