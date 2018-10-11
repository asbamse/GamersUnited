using System;
using System.Collections.Generic;
using System.Text;

namespace GamersUnited.Core.DomainService
{
    public interface ILoginValidation
    {
        Boolean ValidateLoginInformation(string email, string password);
    }
}
