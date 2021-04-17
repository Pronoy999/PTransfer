using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.RequestModels {
    public class UsersForUpdate {
        [Required(ErrorMessage ="Id required to Update")]
        public int UserId { get; set; }        
        public string FirstName { get; set; }        
        public string LastName { get; set; }        
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }        
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
