/**
 * Wesley Chow
 * Oct 29, 2014
 * Skype.cs
 * 
 * Description: Object class for extracting chat history 
 * for one user.
 */

using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows.Forms;

/*
 * Class for holding skype contact information, including username and chat history
 */
public class Contact
{
    public string SkypeName;
    public string Nickname;
    public bool blocked;
    public List<string> ChatHistory;

    public Contact (string name, string nick,bool block)
    {
        string n = ParseMSNSkypeName(name);
        this.SkypeName = n;
        this.Nickname = nick;
        this.blocked = block;
        this.ChatHistory = new List<string>();
    }

    /*
     * Helper method for parsing "1:" or "live:" at the beginning of strings
     */
    private string ParseMSNSkypeName(string name)
    {
        if (name.Length < 6)
        {
            return name;
        }

        string x = name.Substring(0,2);
        if (x.Equals("1:"))
        {
            return name.Substring(2,name.Length-2);
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
    public DbConnection cnn; // database connection
    public List<Contact> listOfContacts; // list of contacts

    /**
     * SkypeExtractor() (constructor)
     * [Parameters] 
     * username: name of skype user
     * [Description]
     * Initializes path to database file and opens a database connection.
     */
    public SkypeExtractor(string username, bool MSN)
    {
        this.username = username;

        string directory;

        // to do: check if username is skype name or a microsoft email address
        directory = "C:\\Users\\"+System.Environment.UserName+"\\AppData\\Roaming\\Skype\\" + this.username + "\\main.db";


        // Build connectionString for the database connection
        SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder();
        builder.ConnectionString = 
            "FailIfMissing=true;" +
            "ReadOnly=true;";
        builder["Data Source"] = directory;

        // Initialize db connection and open it
        this.cnn = new SQLiteConnection(builder.ConnectionString);
        cnn.Open();

        // build your list of contacts
        this.listOfContacts = BuildContactList();
    }

    /*
     * Name: GetContactList()
     * [Parameters]
     * NONE
     * [Description]
     * It is assumed that there is an active database connection currently running
     * on a valid database containing skype information.
     * This creates a list of string arrays, length 3, which contain skype contact
     * information. Each string array represents one contact and has the following:
     *      [0]: Skypename (Either NULL or a string)
     *      [1]: Nickname/Fullname (Either NULL or a string)
     *      [2]: Blocked or unblocked (Either "1" or NULL)
     * It will return that list of string arrays.
     * **/
    private List<Contact> BuildContactList()
    {
        // Create db query
        SQLiteCommand contacts_cmd = cnn.CreateCommand() as SQLiteCommand;
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

            Contact c = new Contact(Convert.ToString(reader[0]), Convert.ToString(reader[1]),blocked); 

            ChatList.Add(c);
            PullChat(c);
        }
        
        // For debugging
        ChatList.Sort(SortContact);
        //ChatList.ForEach(Print);

        return ChatList;
    }

    /*
     * returns sorted list for string (for debug)
     */
    private int SortContact(Contact x, Contact y)
    {
        if (x.SkypeName == null)
        {
            if (y.SkypeName == null)
            {
                return 0; // both null
            }
            else
            {
                return -1; // x < y (x is null)
            }
        }
        else
        {
            if (y.SkypeName == null)
            {
                return 1; // x > y (y is null)
            }
            else
            {
                return (x.SkypeName).CompareTo(y.SkypeName); // returns x < y 
            }
        }

    }

    /*
     * Logic to program...
     * 1. Extract list of your contacts (all)
     * 2. Using each contact, extract chat history by contact
     * 3. Format it as XML, JSON, or whatever
     */
    public void ExtractChat(ListBox.ObjectCollection ContactListToExtract, string directory)
    {
        // Extract each contact's message history from db file
        for (int i=0; i<ContactListToExtract.Count; i++)
        {
            Contact c = GetContact((string)ContactListToExtract[i]);
            WriteFile(c, directory);
        }
    }

    private Contact GetContact(string name)
    {
        return listOfContacts.Find(x => x.SkypeName == name);
    }

    public void WriteFile(Contact c, string dir)
    {
        int number = 0;
        string directory = String.Format(@"{0}\{1}.txt",dir,c.SkypeName);

        while (System.IO.File.Exists(directory))
        {
            directory = String.Format(@"{0}\{1}{2}.txt",dir,c.SkypeName,(++number));
        }

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(directory))
        {
            foreach (string line in c.ChatHistory)
            {
                file.WriteLine(line);
            }
        }

        
    }

    /*
     * Name: PullChat()
     * [Parameters]
     * (string) curr_contact - current contact for message history to be extracted
     * [Description]
     * It is assumed that there is an active database connection currently running
     * on a valid database containing skype information.
     * This creates a list of strings that contain the message history for a given
     * user, which will be returned to the method caller.
     * **/
    public void PullChat(Contact curr)
    {
        // Create db query
        SQLiteCommand contacts_cmd = cnn.CreateCommand() as SQLiteCommand;
        contacts_cmd.CommandText =
            "SELECT author, from_dispname, datetime(timestamp, 'unixepoch') as date, body_xml FROM Messages where dialog_partner = \'"+curr.SkypeName+"\' ORDER BY timestamp;";

        // Execute query
        contacts_cmd.ExecuteNonQuery();

        // Create a reader for the query
        DbDataReader reader = contacts_cmd.ExecuteReader();
        List<Contact> x = new List<Contact>();

        // Process query contents 
        while (reader.Read())
        {
            string msg = "";
            msg += Convert.ToString(reader[0]+" ("+reader[1]+") "+reader[2]+": "+reader[3]);
            curr.ChatHistory.Add(msg);
        }
    }

    private void Print(Contact c)
    {
        string x = string.Format(
            "{0,-35} {1,-30} {2,-20}",
            c.SkypeName,
            c.Nickname,
            c.blocked);
        Console.WriteLine(x);        
    }

    /**
     * CloseConnection()
     * [Parameters] 
     * NONE
     * [Description]
     * Closes database connection.
     */
    public void CloseConnection()
    {
        cnn.Close();
    }

}


