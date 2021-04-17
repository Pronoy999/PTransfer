using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Models {
    public class Users {
        public int UserId { get; set; }        
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(13)]
        public string PhoneNumber { get; set; }
        public string JwToken { get; set; }
        public string Password { get; set; }

        public Users(int userId, string firstName, string lastName, string emailAddress, string phoneNumber) {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }

        public Users(int userId, string firstName, string lastName, string emailAddress, string phoneNumber, string jwToken) : this(userId, firstName, lastName, emailAddress, phoneNumber) {
            JwToken = jwToken;
        }

        public Users() {
        }
        public Users(int UserId) {
            this.UserId = UserId;
        }
        public Dictionary<string, string> ToKeyValuePairs() {
            Dictionary<String, String> userKeyValuePairs = new Dictionary<string, string>();
            userKeyValuePairs.Add("UserId", this.UserId.ToString());
            return userKeyValuePairs;
        }
    }
}
