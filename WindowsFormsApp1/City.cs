using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class City
    {
        private string id;
        private string name;
        private List<Address> addresses = new List<Address>();

        public City()
        { }
        public City(string _id, string _name)
        {
            this.id = _id;
            this.name = _name;
        }

        public void setId(string _id)
        {
            this.id = _id;
        }

        public void setName(string _name)
        {
            this.name= _name;
        }

        public void setAddresses(List<Address> addr)
        {
            addresses.Clear();

            foreach(Address address in addr)
            {
                addresses.Add(address);
            }
        }

        public string getId()
        {
            return this.id;
        }

        public string getName()
        {
            return this.name;
        }

        public List<Address> getAddresses()
        {
            return addresses;
        }
    }
}
