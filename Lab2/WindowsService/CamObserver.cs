using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AForge.Video;
using AForge.Video.DirectShow;

namespace WindowsService
{
    class CamObserver
    {
        private VideoCaptureDevice _videoSource;
        public CamObserver()
        {
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            _videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            _videoSource.NewFrame += VideoSource_newFrame;
        }

        public void Start() =>
            _videoSource.Start();

        public void Stop() =>
            _videoSource.Stop();

        private void VideoSource_newFrame(object sender, NewFrameEventArgs eventargs)
        {  
            var h = eventargs.Frame.Height;
            var w = eventargs.Frame.Width;
            double[] rgb = new double[3];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    var color = eventargs.Frame.GetPixel(i, j);
                    rgb[0] += color.R;
                    rgb[1] += color.G;
                    rgb[2] += color.B;
                }
            }

            rgb = rgb.Select(x => Math.Round(x / (w * h))).ToArray();

            var result = Color.FromArgb((int)rgb[0], (int)rgb[1], (int)rgb[2]);
            UpdController.SendColor(result);
        }
    }
}
