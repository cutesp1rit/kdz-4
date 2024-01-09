using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Address
    {
        string _postalCode;
        string _city;
        string _street;
        string _houseNubmer;
        string _phone;
        string _fax;

        public Address(string postalCode, string street, string houseNubmer, string phone, string fax, string city = "Moscow")
        {
            if (postalCode == null || street==null || houseNubmer==null || phone==null || fax==null || city==null)
            {
                throw new ArgumentNullException();
            }
            _postalCode = postalCode;
            _city = city;
            _street = street;
            _houseNubmer = houseNubmer;
            _phone = phone;
            _fax = fax;
        }

        public Address()
        {
            _postalCode = "";
            _city = "";
            _street = "";
            _houseNubmer = "";
            _phone = "";
            _fax = "";
        }
    }
}
