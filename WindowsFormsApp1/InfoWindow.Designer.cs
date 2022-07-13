namespace WindowsFormsApp1
{
    partial class InfoWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoWindow));
            this.divListBox = new System.Windows.Forms.ListBox();
            this.codeOutputBox = new System.Windows.Forms.PictureBox();
            this.saveCodeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.qrRadioButton = new System.Windows.Forms.RadioButton();
            this.barRadioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fioLabel = new System.Windows.Forms.Label();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.wingLabel = new System.Windows.Forms.Label();
            this.floorLabel = new System.Windows.Forms.Label();
            this.numberLabel = new System.Windows.Forms.Label();
            this.excelReportButton = new System.Windows.Forms.Button();
            this.pdfExportButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.codeOutputBox)).BeginInit();
            this.SuspendLayout();
            // 
            // divListBox
            // 
            this.divListBox.FormattingEnabled = true;
            this.divListBox.ItemHeight = 16;
            this.divListBox.Location = new System.Drawing.Point(12, 12);
            this.divListBox.Name = "divListBox";
            this.divListBox.Size = new System.Drawing.Size(620, 356);
            this.divListBox.TabIndex = 0;
            this.divListBox.SelectedIndexChanged += new System.EventHandler(this.divListBox_SelectedIndexChanged);
            // 
            // codeOutputBox
            // 
            this.codeOutputBox.Location = new System.Drawing.Point(638, 12);
            this.codeOutputBox.Name = "codeOutputBox";
            this.codeOutputBox.Size = new System.Drawing.Size(464, 456);
            this.codeOutputBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.codeOutputBox.TabIndex = 1;
            this.codeOutputBox.TabStop = false;
            // 
            // saveCodeButton
            // 
            this.saveCodeButton.Location = new System.Drawing.Point(920, 508);
            this.saveCodeButton.Name = "saveCodeButton";
            this.saveCodeButton.Size = new System.Drawing.Size(182, 35);
            this.saveCodeButton.TabIndex = 2;
            this.saveCodeButton.Text = "Сохранить код";
            this.saveCodeButton.UseVisualStyleBackColor = true;
            this.saveCodeButton.Click += new System.EventHandler(this.saveCodeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 478);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите тип сохраняемого кода";
            // 
            // qrRadioButton
            // 
            this.qrRadioButton.AutoSize = true;
            this.qrRadioButton.Checked = true;
            this.qrRadioButton.Location = new System.Drawing.Point(641, 497);
            this.qrRadioButton.Name = "qrRadioButton";
            this.qrRadioButton.Size = new System.Drawing.Size(75, 20);
            this.qrRadioButton.TabIndex = 4;
            this.qrRadioButton.TabStop = true;
            this.qrRadioButton.Text = "QR-код";
            this.qrRadioButton.UseVisualStyleBackColor = true;
            this.qrRadioButton.CheckedChanged += new System.EventHandler(this.qrRadioButton_CheckedChanged);
            // 
            // barRadioButton
            // 
            this.barRadioButton.AutoSize = true;
            this.barRadioButton.Location = new System.Drawing.Point(641, 523);
            this.barRadioButton.Name = "barRadioButton";
            this.barRadioButton.Size = new System.Drawing.Size(95, 20);
            this.barRadioButton.TabIndex = 5;
            this.barRadioButton.Text = "Штрих-код";
            this.barRadioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 399);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Этаж";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 426);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Крыло";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 452);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Телефон";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 478);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "ФИО ответственного";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "№ поздразделения";
            // 
            // fioLabel
            // 
            this.fioLabel.Location = new System.Drawing.Point(165, 478);
            this.fioLabel.Name = "fioLabel";
            this.fioLabel.Size = new System.Drawing.Size(467, 16);
            this.fioLabel.TabIndex = 13;
            // 
            // phoneLabel
            // 
            this.phoneLabel.Location = new System.Drawing.Point(165, 452);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(467, 16);
            this.phoneLabel.TabIndex = 14;
            // 
            // wingLabel
            // 
            this.wingLabel.Location = new System.Drawing.Point(165, 426);
            this.wingLabel.Name = "wingLabel";
            this.wingLabel.Size = new System.Drawing.Size(467, 21);
            this.wingLabel.TabIndex = 15;
            // 
            // floorLabel
            // 
            this.floorLabel.Location = new System.Drawing.Point(165, 399);
            this.floorLabel.Name = "floorLabel";
            this.floorLabel.Size = new System.Drawing.Size(467, 16);
            this.floorLabel.TabIndex = 16;
            // 
            // numberLabel
            // 
            this.numberLabel.Location = new System.Drawing.Point(165, 371);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(467, 16);
            this.numberLabel.TabIndex = 17;
            // 
            // excelReportButton
            // 
            this.excelReportButton.Location = new System.Drawing.Point(12, 514);
            this.excelReportButton.Name = "excelReportButton";
            this.excelReportButton.Size = new System.Drawing.Size(310, 35);
            this.excelReportButton.TabIndex = 19;
            this.excelReportButton.Text = "Выгрузить отчет в Excel";
            this.excelReportButton.UseVisualStyleBackColor = true;
            this.excelReportButton.Click += new System.EventHandler(this.excelReportButton_Click);
            // 
            // pdfExportButton
            // 
            this.pdfExportButton.Location = new System.Drawing.Point(328, 514);
            this.pdfExportButton.Name = "pdfExportButton";
            this.pdfExportButton.Size = new System.Drawing.Size(304, 35);
            this.pdfExportButton.TabIndex = 20;
            this.pdfExportButton.Text = "Выгрузить отчет в PDF";
            this.pdfExportButton.UseVisualStyleBackColor = true;
            this.pdfExportButton.Click += new System.EventHandler(this.pdfExportButton_Click);
            // 
            // InfoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 574);
            this.Controls.Add(this.pdfExportButton);
            this.Controls.Add(this.excelReportButton);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.floorLabel);
            this.Controls.Add(this.wingLabel);
            this.Controls.Add(this.phoneLabel);
            this.Controls.Add(this.fioLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.barRadioButton);
            this.Controls.Add(this.qrRadioButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveCodeButton);
            this.Controls.Add(this.codeOutputBox);
            this.Controls.Add(this.divListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InfoWindow";
            this.Text = "Инфомарция по подразделениям";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CodeWindow_FormClosed);
            this.Load += new System.EventHandler(this.CodeWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.codeOutputBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox divListBox;
        private System.Windows.Forms.PictureBox codeOutputBox;
        private System.Windows.Forms.Button saveCodeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton qrRadioButton;
        private System.Windows.Forms.RadioButton barRadioButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label fioLabel;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.Label wingLabel;
        private System.Windows.Forms.Label floorLabel;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.Button excelReportButton;
        private System.Windows.Forms.Button pdfExportButton;
    }
}