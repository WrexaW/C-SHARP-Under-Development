using WebApplication_C14.Dto;
using WebApplication_C14.Entities;

namespace WebApplication_C14.interfaces
{
    public interface IUserPRP
    {
        public UserEntity Register(UserEntity input);
        public UserEntity Update(UpdateDto input);
        public bool Delete(int id);
        public string Login(string username , string password);
        public UserEntity ShowProfile(int id);
    }
}
