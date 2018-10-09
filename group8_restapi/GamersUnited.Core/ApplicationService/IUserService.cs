using GamersUnited.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.ApplicationService
{
    public interface IUserService
    {
        User Add(User user);
        User GetById(int id);
        IList<User> GetAll();
        User Update(int id, User user);
        User Remove(User user);
    }
}
