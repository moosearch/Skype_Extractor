/**
 * Wesley Chow
 * Oct 29, 2014
 * Skype.cs
 * 
 * Description: Contains classes for extracting chat history from a SQLite database 
 * associated with Skype.
 */

using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows.Forms;

/**
 * Class for holding a skype contact, including 
 * username and chat history
 */
public class Contact
{
    public string SkypeName; 
    public string Nickname;
    public bool blocked; 
    public List<string> ChatHistory;

    /// <summary>
    ///   Constructor
    /// </summary>
    /// <param name="name"></param> SkypeID of contact
    /// <param name="nick"></param> Their display name in Skype
    /// <param name="block"></param> Whether they are blocked by the active Skype user or not
    public Contact(string name, string nick, bool block)
    {
        this.SkypeName = ParseMSNSkypeName(name);
        this.Nickname = nick;
        this.blocked = block;
        this.ChatHistory = new List<string>();
    }

    /// <summary>
    /// Helper method for parsing "1:" or "live:" at the beginning of strings
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private string ParseMSNSkypeName(string name)
    {
        // A length check so that the subsequent substring methods will not crash
        if (name.Length < 6)
        {
            return name;
        }

        // Check for two variations of non-skype ids (eg. @live.ca, @live.com)

        string x = name.Substring(0, 2);
        if (x.Equals("1:"))
        {
            return name.Substring(2, name.Length - 2);
        }

        x = name.Substring(0, 5);
        if (x.Equals("live:"))
        {
            return name.Substring(5, name.Length - 5);
        }
        return name;
    }
}

public class SkypeExtractor
{
    public string username; // skype username
    public SQLiteConnection cnn; // database connection
    public List<Contact> listOfContacts; // list of contacts

    // SkypeExtractor constructor
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="username"></param> The username that the database is being extracted for. 
    /// <param name="MSN"></param> Whether this is a skype id or email address; not used yet
    public SkypeExtractor(string username, bool MSN)
    {
        this.username = username;

        string directory;

        // to do: check if username is skype name or a microsoft email address
        directory = "C:\\Users\\" + System.Environment.UserName + "\\AppData\\Roaming\\Skype\\" + this.username + "\\main.db";

        // Initialize db connection
        this.cnn = InitializeConnection(directory);
        this.cnn.Open();

        // build your list of contacts
        this.listOfContacts = BuildContactList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param> Directory String
    /// <returns> Returns an active DB connection to the desired Skype database </returns>
    private SQLiteConnection InitializeConnection(string directory)
    {
        // Build connectionString for the database connection
        SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
        builder.ConnectionString =
            "FailIfMissing=true;" +
            "ReadOnly=true;";
        builder["Data Source"] = directory;

        // Initialize db connection and open it
        return new SQLiteConnection(builder.ConnectionString);
    }


    /// <summary>
    /// This creates a list of string arrays of length 3. They contain skype contact
    /// information. Each string array represents one contact and has the following:
    ///     [0]: Skypename (either NULL or a string)
    ///     [1]: Nickname/Fullname (either NULL or a string)
    ///     [2}: Blocked/unblocked (either "1" or NULL)
    /// </summary>
    /// <returns>A list of contacts</returns>
    private List<Contact> BuildContactList()
    {
        // Create db query
        SQLiteCommand contacts_cmd = cnn.CreateCommand();
        contacts_cmd.CommandText =
            "select skypename, fullname, isblocked from Contacts";

        // Execute query
        contacts_cmd.ExecuteNonQuery();

        // Create a reader for the query
        DbDataReader reader = contacts_cmd.ExecuteReader();
        List<Contact> ChatList = new List<Contact>();

        // Process query contents 
        while (reader.Read())
        {
            bool blocked = false;
            if (Convert.ToString(reader[2]) == "1")
                blocked = true;

            Contact c = new Contact(Convert.ToString(reader[0]), Convert.ToString(reader[1]), blocked);

            ChatList.Add(c);
        }

        return ChatList;
    }

    /// <summary>
    /// For the active skype user, it extracts the chats for their desired contacts. Event is
    /// given by the extract button in the GUI.
    /// </summary>
    /// <param name="ContactListToExtract"></param> The list of contacts that are going to get extracted
    /// <param name="directory"></param> Desired directory to store chat histories for said contacts
    public void ExtractChat(ListBox.ObjectCollection ContactListToExtract, string directory)
    {
        // Extract each contact's message history from db file
        for (int i = 0; i < ContactListToExtract.Count; i++)
        {
            Contact c = listOfContacts.Find(x => x.SkypeName == (string)ContactListToExtract[i]);
            PullChat(c);
            WriteFile(c, directory);
        }
    }

    /// <summary>
    /// Writes chat history files for a Contact c
    /// </summary>
    /// <param name="c"></param> The contact object. Assumes there is already chat histories there.
    /// <param name="dir"></param> Desired directory to write the file.
    public void WriteFile(Contact c, string dir)
    {
        int number = 0;
        string directory = String.Format(@"{0}\{1}.txt", dir, c.SkypeName);

        while (System.IO.File.Exists(directory))
        {
            directory = String.Format(@"{0}\{1}{2}.txt", dir, c.SkypeName, (++number));
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory))
        {
            foreach (string line in c.ChatHistory)
            {
                file.WriteLine(line);
            }
        }


    }

    /// <summary>
    /// It is assumed that there is an active database connection currently running
    /// on a valid database containing skype information. This creates a list of 
    /// strings that contain the message history for a given user, which will be 
    /// returned to the method caller.
    /// </summary>
    /// <param name="c"></param> current contact for message history to be extracted
    public void PullChat(Contact c)
    {
        // Create db query
        SQLiteCommand contacts_cmd = this.cnn.CreateCommand() as SQLiteCommand;
        contacts_cmd.CommandText =
            "SELECT author, from_dispname, datetime(timestamp, 'unixepoch') as date, body_xml FROM Messages where dialog_partner = \'" + c.SkypeName + "\' ORDER BY timestamp;";

        // Execute query
        contacts_cmd.ExecuteNonQuery();

        // Create a reader for the query
        DbDataReader reader = contacts_cmd.ExecuteReader();

        // Process query contents 
        while (reader.Read())
        {
            string msg = "";
            msg += Convert.ToString(reader[0] + " (" + reader[1] + ")\t\t\t " + reader[2] + ": " + reader[3]);
            c.ChatHistory.Add(msg);
        }
    }

}


