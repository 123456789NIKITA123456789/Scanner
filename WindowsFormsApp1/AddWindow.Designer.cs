namespace WindowsFormsApp1
{
    partial class AddWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddWindow));
            this.newDivisionButton = new System.Windows.Forms.Button();
            this.newDivisionInput = new System.Windows.Forms.TextBox();
            this.closeCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fioTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.floorNumeric = new System.Windows.Forms.NumericUpDown();
            this.numberNumeric = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.wingTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.floorNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // newDivisionButton
            // 
            this.newDivisionButton.Location = new System.Drawing.Point(320, 170);
            this.newDivisionButton.Name = "newDivisionButton";
            this.newDivisionButton.Size = new System.Drawing.Size(302, 23);
            this.newDivisionButton.TabIndex = 0;
            this.newDivisionButton.Text = "Добавить новое подразделение";
            this.newDivisionButton.UseVisualStyleBackColor = true;
            this.newDivisionButton.Click += new System.EventHandler(this.newDivisionButton_Click);
            // 
            // newDivisionInput
            // 
            this.newDivisionInput.Location = new System.Drawing.Point(12, 32);
            this.newDivisionInput.Name = "newDivisionInput";
            this.newDivisionInput.Size = new System.Drawing.Size(607, 22);
            this.newDivisionInput.TabIndex = 1;
            // 
            // closeCheckBox
            // 
            this.closeCheckBox.AutoSize = true;
            this.closeCheckBox.Checked = true;
            this.closeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeCheckBox.Location = new System.Drawing.Point(12, 170);
            this.closeCheckBox.Name = "closeCheckBox";
            this.closeCheckBox.Size = new System.Drawing.Size(226, 20);
            this.closeCheckBox.TabIndex = 4;
            this.closeCheckBox.Text = "Закрыть окно после удаления";
            this.closeCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Название подразделения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "ФИО ответственного";
            // 
            // fioTextBox
            // 
            this.fioTextBox.Location = new System.Drawing.Point(13, 81);
            this.fioTextBox.Name = "fioTextBox";
            this.fioTextBox.Size = new System.Drawing.Size(298, 22);
            this.fioTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Этаж";
            // 
            // floorNumeric
            // 
            this.floorNumeric.Location = new System.Drawing.Point(12, 129);
            this.floorNumeric.Name = "floorNumeric";
            this.floorNumeric.Size = new System.Drawing.Size(120, 22);
            this.floorNumeric.TabIndex = 9;
            // 
            // numberNumeric
            // 
            this.numberNumeric.Location = new System.Drawing.Point(191, 129);
            this.numberNumeric.Name = "numberNumeric";
            this.numberNumeric.Size = new System.Drawing.Size(120, 22);
            this.numberNumeric.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Номер";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(317, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Телефон";
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Location = new System.Drawing.Point(320, 82);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(299, 22);
            this.phoneTextBox.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Крыло";
            // 
            // wingTextBox
            // 
            this.wingTextBox.Location = new System.Drawing.Point(320, 129);
            this.wingTextBox.Name = "wingTextBox";
            this.wingTextBox.Size = new System.Drawing.Size(299, 22);
            this.wingTextBox.TabIndex = 15;
            // 
            // AddWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 210);
            this.Controls.Add(this.wingTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numberNumeric);
            this.Controls.Add(this.floorNumeric);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fioTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeCheckBox);
            this.Controls.Add(this.newDivisionInput);
            this.Controls.Add(this.newDivisionButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "AddWindow";
            this.Text = "Добавление подразделения";
            this.Load += new System.EventHandler(this.AddWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.floorNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newDivisionButton;
        private System.Windows.Forms.TextBox newDivisionInput;
        private System.Windows.Forms.CheckBox closeCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fioTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown floorNumeric;
        private System.Windows.Forms.NumericUpDown numberNumeric;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox wingTextBox;
    }
}