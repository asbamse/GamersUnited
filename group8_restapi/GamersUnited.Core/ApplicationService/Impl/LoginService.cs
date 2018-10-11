using System;
using System.Collections.Generic;
using System.Text;
using GamersUnited.Core.ApplicationService.Impl.Utilities;
using GamersUnited.Core.DomainService;
using GamersUnited.Core.Entities;

namespace GamersUnited.Core.ApplicationService.Impl
{
    public class LoginService : ILoginService
    {
        private readonly ILoginValidation _lv;

        public LoginService(ILoginValidation loginValidation)
        {
            _lv = loginValidation;
        }

        public bool ValidateLoginInformation(string email, string password)
        {
            Requirement.Email(email, "Email");

            Requirement.MinLength(8, password, "Password");
            password = Cryptography.Encrypt(password);

            return _lv.ValidateLoginInformation(email, password);
        }
    }
}
