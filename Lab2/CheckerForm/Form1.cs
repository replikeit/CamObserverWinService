using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckerForm
{
    public partial class Form1 : Form
    {
        private UdpClient client;
        private IPEndPoint endPoint;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            var ip = IPAddress.Parse(ConfigurationManager.AppSettings.Get("ip"));
            var listenerPort = int.Parse(ConfigurationManager.AppSettings.Get("listenerPort"));
            var sendPort = int.Parse(ConfigurationManager.AppSettings.Get("sendPort"));
            endPoint = new IPEndPoint(ip, sendPort);
            client = new UdpClient(listenerPort);

            while (true)
            {
                var data = await client.ReceiveAsync();
                var formatter = new BinaryFormatter();
                Color color;
                using (MemoryStream ms = new MemoryStream(data.Buffer))
                {
                    color = (Color)formatter.Deserialize(ms);
                }
                label1.ForeColor = color;
                label1.Text = $"Avg Cam Color = ({color.R}, {color.G}, {color.B})";
            }
        }

        private async void StopButton_Click(object sender, EventArgs e)
        {
            await new UdpClient().SendAsync(new byte[] { 2 }, 1, endPoint);
        }
    }
}
