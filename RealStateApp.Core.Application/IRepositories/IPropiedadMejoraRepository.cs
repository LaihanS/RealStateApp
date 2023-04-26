using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.IRepositories
{
    public interface IPropiedadMejoraRepository: IGenericAppRepository<PropiedadMejora>
    {
        Task<UserDto> GetUserAccount(string iduser);
    }
}