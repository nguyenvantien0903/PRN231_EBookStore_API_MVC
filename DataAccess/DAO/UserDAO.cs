using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        public static List<User> GetUsers()
        {
            var listUsers = new List<User>();
            try
            {
                using var context = new eBookStoreDBContext();
                listUsers = context.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listUsers;
        }

        public static User FindUserById(int prodId)
        {
            User p = new();
            try
            {
                using var context = new eBookStoreDBContext();
                p = context.Users.SingleOrDefault(x => x.UserId == prodId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static void SaveUser(User User)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Users.Add(User);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateUser(User User)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Entry<User>(User).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteUser(User User)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                var p1 = context.Users.SingleOrDefault(
                    x => x.UserId == User.UserId);
                context.Users.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static User AuthenticateUser(string email,string password)
        {
            var user = new User();
            try
            {
                using (var context = new eBookStoreDBContext())
                {
                    user = context.Users.Where(s => s.Email_adress == email && s.Password == password).Include(s => s.Role).Include(s => s.Publisher)
                        .FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        internal static User GetUserByEmail(string email)
        {
            var user = new User();
            try
            {
                using (var context = new eBookStoreDBContext())
                {
                    user = context.Users.Where(s => s.Email_adress == email).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }
    }
}
