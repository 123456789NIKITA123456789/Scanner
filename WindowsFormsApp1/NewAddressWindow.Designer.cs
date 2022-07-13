namespace WindowsFormsApp1
{
    partial class NewAddressWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAddressWindow));
            this.cityComboBox = new System.Windows.Forms.ComboBox();
            this.newCityTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newAddressTextbox = new System.Windows.Forms.TextBox();
            this.addNewAddressButton = new System.Windows.Forms.Button();
            this.closeCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cityComboBox
            // 
            this.cityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cityComboBox.FormattingEnabled = true;
            this.cityComboBox.Location = new System.Drawing.Point(32, 12);
            this.cityComboBox.Name = "cityComboBox";
            this.cityComboBox.Size = new System.Drawing.Size(258, 24);
            this.cityComboBox.TabIndex = 0;
            this.cityComboBox.SelectedIndexChanged += new System.EventHandler(this.cityComboBox_SelectedIndexChanged);
            // 
            // newCityTextbox
            // 
            this.newCityTextbox.Enabled = false;
            this.newCityTextbox.Location = new System.Drawing.Point(299, 31);
            this.newCityTextbox.Name = "newCityTextbox";
            this.newCityTextbox.Size = new System.Drawing.Size(226, 22);
            this.newCityTextbox.TabIndex = 1;
            this.newCityTextbox.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите название нового города";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(229, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Введите название нового адреса";
            // 
            // newAddressTextbox
            // 
            this.newAddressTextbox.Location = new System.Drawing.Point(299, 75);
            this.newAddressTextbox.Name = "newAddressTextbox";
            this.newAddressTextbox.Size = new System.Drawing.Size(226, 22);
            this.newAddressTextbox.TabIndex = 3;
            // 
            // addNewAddressButton
            // 
            this.addNewAddressButton.Location = new System.Drawing.Point(32, 75);
            this.addNewAddressButton.Name = "addNewAddressButton";
            this.addNewAddressButton.Size = new System.Drawing.Size(258, 23);
            this.addNewAddressButton.TabIndex = 5;
            this.addNewAddressButton.Text = "Добавить адрес";
            this.addNewAddressButton.UseVisualStyleBackColor = true;
            this.addNewAddressButton.Click += new System.EventHandler(this.addNewAddressButton_Click);
            // 
            // closeCheckBox
            // 
            this.closeCheckBox.AutoSize = true;
            this.closeCheckBox.Checked = true;
            this.closeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeCheckBox.Location = new System.Drawing.Point(32, 49);
            this.closeCheckBox.Name = "closeCheckBox";
            this.closeCheckBox.Size = new System.Drawing.Size(242, 20);
            this.closeCheckBox.TabIndex = 6;
            this.closeCheckBox.Text = "Закрыть окно после добавления";
            this.closeCheckBox.UseVisualStyleBackColor = true;
            // 
            // NewAddressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 119);
            this.Controls.Add(this.closeCheckBox);
            this.Controls.Add(this.addNewAddressButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newAddressTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newCityTextbox);
            this.Controls.Add(this.cityComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewAddressWindow";
            this.Text = "Добавить новый адрес";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewAddressWindow_FormClosing);
            this.Load += new System.EventHandler(this.NewAddressWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cityComboBox;
        private System.Windows.Forms.TextBox newCityTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newAddressTextbox;
        private System.Windows.Forms.Button addNewAddressButton;
        private System.Windows.Forms.CheckBox closeCheckBox;
    }
}