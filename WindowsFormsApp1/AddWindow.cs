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
    public partial class AddWindow : Form
    {
        public AddWindow()
        {
            InitializeComponent();
            
        }

        private void AddWindow_Load(object sender, EventArgs e)
        {
            floorNumeric.Minimum = -100;
            numberNumeric.Maximum = 1000;
        }

        private void newDivisionButton_Click(object sender, EventArgs e)
        {
            AddInDB();
        }

        private void AddInDB()
        {
            if (newDivisionInput.Text != "")
            {
                string newElname = newDivisionInput.Text;
                List<Division> allDiv = Program.dBworker.GetData();
                for (int i = 0; i < allDiv.Count; i++)
                {
                    //Does this element exist in DB
                    if (allDiv[i].getName() == newElname)
                    {
                        return; //If so, don't add and exit
                    }
                }

                string fio = fioTextBox.Text;
                string phone = phoneTextBox.Text;
                string wing = wingTextBox.Text;
                int floor = int.Parse(floorNumeric.Value.ToString());
                int number = int.Parse(numberNumeric.Value.ToString());


                Program.dBworker.AddNewDivision(newElname,fio, floor,number,phone,wing);
                newDivisionInput.Clear();

                if (closeCheckBox.Checked)
                {
                    this.Close();
                }
                else
                {
                    newDivisionInput.Text = "";
                    fioTextBox.Text = "";
                    phoneTextBox.Text = "";
                    wingTextBox.Text = "";
                    floorNumeric.Value = 0;
                    numberNumeric.Value = 0;
                }
            }
            else
            {
                MessageBox.Show("Введите название подразделения");
            }
        }
        
    }
}
