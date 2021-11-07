using NewsReaderProject.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// my data class for the login, it has a username,password and the newsgroup.
    /// everytime you change the text in the view it also updates it into a json file.
    /// </summary>
    public class UserData : ObjectUpdater
    {
        private readonly string JsonfileUrl = @"C:\Users\Morta\source\repos\NewsReaderProjectTest\NewsReaderProject\JsonFile\LoginInfo.txt";
        private string _username;
        private string _password;
        private string _newsServer;
        public string Username 
        { 
            get { return _username; } 
            set { _username = value; OnPropertyChanged(); UpdateTextFile(value); } 
        }
        public string Password 
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); UpdateTextFile(value); }
        }
        public string NewsServer 
        {
            get { return _newsServer; }
            set { _newsServer = value; OnPropertyChanged(); UpdateTextFile(value); }
        }

        /// <summary>
        /// here when the program starts we load the json data into the propery's
        /// </summary>
        public UserData()
        {
            loadJsoninfo();
        }
        /// <summary>
        /// used for saving the data into the json file
        /// </summary>
        /// <param name="input"></param>
        public void UpdateTextFile(string input)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(JsonfileUrl))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }
        /// <summary>
        /// used for the loading of json info into the property's
        /// </summary>
        public void loadJsoninfo() 
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            string tempData = "";

            using (StreamReader sr = new StreamReader(JsonfileUrl))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                tempData = serializer.Deserialize(reader).ToString();
            }
            //i use serperators so i get only the text i want.
            char[] seperators = { ',', ':', '{', '}','\r','\n',' '};
            //i replace " with nothing
            tempData = tempData.Replace("\"",String.Empty);

            string[] user = tempData.Split(seperators , StringSplitOptions.RemoveEmptyEntries);
            //i have to used odd numbers beacuse of how the array gets created.
            Username = user[1].Substring(0, user[1].Length);
            //tempdata Password
            Password = user[3].Substring(0, user[3].Length);
            //tempdata newsserver
            NewsServer = user[5].Substring(0, user[5].Length);
        }
    }
}
