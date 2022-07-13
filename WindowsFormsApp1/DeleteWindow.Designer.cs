namespace WindowsFormsApp1
{
    partial class DeleteWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteWindow));
            this.delButton = new System.Windows.Forms.Button();
            this.divListBox = new System.Windows.Forms.CheckedListBox();
            this.closeCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // delButton
            // 
            this.delButton.Location = new System.Drawing.Point(12, 237);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(417, 23);
            this.delButton.TabIndex = 1;
            this.delButton.Text = "Удалить выбранное подразделение";
            this.delButton.UseVisualStyleBackColor = true;
            this.delButton.Click += new System.EventHandler(this.delButton_Click);
            // 
            // divListBox
            // 
            this.divListBox.CheckOnClick = true;
            this.divListBox.FormattingEnabled = true;
            this.divListBox.Location = new System.Drawing.Point(12, 12);
            this.divListBox.Name = "divListBox";
            this.divListBox.Size = new System.Drawing.Size(417, 208);
            this.divListBox.TabIndex = 2;
            // 
            // closeCheckBox
            // 
            this.closeCheckBox.AutoSize = true;
            this.closeCheckBox.Checked = true;
            this.closeCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeCheckBox.Location = new System.Drawing.Point(12, 266);
            this.closeCheckBox.Name = "closeCheckBox";
            this.closeCheckBox.Size = new System.Drawing.Size(226, 20);
            this.closeCheckBox.TabIndex = 3;
            this.closeCheckBox.Text = "Закрыть окно после удаления";
            this.closeCheckBox.UseVisualStyleBackColor = true;
            // 
            // DeleteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 309);
            this.Controls.Add(this.closeCheckBox);
            this.Controls.Add(this.divListBox);
            this.Controls.Add(this.delButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DeleteWindow";
            this.Text = "Удаление подразделения";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DeleteWindow_FormClosed);
            this.Load += new System.EventHandler(this.DeleteWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button delButton;
        private System.Windows.Forms.CheckedListBox divListBox;
        private System.Windows.Forms.CheckBox closeCheckBox;
    }
}