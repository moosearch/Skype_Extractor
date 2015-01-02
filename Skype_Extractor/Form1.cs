using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skype_Extractor
{
    public partial class Form1 : Form
    {

        SkypeExtractor sky;
        public Form1()
        {
            InitializeComponent();
        }

        // Will extract contacts + associated chats for given skype user
        private void FindButton_Click(object sender, EventArgs e)
        {
            // Verify that skypename is valid
            // ie. Check if directory exists...

            string skypeId = SkypeIdTextBox.Text;
            if (skypeId == null)
                return;

            string directory = "C:\\Users\\" + System.Environment.UserName + "\\AppData\\Roaming\\Skype\\" + skypeId + "\\main.db";

            if (System.IO.File.Exists(directory))
            {
                sky = new SkypeExtractor(skypeId, false);

                // Clear both listBox items before populating them
                listBox1.Items.Clear();
                listBox2.Items.Clear();

                foreach (Contact s in sky.listOfContacts)
                {
                    listBox1.Items.Add(s.SkypeName);
                }
            }
            else
            {
                // Output msgbox to tell user that they have invalid input
                MessageBox.Show("No such ID. Either check that you have logged on to Skype at least once or check the input.", 
                    "Skype ID Input Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }

        }

        // Adds a contact to have their chat history extracted
        private void AddButton_Click(object sender, EventArgs e)
        {
            // if left list is empty
            if (listBox1.Items.Count == 0)
            {
                return;
            }
            string s = (string) listBox1.SelectedItem;
            if (s == null)
            {
                return;
            }
            listBox1.Items.Remove(s);
            listBox2.Items.Add(s);
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            // If right list is empty
            if (listBox2.Items.Count == 0)
            {
                return;
            }
            string s = (string)listBox2.SelectedItem;
            if (s==null)
            {
                return;
            }
            listBox2.Items.Remove(s);
            listBox1.Items.Add(s);
        }

        private void ExtractButton_Click(object sender, EventArgs e)
        {

            // Format and write chat history to a directory
            saveLocationChatHistory.ShowDialog();

            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show(
                    "Check to see that you have populated the right list with skype contacts you want to extract history for.",
                    "No Contacts found in the List",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (saveLocationChatHistory.SelectedPath == null)
            {
                MessageBox.Show(
                    "Check to see that you have selected a directory where you want to store all the chat history documents in",
                    "No path selected for chat history location",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            sky.ExtractChat(listBox2.Items, saveLocationChatHistory.SelectedPath);

            string dir = String.Format("Chat extraction is done. Your chat histories are found in the following directory:\n {0}",saveLocationChatHistory.SelectedPath);
            MessageBox.Show(
                dir,
                "Extraction successful",
                MessageBoxButtons.OK,
                MessageBoxIcon.None
                );
            SkypeIdTextBox.Text = null;
            listBox1.Items.Clear();
            listBox2.Items.Clear();

        }
    }
}
