using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.Users
{
    public class UserCreateCustomerAuthen
    {
        [Required]
        [MaxLength(256)]
        public String ProviderToken { get; set; }

        public String? Email { get; set; }
        public String? Password { get; set; }
        public String? ProfilePicture { get; set; }
        public String Name { get; set; }
    }
}
