using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModel
{
    public class UserLogin
    {
        [Required, Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AccountViewModel : UserLogin
    {
        public long Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public bool Active { get; set; }

        public List<string> Role { get; set; }

        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }

    public class UserRoleViewModel
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Role { get; set; }

        public bool Active { get; set; }
    }
}

