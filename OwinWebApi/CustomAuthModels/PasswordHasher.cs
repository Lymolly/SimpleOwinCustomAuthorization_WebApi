using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinWebApi.Models
{
    public class PasswordHasher
    {
        public string Hash(string password)
        {
            char[] chars = password.ToCharArray();
            char[] hash = chars.Reverse().ToArray();
            return new string(hash);
        }
    }
}
