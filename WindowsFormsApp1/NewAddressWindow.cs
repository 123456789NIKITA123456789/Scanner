using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class NewAddressWindow : Form, Authentication
    {
        List<City> cities = new List<City>();
        List<Address> addresses = new List<Address>();
        public NewAddressWindow()
        {
            InitializeComponent();
        }

        private void NewAddressWindow_Load(object sender, EventArgs e)
        {
            Program.dBworker.Attach(this);
        }

        public void UpdateCities(List<City> newCities)
        { 
            cities.Clear();
            cities.AddRange(newCities);

            UpdateView();
        }

        private void UpdateView()
        {
            Program.ToMain(cityComboBox, cityComboBox =>
            {
                ((ComboBox)cityComboBox).Items.Clear();
                foreach (City city in cities)
                {
                    ((ComboBox)cityComboBox).Items.Add(city.getName());
                }
                ((ComboBox)cityComboBox).Items.Add("Другой город");
                return true;
            });
            
        }

        public void UpdateAddresses(List<Address> newAddresses)
        {
        }

        private void cityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserChoice();
        }

        private void UserChoice()
        {
            if(cityComboBox.SelectedIndex == cityComboBox.Items.Count - 1)
            {
                newCityTextbox.Visible = true;
                newCityTextbox.Enabled = true;
                label1.Visible = true;
            }
            else
            {
                newCityTextbox.Visible = false;
                newCityTextbox.Enabled = false;
                label1.Visible = false;
            }
        }

        private void addNewAddressButton_Click(object sender, EventArgs e)
        {
            AddAddress();
        }
        
        private void AddAddress()
        {
            if((cityComboBox.SelectedIndex == -1)&&(newCityTextbox.Text==""))
            {
                MessageBox.Show("Выберите город из списка или впишете его название в поле");
                return;
            }

            if(newAddressTextbox.Text == "")
            {
                MessageBox.Show("Впишете в поле адрес подразделения");
                return;
            }

            string cityName = "";
            string addressName = newAddressTextbox.Text;

            if (cityComboBox.SelectedIndex == cityComboBox.Items.Count - 1)
            {
                cityName = newCityTextbox.Text;
            }
            else
            {
                cityName = cityComboBox.Items[cityComboBox.SelectedIndex].ToString();
            }

            Program.dBworker.AddNewAddress(cityName, addressName);
            newCityTextbox.Text = "";
            newAddressTextbox.Text = "";
            cityComboBox.SelectedIndex = -1;


            if (closeCheckBox.Checked)
            {
                this.Close();
            }

        }

        private void NewAddressWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.dBworker.Detach(this);
        }
    }
}
