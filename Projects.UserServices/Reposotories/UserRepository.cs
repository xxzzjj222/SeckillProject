using Projects.UserServices.Context;
using Projects.UserServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.UserServices.Reposotories
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public UserContext UserContext;
        public UserRepository(UserContext UserContext)
        {
            this.UserContext = UserContext;
        }
        public void Create(User User)
        {
            UserContext.Users.Add(User);
            UserContext.SaveChanges();
        }

        public void Delete(User User)
        {
            UserContext.Users.Remove(User);
            UserContext.SaveChanges();
        }

        public User GetUser(string UserName)
        {
            User user = UserContext.Users.First(u => u.UserName == UserName);

            return user;
        }

        public User GetUserById(int id)
        {
            return UserContext.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return UserContext.Users.ToList();
        }

        public void Update(User User)
        {
            UserContext.Users.Update(User);
            UserContext.SaveChanges();
        }
        public bool UserExists(int id)
        {
            return UserContext.Users.Any(e => e.Id == id);
        }

        public bool UserNameExists(string UserName)
        {
            return UserContext.Users.Any(e => e.UserName == UserName);
        }
    }
}
