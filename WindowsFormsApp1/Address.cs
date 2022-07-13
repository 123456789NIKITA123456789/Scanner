using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Address
    {
        private string id;
        private string name;
        private List<Division> divisions;

        public Address(string _id, string _name)
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
            this.name = _name;
        }

        public void setDivisions(List<Division> _div)
        {
            this.divisions = _div;
        }

        public string getId()
        {
            return this.id;
        }

        public string getName()
        {
            return this.name;
        }

        public List<Division> getDivisions()
        {
            return divisions;
        }


    }
}
