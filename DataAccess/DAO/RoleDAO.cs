using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoleDAO
    {
        public static List<Role> GetRoles()
        {
            var listRoles = new List<Role>();
            try
            {
                using var context = new eBookStoreDBContext();
                listRoles = context.Roles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listRoles;
        }

        public static Role FindRoleById(int prodId)
        {
            Role p = new();
            try
            {
                using var context = new eBookStoreDBContext();
                p = context.Roles.SingleOrDefault(x => x.RoleId == prodId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return p;
        }

        public static void SaveRole(Role Role)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Roles.Add(Role);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateRole(Role Role)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                context.Entry<Role>(Role).State =
                    Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteRole(Role Role)
        {
            try
            {
                using var context = new eBookStoreDBContext();
                var p1 = context.Roles.SingleOrDefault(
                    x => x.RoleId == Role.RoleId);
                context.Roles.Remove(p1);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
