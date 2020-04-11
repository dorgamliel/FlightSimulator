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
        public void connect(string ip, int port)
        {
            myClient = new TcpClient();
            myClient.ReceiveTimeout = 10000;
            myClient.SendTimeout = 10000;
            if (!myClient.ConnectAsync(ip, port).Wait(1000))
                throw new Exception();
        }

        public void disconnect()
        {
            myClient.Close();
        }

        public string read()
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

        public void write(string command)
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
