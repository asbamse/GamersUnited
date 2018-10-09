using System;
using System.Collections.Generic;
using System.Text;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService.Impl
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _ur;

        public UserService(IRepository<User> userRepository)
        {
            _ur = userRepository;
        }

        public User Add(User user)
        {
            return _ur.Add(user);
        }

        public IList<User> GetAll()
        {
            return _ur.GetAll();
        }

        public User GetById(int id)
        {
            return _ur.GetById(id);
        }

        public User Remove(User user)
        {
            return _ur.Remove(user);
        }

        public User Update(int id, User user)
        {
            return _ur.Update(id, user);
        }
    }
}
