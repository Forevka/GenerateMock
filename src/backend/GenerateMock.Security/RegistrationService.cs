using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GenerateMock.Dal.Context;
using GenerateMock.Dal.Models.DB;
using GenerateMock.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GenerateMock.Security
{
    public class RegistrationService
    {
        private readonly PublicContext _publicContext;
        private readonly PasswordHasherService _passwordHasherService;

        private readonly Regex _hasNumber;
        private readonly Regex _hasUpperChar;
        private readonly Regex _hasMinimum8Chars;

        public RegistrationService(PublicContext publicContext, PasswordHasherService passwordHasherService)
        {
            _publicContext = publicContext;
            _passwordHasherService = passwordHasherService;

            _hasNumber = new Regex(@"[0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _hasUpperChar = new Regex(@"[A-Z]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _hasMinimum8Chars = new Regex(@".{8,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public async Task<UserDb> Register(string login, string password)
        {
            var isPasswordValid = _hasNumber.IsMatch(password) && _hasUpperChar.IsMatch(password) && _hasMinimum8Chars.IsMatch(password);

            if (!isPasswordValid)
                throw ExceptionFactory.SoftException(ExceptionEnum.PasswordInvalid, $"Password need to follow:\n\tMinimum 8 char\n\tHave at least one number\n\tHave upper char");


            if (await _publicContext.UserSecurity.FirstOrDefaultAsync(x => x.Login == login) != null)
                throw ExceptionFactory.SoftException(ExceptionEnum.UserAlreadyExist,
                    $"User with login {login} already exist");

            UserDb userModel = new UserDb
            {
                UserSecurity = new UserSecurity
                {
                    Login = login,
                    Password = _passwordHasherService.Hash(password),
                    Role = await _publicContext.Role.FirstOrDefaultAsync(x => x.RoleName == "Member"),
                },
                CreatedDate = DateTime.UtcNow,
            };

            await _publicContext.User.AddAsync(userModel);
            await _publicContext.SaveChangesAsync();

            return userModel;
        }
    }
}
