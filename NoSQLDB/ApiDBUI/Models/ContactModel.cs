using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDBUI.Models
{
    //used: Edit>Paste Special>Paste JSON As Classes
    public class ContactModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<EmailAddressModel> EmailAddresses { get; set; }
        public List<PhoneNumberModel> PhoneNumbers { get; set; }
    }

    public class EmailAddressModel
    {
        public string EmailAddress { get; set; }
    }

    public class PhoneNumberModel
    {
        public string PhoneNumber { get; set; }
    }
}
