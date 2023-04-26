using AutoMapper;
using Banking.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Dtos.ImportantDto;
using RealStateApp.Core.Application.Enums;
using RealStateApp.Core.Application.Helpers;
using RealStateApp.Core.Application.IRepositories;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Application.ViewModels;
using RealStateApp.Core.Application.ViewModels.Home;
using RealStateApp.Core.Application.ViewModels.Propiedades;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper imapper;
        private readonly IUserRepository userRepository;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        AuthenticationResponse User = new();

        public UserService(IPropiedadRepository propiedadRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IMapper imapper, IAccountService accountService)
        {
            this.propiedadRepository = propiedadRepository;
            this.httpContextAccessor = httpContextAccessor;
            User = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            this.userRepository = userRepository;
            this.imapper = imapper;
            _accountService = accountService;
        }


        public async Task<List<UserViewModel>> GetAllUsersAsync()
        {
            List<UserViewModel> userlist = imapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            return userlist;

        }

        public async Task<HomeViewModel> GetHomeViewModel()
        {
            HomeViewModel homeview = new();
            List<UserViewModel> userlist = imapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            List<string> userids = new();

            foreach (UserViewModel viewmodel in userlist)
            {
                userids.Add(viewmodel.Id);
            }

            List<UserViewModel> userlistas = await _accountService.GetUsers(userids);
            List<UserViewModel> users = userlistas.Where(users => users.Id != User.id).ToList();
            if (users != null)
            {
                homeview.ClientesActivos = users.Where(user => user.RoleList.Any(rol => rol.Equals(EnumRoles.Cliente.ToString())) && user.EmailConfirmed).ToList();
                homeview.ClientesInactivos = users.Where(user => user.RoleList.Any(rol => rol.Equals(EnumRoles.Cliente.ToString())) && !user.EmailConfirmed).ToList();
                homeview.AgentesActivos = users.Where(user => user.RoleList.Any(rol => rol.Equals(EnumRoles.Agente.ToString())) && user.EmailConfirmed).ToList();
                homeview.AgentesInactivos = users.Where(user => user.RoleList.Any(rol => rol.Equals(EnumRoles.Agente.ToString())) && !user.EmailConfirmed).ToList();
                homeview.DesarrolladoresActivos = users.Where(user => user.RoleList.Any(rol => rol.Equals(EnumRoles.Desarrollador.ToString())) && user.EmailConfirmed).ToList();
                homeview.DesarrolladoresInactivos = users.Where(user => user.RoleList.Any(rol => rol.Equals(EnumRoles.Desarrollador.ToString())) && !user.EmailConfirmed).ToList();
                homeview.Propiedades = imapper.Map<List<PropiedadViewModel>>(await propiedadRepository.GetAsync());

            }

            return homeview;

        }

        public async Task<List<UserViewModel>> ActiveClients()
        {
            List<UserViewModel> userlist = imapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            List<string> userids = new();

            if (userlist != null)
            {
                foreach (UserViewModel viewmodel in userlist)
                {
                    userids.Add(viewmodel.Id);
                }
            }

            List<UserViewModel> userlistas = await _accountService.GetUsers(userids);

            List<UserViewModel> clients = new();

            clients = userlistas.Where(u => u.EmailConfirmed == true && u.RoleList.Any(r => r.Equals(EnumRoles.Cliente.ToString()))).ToList();

            return clients;
        }


        public async Task<List<UserViewModel>> UnactiveClients()
        {
            List<UserViewModel> userlist = imapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            List<string> userids = new();

            if (userlist != null)
            {
                foreach (UserViewModel viewmodel in userlist)
                {
                    userids.Add(viewmodel.Id);
                }
            }

            List<UserViewModel> userlistas = await _accountService.GetUsers(userids);

            List<UserViewModel> clients = new();

            clients = userlistas.Where(u => u.EmailConfirmed == false && u.RoleList.Any(r => r.Equals(EnumRoles.Cliente.ToString()))).ToList();

            return clients;

        }


        public async Task<List<UserViewModel>> GetAllUsersAsyncJoinedFiltered(FilterViewModel filter)
        {
             List<UserViewModel> userlist = imapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            List<string> userids = new();

            foreach (UserViewModel viewmodel in userlist)
            {
                userids.Add(viewmodel.Id);
            }
            List<UserViewModel> users = new();
            List <UserViewModel> userlistas = await _accountService.GetUsers(userids);

            if (User != null)
            {
                users = userlistas.Where(use => use.Id != User.id).ToList();
            }
            else
            {
                users = userlistas.ToList();
            }

            if (filter.NombreAgente != null)
            {
                users = users.Where(prope => prope.FirstName == filter.NombreAgente).ToList();
            }
            return users;

        }
        public async Task<List<UserViewModel>> GetAllUsersAsyncJoined()
        {
            List<UserViewModel> userlist = imapper.Map<List<UserViewModel>>(await userRepository.GetAsync());
            List<string> userids = new();

            foreach (UserViewModel viewmodel in userlist)
            {
                userids.Add(viewmodel.Id);
            }
            List<UserViewModel> users = new();
            List<UserViewModel> userlistas = await _accountService.GetUsers(userids);

            if (User != null)
            {
                users = userlistas.Where(use => use.Id != User.id).ToList();
            }
            else
            {
                users = userlistas.ToList();
            }

            return users;

        }

        public async Task ActivateOrInactivate(string id)
        {
            await _accountService.ActivateOrInactivateUser(id);
        }

        public async Task ActivateOrInactivateManually(string id, bool active)
        {
            await _accountService.ActivateOrInactivateUserManually(id, active);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel loginvm)
        {

            AuthenticationRequest loginrequest = imapper.Map<AuthenticationRequest>(loginvm);

            AuthenticationResponse authenticationResponse = await _accountService.AuthAsync(loginrequest);

            return authenticationResponse;

        }

        public async Task SignOutAsync()
        {
            await _accountService.SingOutAsync();
        }

        public async Task DeleteUserAsync(UserViewModel user)
        {
            List<Propiedades> propiedades = await propiedadRepository.GetAsync();
            UserViewModel usuarioage = await _accountService.FindByIdAsync(user.Id);
            if (usuarioage.RoleList.Any(r => r.Equals(EnumRoles.Agente.ToString())))
            {
                if (propiedades != null)
                {
                    propiedades = propiedades.Where(res => res.AgenteId == user.Id).ToList();
                    foreach (Propiedades item in propiedades)
                    {
                        await propiedadRepository.DeleteAsync(item);
                    }
                }

            }
            await userRepository.DeleteAsync(imapper.Map<UserDto>(user), user.Id);


        }

        public async Task<RegisterResponse> RegisterAdminOrDev(SaveUserViewModel saveuservm)
        {
            Sequence6Digit sequence6Digit = new();

            RegisterRequest registerRequest = imapper.Map<RegisterRequest>(saveuservm);

            RegisterResponse response = await _accountService.RegisterDevOrAdmin(registerRequest, saveuservm.RoleList);

            return response;
        }

        public async Task<UserViewModel> GetUserByIdWithRoles(string Id)
        {
            return await _accountService.FindByIdAsync(Id);
        }

        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveuservm, string origin)
        {
            Sequence6Digit sequence6Digit = new();

            RegisterRequest registerRequest = imapper.Map<RegisterRequest>(saveuservm);

            RegisterResponse response = await _accountService.RegisterBasicUserAsync(registerRequest, origin, saveuservm.IsAdmin);

            //if (!saveuservm.IsAdmin && response.HasError != true)
            //{
            //    List<Products> products = await productRepository.GetAsync();

            //    List<string> sequences = products.Select(p => p.UnicDigitSequence).ToList();

            //    Products product = new();
            //    product.UnicDigitSequence = sequence6Digit.Secuencia(sequences);
            //    product.IsPrincipalAccount = true;
            //    product.ProductAmount = saveuservm.Monto;
            //    product.ProductType = ProductTypeEnum.Cuenta_Ahorro.ToString();
            //    product.UserID = response.UserID;

            //    Products respones =  await productRepository.AddAsync(product);

            //    Products product0k = respones;

            //}

            return response;
        }

        public async Task<SaveUserViewModel> EditUser(SaveUserViewModel uservmsave)
        {

            UserViewModel uservm = await _accountService.FindByEmailAsync(uservmsave.Email);
            if (uservm != null && uservm.Id != uservmsave.Id) 
            {
                uservmsave.ErrorDetails = "Ya hay un usuario con ese correo";
                uservmsave.HasError = true;
                return uservmsave;
            }
            UserDto user = imapper.Map<UserDto>(uservmsave);

            await userRepository.EditAsync(user, uservmsave.Id);

            if (uservm.RoleList.Any(rol => rol.Equals(EnumRoles.Agente.ToString())) && User == null)
            {
                await _accountService.EditUser(imapper.Map<UserViewModel>(uservmsave));
                return uservmsave;
            }

            else if (uservm.RoleList.Any(rol => rol.Equals(EnumRoles.Cliente.ToString())) && User == null)
            {
                await _accountService.EditUser(imapper.Map<UserViewModel>(uservmsave));
                return uservmsave;
            }

            await _accountService.EditUser(imapper.Map<UserViewModel>(uservmsave));

            return uservmsave;
        }

        public async Task<SaveUserViewModel> GetEditAsync(string id)
        {
            UserViewModel user = imapper.Map<UserViewModel>(await userRepository.GetByidAsync(id));

            return imapper.Map<SaveUserViewModel>(user);
        }


        public async Task<string> ConfirmAsync(string userid, string token)
        {
            return await _accountService.ConfirmUserAsync(userid, token);
        }

        public async Task<ForgotPassworResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordvm, string origin)
        {
            ForgotPassworRequest forgotPassworRequest = imapper.Map<ForgotPassworRequest>(forgotPasswordvm);

            return await _accountService.ForgotPasswordAsync(forgotPassworRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPasswordvm, string origin)
        {
            ResetPasswordRequest resetPassworRequest = imapper.Map<ResetPasswordRequest>(resetPasswordvm);

            return await _accountService.ResetPasswordAsync(resetPassworRequest);
        }
    }
}

