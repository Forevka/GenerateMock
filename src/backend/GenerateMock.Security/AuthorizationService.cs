using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using GenerateMock.Dal.Context;
using GenerateMock.Dal.Models.DB;
using GenerateMock.Security.Options;
using GenerateMock.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GenerateMock.Security
{
    public class AuthenticationService
    {
        private readonly PublicContext _context;
        private readonly PasswordHasherService _passwordHasherService;

        public AuthenticationService(PublicContext publicContext, PasswordHasherService passwordHasherService)
        {
            _context = publicContext;
            _passwordHasherService = passwordHasherService;
        }

        public async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var user = await _context.User.Include(x => x.UserSecurity)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserSecurity.Login == login);

            if (user?.UserSecurity == null)
                throw ExceptionFactory.SoftException(ExceptionEnum.UserNotFound, $"User with login {login} not found");

            if (!_passwordHasherService.Check(user.UserSecurity.Password, password).Verified)
                throw ExceptionFactory.SoftException(ExceptionEnum.PasswordIncorrect, "Password is incorrect");

            /*Ignore user not confirmed email because they have role preMember
             if (user.UserSecurity.IsConfirmed == false)
                throw ExceptionFactory.SoftException(ExceptionEnum.EmailNotConfirmed, $"Please confirm your email. Check spam folder!");*/

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.UserSecurity.Role.RoleName),
            };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public string GenerateToken(ClaimsIdentity claims)
        {
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: claims.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<UserDb> GetUser(Guid userId)
        {
            return await _context.User.Include(x => x.UserSecurity).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}