using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDBUI.Models
{
    //used: Edit>Paste Special>Paste JSON As Classes
    public class Rootobject
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Emailaddress[] emailAddresses { get; set; }
        public Phonenumber[] phoneNumbers { get; set; }
    }

    public class Emailaddress
    {
        public string emailAddress { get; set; }
    }

    public class Phonenumber
    {
        public string phoneNumber { get; set; }
    }
}
