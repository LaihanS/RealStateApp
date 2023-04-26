using Banking.Infrastructure.Persistence.Repositories;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.IRepositories
{
    public interface IVentaTypeRepository: IGenericAppRepository<VentaType>
    {
        Task<UserDto> GetUserAccount(string iduser);
    }
}