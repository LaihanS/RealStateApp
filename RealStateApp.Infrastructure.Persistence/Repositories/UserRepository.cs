using AutoMapper;
using Banking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Infrastructure.Identity.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserDto, RealStateApp.Infrastructure.Identity.Entities.User>, IUserRepository
    {
        public readonly IdentityContext identityccontext;

        private readonly IMapper mapper;

        public UserRepository(IMapper mapper, IdentityContext context) : base(mapper, context)
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
