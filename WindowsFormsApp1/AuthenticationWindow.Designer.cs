namespace WindowsFormsApp1
{
    partial class AuthenticationWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthenticationWindow));
            this.cityBox = new System.Windows.Forms.ListBox();
            this.addressBox = new System.Windows.Forms.ListBox();
            this.outputInfoButton = new System.Windows.Forms.Button();
            this.addAddressButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cityBox
            // 
            this.cityBox.FormattingEnabled = true;
            this.cityBox.ItemHeight = 16;
            this.cityBox.Location = new System.Drawing.Point(12, 12);
            this.cityBox.Name = "cityBox";
            this.cityBox.Size = new System.Drawing.Size(301, 292);
            this.cityBox.TabIndex = 0;
            this.cityBox.SelectedIndexChanged += new System.EventHandler(this.cityBox_SelectedIndexChanged);
            // 
            // addressBox
            // 
            this.addressBox.FormattingEnabled = true;
            this.addressBox.ItemHeight = 16;
            this.addressBox.Location = new System.Drawing.Point(319, 12);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(301, 292);
            this.addressBox.TabIndex = 1;
            // 
            // outputInfoButton
            // 
            this.outputInfoButton.Location = new System.Drawing.Point(290, 310);
            this.outputInfoButton.Name = "outputInfoButton";
            this.outputInfoButton.Size = new System.Drawing.Size(162, 28);
            this.outputInfoButton.TabIndex = 2;
            this.outputInfoButton.Text = "Выбрать";
            this.outputInfoButton.UseVisualStyleBackColor = true;
            this.outputInfoButton.Click += new System.EventHandler(this.outputInfoButton_Click);
            // 
            // addAddressButton
            // 
            this.addAddressButton.Location = new System.Drawing.Point(458, 310);
            this.addAddressButton.Name = "addAddressButton";
            this.addAddressButton.Size = new System.Drawing.Size(162, 28);
            this.addAddressButton.TabIndex = 5;
            this.addAddressButton.Text = "Добавить адрес";
            this.addAddressButton.UseVisualStyleBackColor = true;
            this.addAddressButton.Click += new System.EventHandler(this.addAddressButton_Click);
            // 
            // AuthenticationWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 350);
            this.Controls.Add(this.addAddressButton);
            this.Controls.Add(this.outputInfoButton);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.cityBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AuthenticationWindow";
            this.Text = "Адрес Вашего рабочего места";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AuthenticationWindow_FormClosing);
            this.Load += new System.EventHandler(this.AuthenticationWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox cityBox;
        private System.Windows.Forms.ListBox addressBox;
        private System.Windows.Forms.Button outputInfoButton;
        private System.Windows.Forms.Button addAddressButton;
    }
}