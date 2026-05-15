using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGR.Apps.Core.Models;
using AGR.DataAccess.EFCore.Interfaces;

namespace AGR.Apps.Core.Interfaces
{
    public interface IIdentityService : IRepository
    {
        ResponseModel<TokenModel> GenerateToken(string username);
        ResponseModel<TokenModel> GenerateTokenBySessionid(string sessionId);
        AuthenticationResult Authecticate(UserModel user);
    }
}
