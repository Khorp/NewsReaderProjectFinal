using NUnit.Framework;
using System;
using System.Net;
using System.Net.Sockets;

namespace NewsReaderProjectTest
{
    public class Tests
    {
        String response;
        byte[] downBuffer;
        byte[] byteSendInfo;
        int bytesSize;

        String url1;
        int port;

        TcpClient socket;

        NetworkStream ns;



        [SetUp]
        public void Setup()
        {
            downBuffer = new byte[2048];
            byteSendInfo = new byte[2048];
            
            url1 = "news.sunsite.dk";
            port = 119;

            socket = new TcpClient();

            ns = null;
        }

        [Test]
        public void Test1()
        {
            IPAddress ip = Dns.GetHostEntry(url1).AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            //(1) this blocks until connection is reached or fails
            socket.Connect(endPoint);

            //(2) get the streams
            ns = socket.GetStream();
            GetResponse(ns);
            Write(ns, "authinfo user rasm523c@easv365.dk");
            GetResponse(ns);
            Write(ns, "authinfo pass 1de023");
            response = GetResponse(ns);


            Assert.IsTrue(response.Contains("281"));
        }
        [TearDown]
        public void TearDown()
        {
            ns.Close();
            socket.Close();
        }

        public string GetResponse(NetworkStream ns)
        {
            bytesSize = ns.Read(downBuffer, 0, downBuffer.Length);
            response = System.Text.Encoding.ASCII.GetString(downBuffer, 0, bytesSize);
            return response + "\n";
        }
        public void Write(NetworkStream ns, String messsage)
        {
            byteSendInfo = StringToByteArr(messsage + "\r\n");
            ns.Write(byteSendInfo, 0, byteSendInfo.Length);
        }
        public void GetResponses(NetworkStream ns)
        {
            while ((bytesSize = ns.Read(downBuffer, 0, downBuffer.Length)) > 0)
            {
                String newChunk = System.Text.Encoding.ASCII.GetString(downBuffer, 0, bytesSize);
                response += newChunk;
                if (newChunk.Substring(newChunk.Length - 5, 5) == "\r\n.\r\n")
                {
                    // Remove the "\r\n.\r\n" from the end of the string
                    response = response.Substring(0, response.Length - 3);
                    break;
                }
            }
            string[] test = response.Split('\n');
            foreach (String item in test)
            {
                Console.WriteLine(item + "\n");
            }
        }

        public byte[] StringToByteArr(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }

    }
}