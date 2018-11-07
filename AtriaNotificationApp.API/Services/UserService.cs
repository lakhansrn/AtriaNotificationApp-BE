using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using AtriaNotificationApp.API.Settings;
using AtriaNotificationApp.BL.Services;
using AtriaNotificationApp.DAL.Entities;
using AtriaNotificationApp.DAL.Interfaces;
using AtriaNotificationApp.DAL.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace AtriaNotificationApp.API.Services
{
    public class UserService :IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserDto> _users = new List<UserDto>
        { 
            new UserDto { Id = 1, FirstName = "Test", LastName = "User", Email = "test", Password = "test", Role="announcer" } 
        };

        private readonly AppSettings _appSettings;
        private readonly IUserAggregateRepository userRepository;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            userRepository = new UserAggregateRepository();
        }


/// <summary>
/// 
/// </summary>
/// <param name="username"></param>
/// <param name="password"></param>
/// <returns></returns>
        public async Task<User> Authenticate(string email, string password)
        {
            var userDetails = await userRepository.Authenticate(email, password);
            if (userDetails == null)
                return null;
            User user = userDetails;
            var pwd = new PasswordService();
            var hashedPassword = user.Password;
            bool isVerified = pwd.VerifyHashedPassword(hashedPassword, password);

            // return null if password is not correct
            if (!isVerified)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public IEnumerable<UserDto> GetAll()
        {
            // return users without passwords
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }

        public Task<string> checkIfUserExists(string email, string pno)
        {
            var user = userRepository.CheckIfUserExists(email, pno);

            return user;
        }

        public Task<User> RegisterUser(User user)
        {
            var pwd = new PasswordService();
            var password = user.Password;
            user.Password = pwd.HashPassword(password);
            var userDetails = userRepository.RegisterUser(user);
            return userDetails;
        }

        public async Task<User> GetUserAsync(Guid userid)
        {
            var user = await userRepository.GetUserAsync(userid);
            user.Password = null;
            return user;
        }
    }
}