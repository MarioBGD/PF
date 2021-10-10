using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.Common.Validators
{
    public class AuthValidator
    {
        public void ValidateLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ApplicationException("Nie podano loginu");

            if (login.Length < 6 || login.Length > 40)
                throw new ApplicationException("Dlugosc loginu powinna byc miedzy 6 a 40 znakow");

            if (!login.Contains('@') || !login.Contains('.'))
                throw new ApplicationException("nie prawidlowy format maila");
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ApplicationException("Nie podano hasla");

            if (password.Length < 6 || password.Length > 40)
                throw new ApplicationException("Dlugosc hasla powinna byc miedzy 6 a 40 znakow");
        }

        public void NameValidation(string name)
        {

        }
    }
}
