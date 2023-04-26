
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Shared.EmailServices;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.User;
using RealStateApp.Core.Application.Dtos.Email;
using RealStateApp.Core.Application.Enums;
using Microsoft.Extensions.Options;
using RealStateApp.Core.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace RealStateApp.Infrastructure.Identity.Services
{
    public class AccountJWTService : IAccountJWTService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailService emailService;
        private readonly IMapper imapper;
        private readonly JWTSettings _jwtSettings;


        public AccountJWTService(IOptions<JWTSettings> jwtSettings, IMapper imapper, IEmailService emailService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.emailService = emailService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.imapper = imapper;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticationJWTResponse> AuthAsync(AuthenticationRequest request)
        {
            AuthenticationJWTResponse response = new();

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.ErrorDetails = $"No Accounts registered with {request.Email}";
                return response;
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.ErrorDetails = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.ErrorDetails = $"Account no confirmed for {request.Email}";
                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            response.id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.RoleList = rolesList.ToList();
            response.Verified = user.EmailConfirmed;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshTokens = refreshToken.Token;

            return response;
        }

        public async Task SingOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task ActivateOrInactivateUser(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if (user.EmailConfirmed == true)
            {
                user.EmailConfirmed = false;
            }
            else
            {
                user.EmailConfirmed = true;
            }

            await userManager.UpdateAsync(user);

        }

        public async Task<List<UserViewModel>> GetUsers(List<string> userids)
        {

            //List<User> userlist = new();
            List<UserViewModel> userlista = new();

            foreach (string item in userids)
            {
                User user = await userManager.FindByIdAsync(item);
                UserViewModel uservm = imapper.Map<UserViewModel>(user);
                uservm.RoleList = await userManager.GetRolesAsync(user);
                userlista.Add(uservm);
            }


            return userlista;

        }

        public async Task EditUser(UserViewModel edituser)
        {

            User user = await userManager.FindByIdAsync(edituser.Id);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.ResetPasswordAsync(user, token, edituser.Password);
        }

        //public async Task DeleteUserAsync(UserViewModel uservm)
        //{
        //    User user = imapper.Map<User>(uservm);

        //    await userManager.DeleteAsync(user);
        //}

        public async Task<string> ConfirmUserAsync(string userid, string token)
        {
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return "No hay cuentas registradas con este usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Mete mano {user.FirstName}";
            }
            else
            {
                return $"Error confirmando el usuario {user.FirstName}";
            }
        }

        public async Task<ForgotPassworResponse> ForgotPasswordAsync(ForgotPassworRequest request, string origin)
        {
            ForgotPassworResponse response = new()
            {
                HasError = false
            };

            var account = await userManager.FindByEmailAsync(request.Email);
            if (account == null)
            {
                response.HasError = true;
                response.ErrorDetails = $"No se jayó un usuario con {request.Email}";
                return response;
            }

            var verificationuri = await VerificationPasswordUriAsync(account, origin);
            await emailService.SendAsync(new EmailRequest()
            {
                To = account.Email,
                Body = $"{account.FirstName}, recupere su contraseña accediendo aquí: {verificationuri}",
                Subject = $"Restablecimiento de contraseña, {account.FirstName}",
            });

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest reset)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await userManager.FindByEmailAsync(reset.Email);

            if (user == null)
            {
                response.HasError = true;
                response.ErrorDetails = $"No se hayó cuenta con el correo: {reset.Email}";
                return response;
            }

            reset.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(reset.Token));
            var result = await userManager.ResetPasswordAsync(user, reset.Token, reset.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.ErrorDetails = $"Hubo un error al restablecer la contraseña. Revise los datos de su transaccion";
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin, bool IsAdmin)
        {
            RegisterResponse response = new();
            response.HasError = false;

            var userWithSameName = await userManager.FindByNameAsync(request.UserName);

            if (userWithSameName != null)
            {
                response.HasError = true;
                response.ErrorDetails = $"Ya existe un usuario con el username: {request.UserName}";
                return response;
            }
            var userWithSameEmail = await userManager.FindByEmailAsync(request.Email);



            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.ErrorDetails = $"Ya existe un usuario con el email: {request.Email}";
                return response;
            }

            var user = new User
            {
                UserName = request.UserName,
                FirstName = request.UserName,
                LastName = request.LastName,
                Email = request.Email,
                Cedula = request.Cedula,
                EmailConfirmed = true
            };

            var createuserresult = await userManager.CreateAsync(user, request.Password);
            if (createuserresult.Succeeded)
            {
                if (IsAdmin)
                {
                    await userManager.AddToRoleAsync(user, EnumRoles.Administrador.ToString());
                }
                else
                {
                    await userManager.AddToRoleAsync(user, EnumRoles.Desarrollador.ToString());
                }
                var verificationuri = await VerificationUriAsync(user, origin);
                //await emailService.SendAsync(new EmailRequest()
                //{
                //    To = user.Email,
                //    Body = $"{user.FirstName}, active su cuenta accediendo aquí: {verificationuri}",
                //    Subject = "Activación de cuenta",
                //});

                User ObtainUser = await userManager.FindByEmailAsync(user.Email);

                response.UserID = ObtainUser.Id;
            }
            else
            {
                response.HasError = true;
                response.ErrorDetails = $"No se ha podido crear el usuario";
                return response;
            }

            return response;
        }

        private async Task<JwtSecurityToken> GenerateJWToken(User user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmectricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredetials = new SigningCredentials(symmectricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredetials);

            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }


        private async Task<string> VerificationUriAsync(User user, string origin)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ConfirmEmail";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(uri.ToString(), "userid", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }

        private async Task<string> VerificationPasswordUriAsync(User user, string origin)
        {
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationstring = QueryHelpers.AddQueryString(uri.ToString(), "Token", code);

            return verificationstring;
        }

    }
}
