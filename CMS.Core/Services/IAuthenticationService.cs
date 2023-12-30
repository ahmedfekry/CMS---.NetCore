using CMS.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Services
{
    public interface IAuthenticationService
    {
        public Task<AuthModel> RegisterUserAsync(RegisterModel registerModel);

        public Task<AuthModel> LoginUserAsync(LoginModel loginModel);
    }
}
