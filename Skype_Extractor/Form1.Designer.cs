namespace Skype_Extractor
{
    partial class Form1
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
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.FindButton = new System.Windows.Forms.Button();
            this.SkypeIdTextBox = new System.Windows.Forms.TextBox();
            this.SkypeIdLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.HoriDivider1 = new System.Windows.Forms.Label();
            this.ListContactsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.ExtractButton = new System.Windows.Forms.Button();
            this.saveLocationChatHistory = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // openFile
            // 
            this.openFile.Filter = "database (.db) |*.db";
            this.openFile.InitialDirectory = "C:\\";
            this.openFile.Title = "Open Skype Database";
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(221, 4);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(75, 23);
            this.FindButton.TabIndex = 1;
            this.FindButton.Text = "Find";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // SkypeIdTextBox
            // 
            this.SkypeIdTextBox.Location = new System.Drawing.Point(106, 6);
            this.SkypeIdTextBox.Name = "SkypeIdTextBox";
            this.SkypeIdTextBox.Size = new System.Drawing.Size(109, 20);
            this.SkypeIdTextBox.TabIndex = 2;
            // 
            // SkypeIdLabel
            // 
            this.SkypeIdLabel.AutoSize = true;
            this.SkypeIdLabel.Location = new System.Drawing.Point(12, 9);
            this.SkypeIdLabel.Name = "SkypeIdLabel";
            this.SkypeIdLabel.Size = new System.Drawing.Size(88, 13);
            this.SkypeIdLabel.TabIndex = 3;
            this.SkypeIdLabel.Text = "Skype Username";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 180);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(518, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(15, 57);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(200, 82);
            this.listBox1.Sorted = true;
            this.listBox1.TabIndex = 5;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(221, 57);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(200, 82);
            this.listBox2.Sorted = true;
            this.listBox2.TabIndex = 6;
            // 
            // HoriDivider1
            // 
            this.HoriDivider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HoriDivider1.Location = new System.Drawing.Point(15, 30);
            this.HoriDivider1.Name = "HoriDivider1";
            this.HoriDivider1.Size = new System.Drawing.Size(488, 2);
            this.HoriDivider1.TabIndex = 7;
            // 
            // ListContactsLabel
            // 
            this.ListContactsLabel.AutoSize = true;
            this.ListContactsLabel.Location = new System.Drawing.Point(12, 41);
            this.ListContactsLabel.Name = "ListContactsLabel";
            this.ListContactsLabel.Size = new System.Drawing.Size(80, 13);
            this.ListContactsLabel.TabIndex = 8;
            this.ListContactsLabel.Text = "List of Contacts";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Chats to Extract For";
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(15, 145);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(200, 23);
            this.AddButton.TabIndex = 11;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(221, 145);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(200, 23);
            this.RemoveButton.TabIndex = 12;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // ExtractButton
            // 
            this.ExtractButton.BackColor = System.Drawing.SystemColors.Control;
            this.ExtractButton.Location = new System.Drawing.Point(427, 57);
            this.ExtractButton.Name = "ExtractButton";
            this.ExtractButton.Size = new System.Drawing.Size(76, 111);
            this.ExtractButton.TabIndex = 14;
            this.ExtractButton.Text = "Extract";
            this.ExtractButton.UseVisualStyleBackColor = false;
            this.ExtractButton.Click += new System.EventHandler(this.ExtractButton_Click);
            // 
            // saveLocationChatHistory
            // 
            this.saveLocationChatHistory.Description = "Choose Chat History Save Location";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(518, 202);
            this.Controls.Add(this.ExtractButton);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListContactsLabel);
            this.Controls.Add(this.HoriDivider1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.SkypeIdLabel);
            this.Controls.Add(this.SkypeIdTextBox);
            this.Controls.Add(this.FindButton);
            this.Name = "Form1";
            this.Text = "Skype Chat Extractor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.TextBox SkypeIdTextBox;
        private System.Windows.Forms.Label SkypeIdLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label HoriDivider1;
        private System.Windows.Forms.Label ListContactsLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Button ExtractButton;
        private System.Windows.Forms.FolderBrowserDialog saveLocationChatHistory;


    }
}

