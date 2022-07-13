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
    public partial class ChangeDataWindow : Form
    {
        bool wasChecked;
        Division item;
        public ChangeDataWindow(Division _item)
        {
            InitializeComponent();
            wasChecked = _item.getCheck();
            item = _item;
        }

        private void ChangeDataWindow_Load(object sender, EventArgs e)
        {
            if(wasChecked)
            {
                questionLabel.Text = "Снять отметку с элемента?";
            }
            else
            {
                questionLabel.Text = "Отметить элемент?";
            }
            nameLabel.Text = item.getName();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Program.dBworker.UpdateDivisionCheck(item.getId(), !item.getCheck());
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
