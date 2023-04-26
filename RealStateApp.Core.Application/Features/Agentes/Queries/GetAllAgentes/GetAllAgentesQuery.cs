using AutoMapper;
using Banking.Core.Application.Helpers;
using MediatR;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Features.Mejoras.Commands.UpdateMejorasCommand;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels.Mejora;
using RealStateApp.Core.Application.ViewModels.PropertyType;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.ViewModels.Venta;
using RealStateApp.Core.Application.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.Mejoras.Queries.GetAllAgentesQuery
{

    public class GetAllAgentesQuery : IRequest<Response<IEnumerable<UserViewModel>>>
    {
    }
    public class GetAllAgentesQueryHandler : IRequestHandler<GetAllAgentesQuery, Response<IEnumerable<UserViewModel>>>
    {
        private readonly IMapper mapper;
        Sequence6Digit sequence6Digit = new();
        private readonly IMejoraRepository mejoraRepository;
        private readonly IUserRepository userRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly IPropiedadMejoraRepository propiedadMejoraRepository;

        public GetAllAgentesQueryHandler(IUserRepository userRepository, IAccountService accountService, IMejoraRepository mejoraRepository, IUserService userService, IPropiedadMejoraRepository propiedadMejoraRepository, IMapper mapper, IPropiedadRepository propiedadRepository)
        {
            this.mejoraRepository = mejoraRepository;
            this.userService = userService;
            this.propiedadMejoraRepository = propiedadMejoraRepository;
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
            this.accountService = accountService;
            this.userRepository = userRepository;
        }

        public async Task<Response<IEnumerable<UserViewModel>>> Handle(GetAllAgentesQuery request, CancellationToken cancellationToken)
        {
            var categoriesViewModel = await GetAllUsersAsyncJoined();
            if (categoriesViewModel == null || categoriesViewModel.Count() == 0) throw new ApiExceptions("No hay agentes", (int)HttpStatusCode.NotFound);
            return new Response<IEnumerable<UserViewModel>>(categoriesViewModel);
        }

        private async Task<List<UserViewModel>> GetAllUsersAsyncJoined()
        {
            List<UserViewModel> userlist = mapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            List<string> userids = new();

            foreach (UserViewModel viewmodel in userlist)
            {
                userids.Add(viewmodel.Id);
            }
            List<UserViewModel> users = new();
            List<UserViewModel> userlistas = await accountService.GetUsers(userids);

           
            users = userlistas.ToList();
            users = users.Where(r => r.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString()))).ToList();
            List<RealStateApp.Core.Domain.Entities.Propiedades> props = await propiedadRepository.GetAsync();

            foreach (UserViewModel item in users)
            {
                item.CantidadPropiedades = props.Where(p => p.AgenteId == item.Id).Count();
            }

            return users;

        }
    }
}
