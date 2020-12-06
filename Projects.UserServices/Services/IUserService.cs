using Projects.UserServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.UserServices.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(string UserName);
        User GetUserById(int id);
        void Create(User User);
        void Update(User User);
        void Delete(User User);
        bool UserExists(int id);
        bool UserNameExists(string UserName);
    }
}
