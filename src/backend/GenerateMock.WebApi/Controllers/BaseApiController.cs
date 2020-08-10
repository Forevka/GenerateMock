using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GenerateMock.Utilities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace GenerateMock.WebApi.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Current user
        /// </summary>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public Guid CurrentUser()
        {
            if (Guid.TryParse(User.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value, out Guid currentUserId))
                return currentUserId;

            throw ExceptionFactory.SoftException(ExceptionEnum.UserNotFound, "User not found while getting currentUser");
        }
    }
}
