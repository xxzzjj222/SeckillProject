using Projects.UserServices.Models;
using Projects.UserServices.Reposotories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.UserServices.Services
{
    public class UserServiceImpl:IUserService
    {
        public readonly IUserRepository UserRepository;

        public UserServiceImpl(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }

        public void Create(User User)
        {
            UserRepository.Create(User);
        }

        public void Delete(User User)
        {
            UserRepository.Delete(User);
        }

        public User GetUser(string UserName)
        {
            return UserRepository.GetUser(UserName);
        }

        public User GetUserById(int id)
        {
            return UserRepository.GetUserById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return UserRepository.GetUsers();
        }

        public void Update(User User)
        {
            UserRepository.Update(User);
        }

        public bool UserExists(int id)
        {
            return UserRepository.UserExists(id);
        }

        public bool UserNameExists(string UserName)
        {
            return UserRepository.UserNameExists(UserName);
        }
    }
}
