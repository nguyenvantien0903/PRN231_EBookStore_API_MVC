using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public void DeleteRole(Role p) => RoleDAO.DeleteRole(p);

        public Role GetRoleById(int id) => RoleDAO.FindRoleById(id);

        public List<Role> GetRoles() => RoleDAO.GetRoles();

        public void SaveRole(Role p) => RoleDAO.SaveRole(p);

        public void UpdateRole(Role p) => RoleDAO.UpdateRole(p);
    }
}
