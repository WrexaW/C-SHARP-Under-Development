using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication_C14.Dto;
using WebApplication_C14.Entities;
using WebApplication_C14.interfaces;
using WebApplication_C14.server;

namespace WebApplication_C14.Service
{
    public class UserService : IUserPRP
    {
        public UserDb _context = new UserDb();
        //private object _tokenService;

        public UserEntity Register(UserEntity input)
        {
            UserEntity _user = new UserEntity()
            {
                Age = input.Age ,
                Name = input.Name,
                UserName = input.UserName,
                Password = input.Password,
                Mobile = input.Mobile,

            };
           

            _context.Users.Add(_user);
            _context.SaveChanges();
            return _user;
        }


        public string Login(string username, string password)
        {
            UserEntity _user = new UserEntity();
            List<Claim> claims = new List<Claim>();
            string tokenString = " incorrect ";
            var user = _context.Users.FirstOrDefault(x => x.UserName == username && x.Password == password);

            if (user != null)
            {
                string secretKey = "dfgkjropjowepjforepit598uy68t59i40fklrmthblkmsfl;dfropw8ueowkopjkfmvkbmklhmo";
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: sign
                );
                tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            }

            return tokenString;
  
        }

        public UserEntity ShowProfile(int id)
        {
          var Prof =  _context.Users.FirstOrDefault( x => x.Id == id)!;

            UserEntity _user = new UserEntity()
            {
                UserName = Prof.UserName,
                Password = Prof.Password,
                Mobile = Prof.Mobile,
                Age = Prof.Age,
                Name = Prof.Name,

            };

            return Prof;
        }

        public UserEntity Update(UpdateDto input)
        {
 
          UserEntity userEntity = _context.Users.FirstOrDefault(x => x.Id == input.id)!;
            try
            {
                if (userEntity != null)
                {

                    userEntity.Age = input.age;
                    userEntity.Name = input.name;
                    userEntity.Mobile = input.mobile;
                    userEntity.Password = input.password;
                    _context.Users.Add(userEntity);
                    _context.SaveChanges();

                }
            }
            catch (Exception ex) { }

            return userEntity!;
        }

        
        public bool Delete(int id)
        {
           bool result = false;
            UserEntity user = new UserEntity()
            {
                Id = id,
            };
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;

        }

    }
}
