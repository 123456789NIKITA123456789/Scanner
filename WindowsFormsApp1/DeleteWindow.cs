using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using Google.Cloud.Firestore;

namespace WindowsFormsApp1
{
    public partial class DeleteWindow : Form, Window
    {
        ArrayList checkedEl = new ArrayList();
        List<Division> allDiv = new List<Division>();
        public DeleteWindow()
        {
            InitializeComponent();
        }

        private void DeleteWindow_Load(object sender, EventArgs e)
        {
            Program.dBworker.Attach(this);
        }
        public void Update(List<Division> data)
        {
            UpdateView(data);
            allDiv = data;
        }

        private void UpdateView(List<Division> allDiv)
        {
            Program.ToMain(divListBox, (divListBox) =>
            {
                ((ListBox)divListBox).Items.Clear();
                for (int i = 0; i < allDiv.Count; i++)
                {
                    ((ListBox)divListBox).Items.Add(allDiv[i].getName());
                }
                return true;
            }
                );
        }
        private void delButton_Click(object sender, EventArgs e)
        {
            DeleteDivisions();
        }

        private void DeleteDivisions()
        {
 
            if(divListBox.CheckedItems.Count == 0)
            {
                MessageBox.Show("Выделите удаляемые подразделения");
                
            }
            else
            {
                int j = 0;
                for(int i = 0; i < divListBox.Items.Count; i++)
                {
                    if(divListBox.Items[i] == divListBox.CheckedItems[j])
                    {
                        Program.dBworker.DeleteDivision(allDiv[i].getId());
                        if (j < divListBox.CheckedItems.Count - 1)
                        {
                            j++;
                        }
                        else
                        {
                            i = divListBox.Items.Count;
                        }

                    }
                }

                if (closeCheckBox.Checked)
                {
                    this.Close();
                }
            }

            
        }

        private void DeleteWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.dBworker.Detach(this);
        }
    }
}
