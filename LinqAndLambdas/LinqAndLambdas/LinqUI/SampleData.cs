using LinqUI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinqUI
{
    public static class SampleData
    {
        public static List<ContactModel> GetContactData()
        {
            List<ContactModel> output = new List<ContactModel>
            {
                new ContactModel{ Id = 1, FirstName = "fName1", LastName = "lName1", Addresses = new List<int>{1,2,3}},
                new ContactModel{ Id = 2, FirstName = "fName2", LastName = "lName2", Addresses = new List<int>{1,2,3}},
                new ContactModel{ Id = 3, FirstName = "fName3", LastName = "lName3", Addresses = new List<int>{1,2}},
                new ContactModel{ Id = 4, FirstName = "fName4", LastName = "lName4", Addresses = new List<int>{1}},
                new ContactModel{ Id = 5, FirstName = "fName5", LastName = "lName5", Addresses = new List<int>{2}}
            };

            return output;
        }

        public static List<AddressModel> GetAddressData()
        {
            List<AddressModel> output = new List<AddressModel>
            {
                new AddressModel{Id = 1, City = "Cincinnati", State = "OH"},
                new AddressModel{Id = 2, City = "Dayton", State = "OH"},
                new AddressModel{Id = 3, City = "Lexington", State = "KY"}
            };

            return output;
        }
    }
}
