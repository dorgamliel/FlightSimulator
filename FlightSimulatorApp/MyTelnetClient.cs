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
        private TcpClient myClient;
        public void connect(string ip, int port)
        {
            myClient = new TcpClient();
            var result = myClient.BeginConnect(ip, port, null, null);
            var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(1));
            if (!success)
                throw new Exception();
        }

        public void disconnect()
        {
            myClient.Close();
        }

        public string read()
        {
            String responseData = String.Empty;
            try
            {
                NetworkStream stream = myClient.GetStream();
                Byte[] data = new Byte[256];
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (NullReferenceException e){
                Console.WriteLine(e);
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine(e);
            }
            return responseData;
        }

        public void write(string command)
        {
            command += "\r\n";
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            try
            {
                NetworkStream stream = myClient.GetStream();
                stream.Write(data, 0, data.Length);
                Console.WriteLine("Sent: {0}", command);
            } catch(Exception e)
            {

            }
            
        }
    }
}
