using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    static class UpdController
    {
        private static IPEndPoint endPoint;
        private static UdpClient udpClient;
        private static UdpClient client = new UdpClient();
        static UpdController()
        {
            var ip = IPAddress.Parse(ConfigurationManager.AppSettings.Get("ip"));
            var listenerPort = int.Parse(ConfigurationManager.AppSettings.Get("listenerPort"));
            var sendPort = int.Parse(ConfigurationManager.AppSettings.Get("sendPort"));
            endPoint = new IPEndPoint(ip, sendPort);
            udpClient = new UdpClient(listenerPort);
            
            StartListener();
        }



        public static void SendColor(Color color)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                   
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, color);
                    var bytes = ms.ToArray();
                    client.Send(bytes, bytes.Length, endPoint);
                }
            }
            catch (Exception e)
            {
                using (StreamWriter fs = new StreamWriter(@"D:\Log.txt", true))
                {
                    fs.WriteLine(e.Message);
                }
            }
        }

        private static async void StartListener()
        {
            while (true)
            {
                var data = (await udpClient.ReceiveAsync()).Buffer;
                if (data[0] == 1) Program.Service.Stop();
            }
        }
    }
}
