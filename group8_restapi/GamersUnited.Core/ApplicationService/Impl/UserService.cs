using System;
using System.Collections.Generic;
using System.Text;
using GamersUnited.Core.ApplicationService.Impl.Utilities;
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
            ValidateData(user);
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
            ValidateData(user);
            return _ur.Update(id, user);
        }

        private void ValidateData(User user)
        {
            Requirement.MinLength(2, user.FirstName, "FirstName");
            Requirement.MaxLength(50, user.FirstName, "FirstName");

            Requirement.MinLength(2, user.LastName, "LastName");
            Requirement.MaxLength(50, user.LastName, "LastName");

            Requirement.MinLength(2, user.Address, "Address");
            Requirement.MaxLength(50, user.Address, "Address");

            Requirement.Email(user.Email, "Email");

            Requirement.DanishPhoneNumber(user.PhoneNumber, "PhoneNumber");

            Requirement.MinLength(8, user.Password, "Password");
            user.Password = Cryptography.Encrypt(user.Password);
        }
    }
}
