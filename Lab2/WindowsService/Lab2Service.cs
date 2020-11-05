using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    public partial class Lab2Service : ServiceBase
    {
        private CamObserver _camObs;

        public Lab2Service()
        {
            InitializeComponent();
            _camObs = new CamObserver();
        }

        protected override void OnStart(string[] args) =>
            _camObs.Start();

        protected override void OnStop() =>
            _camObs.Stop();
    }
}
