using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

namespace NewsReaderProject.MVVM.Model
{
    /// <summary>
    /// My socket class i use it for all the server commands.
    /// </summary>
    public class SocketHelper
    {
        //this is the port i use, it is always this port so you cant change it.
        private readonly int port = 119;
        private String response;
        private byte[] downBuffer = new byte[2048];
        private byte[] byteSendInfo = new byte[2048];
        private int bytesSize;

        private TcpClient socket;

        private NetworkStream ns = null;
        private string _server;

        /// <summary>
        /// used to login to the server
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="server"></param>
        public void Connect(string username, string password, string server)
        {
            _server = server;
            setupConncetion();

            GetResponse();
            Write("authinfo user "+username);
            GetResponse();
            Write("authinfo pass "+password);
            response = GetResponse();
        }
        /// <summary>
        /// used to make sure the socket and networkstream has what they need to work.
        /// </summary>
        public void setupConncetion()
        {
            IPAddress ip = Dns.GetHostEntry(_server).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            //(1) this blocks until connection is reached or fails
            socket = new TcpClient();
            socket.Connect(endPoint);

            //(2) get the streams
            ns = socket.GetStream();
        }

        /// <summary>
        /// i use this to get the groups. from the server.
        /// i remove all the 00000 00100012 thing and i also remove some empty, and respones that arent groups.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> GiveGroups()
        {
            Write("list");
            List<string> temp = GetResponses().ToList();
            for(int i =0;i<temp.Count;i++)
            {
                temp[i] = temp[i].Split(' ')[0].Replace(" ", string.Empty);
            }
            temp.RemoveAll(x => ((string) x)=="");
            temp.RemoveAll(x => ((string)x).Contains("281"));
            temp.RemoveAll(x => ((string)x).Contains("215"));
            temp.RemoveAll(x => ((string)x).Contains("240"));
            //here convert the string list into a group list.
            ObservableCollection<string> tempC = new ObservableCollection<string>();
            foreach (var item in temp)
            {
                tempC.Add(item);
            }
            return tempC;
        } 

        /// <summary>
        /// used to get all the headlines from a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public ObservableCollection<Article> GiveArticles(string group)
        {
            Write("group "+group);
            string check = GetResponse();
            int startNumber = int.Parse(check.Split(' ')[2]);
            int endNumber = int.Parse(check.Split(' ')[3]);

            List<string> test = GetHeadlines(startNumber,endNumber);
            //loop trough all the articles
            
            //add them to list
            ObservableCollection<Article> tempA = new ObservableCollection<Article>();
            foreach (string item in test)
            {
                tempA.Add(new Article() { Headline = item.Split('\t')[1].Replace("=?UTF-8?Q?",string.Empty),
                    ArticleID = item.Split('\t')[0].Trim('\n')});
            }
            
            return tempA;
        }

        /// <summary>
        /// my logout command it just send quit to the server and closes the socket and stream.
        /// </summary>
        public void Quit()
        {
            Write("quit");
            ns.Close();
            socket.Close();
        }

        /// <summary>
        /// my read article method it gets an article obect and uses it to get the article then it returns the text.
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public string ReadArticle(Article article)
        {
            Write("body "+article.ArticleID);

            string[] test = GetResponses();
            string temp = "";

            foreach (var item in test)
            {
                temp += item;
            }


            return temp;
        }
        /// <summary>
        /// my post method it gets all the things it needs from the postingviewmodel and then posts.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="group"></param>
        /// <param name="Subject"></param>
        /// <param name="message"></param>
        public void PostArticle(string username,string group,string Subject,string message)
        {
            //string postString = "post:\r\nFrom:"+username+"\r\nSubject:"+Subject+"\r\nNewsGroups:"+group+"\r\n\r\n"+message+"\r\n\r\n.\r\n";
            Write("Post");
            string test2 = GetResponse();
            if (test2.Contains("500"))
            {
                
            }
            Write("From: "+username);
            Write("NewsGroups: "+group);
            Write("Subject: " + Subject+"\n");
            Write("\nMessage: "+message);
            Write("\r\n.");

            string test = GetResponse();
            //a messagebox to see if it got posted.
            MessageBox.Show(test);
        }

        /// <summary>
        /// a helper method for getting all the headlines.
        /// </summary>
        /// <param name="startNumber"></param>
        /// <param name="endNumber"></param>
        /// <returns></returns>
        private List<string> GetHeadlines(int startNumber, int endNumber) 
        {
            List<string> test = new List<string>();
            for (int i = startNumber; i <= endNumber; i++)
            {
                Write("xover " + i);
                test.Add(GetResponse().Split("224 data follows\r")[1]);
                Thread.Sleep(2);
            }
            test.RemoveAll(x => ((string)x) == "\n.\r\n\n");

            return test;
        }

        /// <summary>
        /// a helper method to get an respone from the server
        /// </summary>
        /// <returns></returns>
        private string GetResponse()
        {
            bytesSize = ns.Read(downBuffer, 0, downBuffer.Length);
            response = System.Text.Encoding.UTF8.GetString(downBuffer, 0, bytesSize);
            return response + "\n";
        }
        /// <summary>
        /// my write method it gets what you want to write to the server and then adds some ascii end
        /// charecter and converts it into a bytes for the networkstream write.
        /// </summary>
        /// <param name="messsage"></param>
        private void Write(String messsage)
        {
            byteSendInfo = new byte[2048];
            byteSendInfo = StringToByteArr(messsage + "\r\n");
            ns.Write(byteSendInfo, 0, byteSendInfo.Length);
        }
        /// <summary>
        /// a loop get responeses for a bit longer respsones.
        /// </summary>
        /// <returns></returns>
        private string[] GetResponses()
        {
            while ((bytesSize = ns.Read(downBuffer, 0, downBuffer.Length)) > 0)
            {
                String newChunk = System.Text.Encoding.UTF8.GetString(downBuffer, 0, bytesSize);
                response += newChunk;
                if (newChunk.Substring(newChunk.Length - 5, 5) == "\r\n.\r\n")
                {
                    // Remove the "\r\n.\r\n" from the end of the string
                    response = response.Substring(0, response.Length - 3);
                    break;
                }
            }
            string[] test = response.Split('\n');

            return test;
        }

        /// <summary>
        /// a convert string to byte array method.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private byte[] StringToByteArr(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// my Deconstructor so when to program closes it closes the socket and stream.
        /// </summary>
        ~SocketHelper()
        {
            ns.Close();
            socket.Close();
        }
    }
}
