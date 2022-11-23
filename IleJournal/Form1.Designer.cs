namespace IleJournal
{
    partial class Journal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Save = new System.Windows.Forms.Button();
            this.WeekBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(523, 426);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
         
            // 
            // Save
            // 
            this.Save.Image = global::IleJournal.Properties.Resources.icons8_save_50;
            this.Save.Location = new System.Drawing.Point(459, 371);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(50, 50);
            this.Save.TabIndex = 1;
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.button1_Click);
            // 
            // WeekBox
            // 
            this.WeekBox.FormattingEnabled = true;
            this.WeekBox.Location = new System.Drawing.Point(125, 398);
            this.WeekBox.Name = "WeekBox";
            this.WeekBox.Size = new System.Drawing.Size(121, 23);
            this.WeekBox.TabIndex = 2;
            this.WeekBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 398);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "GET WEEK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Journal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(569, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.WeekBox);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Journal";
            this.Text = "Journal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox richTextBox1;
        private Button Save;
        private ComboBox WeekBox;
        private Button button1;
    }
}