using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class AuthenticationWindow : Form, Authentication
    {
        private List<City> cityList = new List<City>();
        private List<Address> addressList = new List<Address>();
        private City oldCity = new City();
        int indexOldAddress = -1;
        private bool loadSucces = false;
        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        private void AuthenticationWindow_Load(object sender, EventArgs e)
        {
            LoadWindow();
        }

        private void LoadWindow()
        {
            Program.dBworker.Attach(this);
            if(Program.dBworker.getCurCityIndex() > 0)
                cityBox.SelectedIndex = Program.dBworker.getCurCityIndex();
            Program.dBworker.StartListenerFromDB_Cities();
        }

        public void UpdateCities(List<City> cities)
        {
            bool flag = false;

            if(cities.Count != cityList.Count)
            {
                flag = true;
            }
            else
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    if(cityList[i] != cityList[i])
                    {
                        flag = true;
                    }
                }
            }
            

            if(flag)
            {
                cityList.Clear();
                cityList.AddRange(cities);
                Program.ToMain(cityBox, cityBox =>
                {
                    ((ListBox)cityBox).Items.Clear();
                    foreach (City city in cityList)
                    {

                        ((ListBox)cityBox).Items.Add(city.getName());

                    }
                    return true;
                });
            }
        }

        public void UpdateAddresses(List<Address> addresses)
        {
            bool flag = false;

            if (addresses.Count != addressList.Count)
            {
                flag = true;
            }
            else
            {
                for (int i = 0; i < addresses.Count; i++)
                {
                    if (addresses[i] != addressList[i])
                    {
                        flag = true;
                    }
                }
            }


            if (flag)
            {
                addressList.Clear();
                for(int i = 0; i < addresses.Count; i++)
                {
                    addressList.Add(addresses[i]);
                }

                Program.ToMain(addressBox, (addressBox) =>
                {
                    ((ListBox)addressBox).Items.Clear();
                    foreach (Address address in addressList)
                    {

                        ((ListBox)addressBox).Items.Add(address.getName());

                    }
                    return true;
                });
            }
        }

        private void cityBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAddresses();
        }
        private void LoadAddresses()
        {
            addressBox.Items.Clear();
            int curCityIndex = cityBox.SelectedIndex;
            Program.dBworker.setCurCityIndex(curCityIndex);
            Program.dBworker.StartListenerFromDB_Addresses(Program.dBworker.getCurCityId());
            //Program.dBworker.GetAddressesFromDB();
        }

        private void addAddressButton_Click(object sender, EventArgs e)
        {
            AddNewAddress();
        }

        private void AddNewAddress()
        {
            this.Enabled = false;
            Program.dBworker.Detach(this);

            NewAddressWindow naw = new NewAddressWindow();
            naw.ShowDialog();

            Program.dBworker.Attach(this);
            this.Enabled = true;
            addressList.Clear();
            addressBox.Items.Clear();
            if (Program.dBworker.getCurCityIndex() > 0)
                cityBox.SelectedIndex = Program.dBworker.getCurCityIndex();
        }

        public void setCurrentCity(City currentCity)
        {
            if ((currentCity != null) && (currentCity.getId() != null) && 
                (currentCity.getAddresses() != null) && (currentCity.getAddresses().Count != 0))
            {
                oldCity.setId(currentCity.getId());
                oldCity.setName(currentCity.getName());

                List<Address> addresses = new List<Address>();
                for (int i = 0; i < currentCity.getAddresses().Count; i++)
                {
                    Address address = new Address(currentCity.getAddresses()[i].getId(), currentCity.getAddresses()[i].getName());
                    addresses.Add(address);
                }

                oldCity.setAddresses(addresses);

                indexOldAddress = Program.dBworker.getCurAddressIndex();
            }
        }

        private void outputInfoButton_Click(object sender, EventArgs e)
        {
            LoadDataAndClose();
        }
        private void LoadDataAndClose()
        {
            if (addressBox.SelectedIndex != -1)
            {
                int curAddressIndex = addressBox.SelectedIndex;
                Program.dBworker.setCurAddressIndex(curAddressIndex);

                FileStream fs = File.Create(Path.Combine(Path.GetTempPath(), "depart.txt"));
                fs.Close();
                StreamWriter sw = new StreamWriter(Path.Combine(Path.GetTempPath(), "depart.txt"));
                sw.WriteLine(Program.dBworker.getCurCity().getId());
                sw.WriteLine(Program.dBworker.getCurAddress().getId());
                sw.Close();

                loadSucces = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите адрес подразделения");
            }

        }
        private void AuthenticationWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dBworker.Detach(this);
            

            if(!loadSucces)
            {
                if((oldCity != null)&&(oldCity.getId() != null)&&
                    (oldCity.getAddresses() != null)&&(oldCity.getAddresses().Count != 0)&&
                    (indexOldAddress != -1))
                {
                    Program.dBworker.setCurCityId(oldCity.getId());
                    Program.dBworker.setCurAddressID(oldCity.getAddresses()[indexOldAddress].getId());

                    FileStream fs = File.Create(Path.Combine(Path.GetTempPath(), "depart.txt"));
                    fs.Close();
                    StreamWriter sw = new StreamWriter(Path.Combine(Path.GetTempPath(), "depart.txt"));
                    sw.WriteLine(Program.dBworker.getCurCity().getId());
                    sw.WriteLine(Program.dBworker.getCurAddress().getId());
                    sw.Close();
                }

                loadSucces = false;
            }
        }
    }
}
