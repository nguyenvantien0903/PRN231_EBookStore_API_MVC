using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        public void DeleteUser(User p) => UserDAO.DeleteUser(p);

        public User GetUserById(int id) => UserDAO.FindUserById(id);

        public List<User> GetUsers() => UserDAO.GetUsers();

        public void SaveUser(User p) => UserDAO.SaveUser(p);

        public void UpdateUser(User p) => UserDAO.UpdateUser(p);
    }
}
