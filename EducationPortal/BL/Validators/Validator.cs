using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BL.Validators
{
    public class Validator
    {
        public bool ValidatePassword(string password)
        {
            Regex regexPassword = new Regex(@"([0-9]|[a-zA-Z]){6,12}");
            return regexPassword.IsMatch(password);
        }
        public bool ValidateEmail(string email)
        {
            Regex regexEmail= new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regexEmail.IsMatch(email);
        }
        public bool ValidateName(string name)
        {
            Regex regexName = new Regex(@"^.{6,12}$");
            return regexName.IsMatch(name);
        }
        public string ValidateAccount(string name, string email, string password, string password2)
        {
            StringBuilder errors = new StringBuilder();
            if (!ValidatePassword(password) || password.CompareTo(password2) != 0)
            {
                errors.Append("Password too easy or don`t match\n");
            }
            if(!ValidateEmail(email))
            {
                errors.Append("Incorrect email \n");
            }
            if(!ValidateName(name))
            {
                errors.Append("The name is too short or too long\n");
            }
            return errors.ToString();
        }
    }
}
