﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        void SaveUser(User p);

        User GetUserById(int id);

        void DeleteUser(User p);

        void UpdateUser(User p);

        List<User> GetUsers();
    }
}
