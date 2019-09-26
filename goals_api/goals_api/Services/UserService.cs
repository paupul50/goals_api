using goals_api.Helpers;
using goals_api.Models;
using goals_api.Models.DataContext;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace goals_api.Services
{
    public class UserService : IUserService
    {
        private AppSettings _appSettings;
        DataContext _dataContext;

        public UserService(IOptions<AppSettings> appSettings, DataContext dataContext)
        {
            _appSettings = appSettings.Value;
            _dataContext = dataContext;
        }

        public User Authenticate(string username, string password)
        {
            var user = _dataContext.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
                return null;

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isPasswordValid)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            if (user.Token != null && tokenHandler.ReadToken(user.Token).ValidTo > DateTime.UtcNow)
            {
                return user;
            }
            
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            _dataContext.Update(user);
            _dataContext.SaveChanges();

            return user;
        }
    }
}
