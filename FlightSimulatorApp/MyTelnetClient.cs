using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    class MyTelnetClient : ITelnetClient
    {
        private TcpClient myClient = new TcpClient();
        //Connection to server.
        public void Connect(string ip, int port)
        {
            myClient = new TcpClient();
            myClient.ReceiveTimeout = 10000;
            myClient.SendTimeout = 10000;
            if (!myClient.ConnectAsync(ip, port).Wait(1000))
                throw new Exception();
        }
        //Dinsonnection from server.
        public void Disconnect()
        {
            myClient.Close();
        }
        //Reading from server.
        public string Read()
        {
            //TODO: remove printing to console
            string responseData = string.Empty;
            NetworkStream stream = myClient.GetStream();
            Byte[] data = new Byte[256];
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);
            return responseData;
        }
        //Writing to server.
        public void Write(string command)
        {
            //TODO: remove printing to console
            command += "\r\n";
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            NetworkStream stream = myClient.GetStream();
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", command);
        }
    }
}
