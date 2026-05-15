using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using AGR.Apps.Core.Interfaces;
using AGR.Apps.Core.Models;
using AGR.DataAccess.EFCore.Interfaces;
using AGR.DataAccess.EFCore.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace AGR.Apps.Core.Services
{
    public partial class IdentityService : EFRepository, IIdentityService
    {
        #region Private Variables
        private readonly IRepository repository;
        #endregion                
        public IdentityService(string connString, IRepository repository) : base(connString)
        {
            this.repository = repository;
        }

        public AuthenticationResult Authecticate(UserModel user)
        {
            //setting configuration TokenLifetime, Secret and issuer
            TimeSpan TokenLifetime = TimeSpan.Parse("08:30:45");
            string plainTextSecurityKey = "hKENZygugWwQogvF21EktUzzn5kReKUNPVGPDFQC6Sc="; //Secret

            //define authetication result
            AuthenticationResult result = new AuthenticationResult();
            //define token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

                //define claim identity
                ClaimsIdentity subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Username", user.Username),
                    new Claim("FullName", user.FullName),
                    new Claim("RoleCode", user.RoleCode),
                    new Claim("Email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                });

                //define role from get roles
                foreach (var item in GetRoles(user))
                {
                    subject.AddClaim(new Claim(ClaimTypes.Role, item.RoleCode));
                }

                //define token descriptor
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = subject,
                    Expires = DateTime.UtcNow.Add(TokenLifetime),
                    SigningCredentials = credentials
                    //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                //create token
                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.Token = tokenHandler.WriteToken(token);
                result.RefreshToken = Guid.NewGuid().ToString();
                result.Success = true;

                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }

        private List<RoleModel> GetRoles(UserModel user)
        {
            try
            {
                string query = @"SELECT RoleCode = APP_CODE, RoleName = APP_NAME 
                                FROM  SECURITY.dbo.V_ROLE_APPS 
                                WHERE ROLE_CODE = @RoleCode
                                ORDER BY APP_NAME";
                var param = new Dictionary<string, object>
                {
                    {"@RoleCode", user.RoleCode }
                };

                //conn.QueryString = query;
                var data = repository.QueryList<RoleModel>(query, param, false);
                return data;
            }
            catch (Exception)
            {

                return new List<RoleModel>();
            }
        }

        public ResponseModel<TokenModel> GenerateToken(string username)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();

            try
            {
                string query = @"SELECT
                                Username = a.CODE,
                                FullName = UPPER(ISNULL(a.FRONT_NAME,'') + REPLACE(' ' + ISNULL(MID_NAME,'') + ' ','  ',' ') + ISNULL(a.LAST_NAME,'')),
                                RoleCode = a.ROLE_CODE,
                                Email = a.EMAIL
                                FROM SECURITY.dbo.M_USERS a
                                INNER JOIN SECURITY.dbo.USER_LOG_HISTORY b on a.CODE=b.USER_CODE
                                WHERE
                                b.ROWID = @SessionId
                                AND isnull(a.ACTIVE,0) > 0
                                --AND a.CODE = 'johan'
                                AND b.LOGOUT IS NULL";
                var param = new Dictionary<string, object>
                {
                    {"@SessionId", username }
                };
                //conn.QueryString = query;
                var data = repository.QueryList<UserModel>(query, param, false);
                var user = data.FirstOrDefault();

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Username not found";

                    return response;
                }

                //define authenticate token
                AuthenticationResult authenticationResult = Authecticate(user);
                if (authenticationResult != null && authenticationResult.Success)
                {
                    response.Data = new TokenModel
                    {
                        Token = authenticationResult.Token,
                        RefreshToken = authenticationResult.RefreshToken
                    };
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }

                //HttpContext.Session.SetString("JwToken", token);

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ResponseModel<TokenModel> GenerateTokenBySessionid(string sessionId)
        {
            ResponseModel<TokenModel> response = new ResponseModel<TokenModel>();

            try
            {
                string query = @"SELECT
                                Username = a.CODE,
                                FullName = UPPER(ISNULL(a.FRONT_NAME,'') + REPLACE(' ' + ISNULL(MID_NAME,'') + ' ','  ',' ') + ISNULL(a.LAST_NAME,'')),
                                RoleCode = a.ROLE_CODE,
                                Email = a.EMAIL
                                FROM SECURITY.dbo.M_USERS a
                                INNER JOIN SECURITY.dbo.USER_LOG_HISTORY b on a.CODE=b.USER_CODE
                                WHERE
                                b.ROWID = @SessionId
                                AND isnull(a.ACTIVE,0) > 0
                                --AND a.CODE = 'johan'
                                AND b.LOGOUT IS NULL";
                var param = new Dictionary<string, object>
                {
                    {"@SessionId", sessionId }
                };
                //conn.QueryString = query;
                var data = repository.QueryList<UserModel>(query, param, false);
                var user = data.FirstOrDefault();

                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Username not found";

                    return response;
                }

                //define authenticate token
                AuthenticationResult authenticationResult = Authecticate(user);
                if (authenticationResult != null && authenticationResult.Success)
                {
                    response.Data = new TokenModel
                    {
                        Token = authenticationResult.Token,
                        RefreshToken = authenticationResult.RefreshToken
                    };
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                }

                //HttpContext.Session.SetString("JwToken", token);

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}