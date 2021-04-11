using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTransfer.Models {
    public class Users {
        public int UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public string PhoneNumber { get; }

        public Users(int userId, string firstName, string lastName, string emailAddress, string phoneNumber) {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }

        public Users() {
        }
    }
}
