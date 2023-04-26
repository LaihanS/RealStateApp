using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.IRepositories
{
    public interface IPropiedadRepository: IGenericAppRepository<Propiedades>
    {
        Task<UserDto> GetUserAccount(string iduser);
        Task<List<PropiedadViewModel>> GetAsyncWithJoinNoGeneric();
    }
}