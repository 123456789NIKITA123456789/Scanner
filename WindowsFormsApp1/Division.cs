using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Division
    {
        private string id = "";
        private string name = "";
        private bool check = false;
        private string fio = "";
        private int floor = -1;
        private int number = -1;
        private string phone = "";
        private string wing = "";


        public Division(string _id)
        {
            id = _id;
        }

        public Division(string _id, string _name, bool _check, string _fio, int _floor, int _number, string _phone, string _wing)
        {
            id = _id;
            name = _name;
            check = _check;
            fio = _fio;
            floor = _floor; 
            number = _number;
            phone = _phone;
            wing = _wing;
        }

        public void setId(string _id)
        {
            this.id = _id;
        }

        public void setName(string _name)
        {
            this.name = _name;
        }

        public void setCheck(bool _check)
        {
            this.check = _check;
        }
        public void setFIO(string _fio)
        {
            this.fio = _fio;
        }

        public void setFloor(int _floor)
        {
            this.floor = _floor;
        }

        public void setNumber(int _number)
        {
            this.number = _number;
        }

        public void setPhone(string _phone)
        {
            this.phone = _phone;
        }

        public void setWing(string _wing)
        {
            this.wing = _wing;
        }

        public string getId()
        {
            return id;
        }

        public string getName()
        {
            return name;

        }

        public bool getCheck()
        {
            return check;
        }
        public string getFIO()
        {
            return fio;
        }

        public int getFloor()
        {
            return floor;
        }

        public int getNumber()
        {
            return number;
        }

        public string getPhone()
        {
            return phone;
        }

        public string getWing()
        {
            return wing;
        }
    }
}
