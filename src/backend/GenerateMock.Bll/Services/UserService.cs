using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GenerateMock.Dal.Context;
using GenerateMock.Dal.Models.DB;
using GenerateMock.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GenerateMock.Bll.Services
{
    public class UserService
    {
        private readonly PublicContext _publicContext;

        public UserService(PublicContext publicContext)
        {
            _publicContext = publicContext;
        }

        /*public async Task<UserDb> AddUserIfNotExist(string userName)
        {
            var user = await _publicContext.User.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
            {
                user = new UserDb
                {
                    UserId = Guid.NewGuid(),
                    Username = userName,
                };

                await _publicContext.User.AddAsync(user);
                await _publicContext.SaveChangesAsync();
            }

            return user;
        }*/

        /*public async Task<UserDb> GetUser(string userName)
        {
            var user = await _publicContext.User.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null)
                throw ExceptionFactory.SoftException(ExceptionEnum.UserNotFound, "User not found");

            return user;
        }*/
    }
}
