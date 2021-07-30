using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinWebApi.Models
{
    public class User
    {
        public User()
        {
            Claims = new List<UserClaim>();
            Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<UserClaim> Claims { get; set; }
    }
}
