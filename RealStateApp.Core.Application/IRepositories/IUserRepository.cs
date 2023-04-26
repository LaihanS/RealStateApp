using Banking.Infrastructure.Persistence.Repositories;
using RealStateApp.Core.Application.Dtos.ImportantDto;

namespace RealStateApp.Core.Application.IRepositories
{
    public interface IUserRepository: IGenericRepository<UserDto, User>
    {
        Task<UserDto> GetUserAccount(string iduser);
    }
}