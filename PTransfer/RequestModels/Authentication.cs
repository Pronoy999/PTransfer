using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.RequestModels {
    public class Authentication {
        [Required(ErrorMessage = "Email Address is missing")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }        
    }
}
