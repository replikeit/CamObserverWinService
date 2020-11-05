using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        public static Lab2Service Service = new Lab2Service();

        static void Main()
        {
            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                Service
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
