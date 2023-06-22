using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRoleRepository
    {
        void SaveRole(Role p);

        Role GetRoleById(int id);

        void DeleteRole(Role p);

        void UpdateRole(Role p);

        List<Role> GetRoles();
    }
}
