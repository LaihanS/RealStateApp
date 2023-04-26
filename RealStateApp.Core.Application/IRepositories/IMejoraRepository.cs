using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.IRepositories
{
    public interface IMejoraRepository: IGenericAppRepository<Mejora>
    {
        Task<UserDto> GetUserAccount(string iduser);
    }
}