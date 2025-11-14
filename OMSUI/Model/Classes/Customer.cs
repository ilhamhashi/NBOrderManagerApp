using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMSDesktopUI.Model.Interfaces;

namespace OMSDesktopUI.Model.Classes
{
    public class Customer : IPerson
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public Customer(int personId, string firstName, string lastName, string phoneNumber)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }
    }
}
