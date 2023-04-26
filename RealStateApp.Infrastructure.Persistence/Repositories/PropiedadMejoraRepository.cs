using AutoMapper;
using Banking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Identity.Contexts;
using RealStateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class PropiedadMejoraRepository : GenericAppRepository<PropiedadMejora>, IPropiedadMejoraRepository
    {
        public readonly ApplicationContext identityccontext;

        private readonly IMapper mapper;

        public PropiedadMejoraRepository(IMapper mapper, ApplicationContext context) : base(context)
        {
            identityccontext = context;
            this.mapper = mapper;
        }

        public async Task<UserDto> GetUserAccount(string iduser)
        {
            User user = await identityccontext.
                Set<User>().FirstOrDefaultAsync(p => p.Id == iduser);

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
