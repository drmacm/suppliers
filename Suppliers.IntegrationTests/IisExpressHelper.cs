using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Suppliers.IntegrationTests
{
    public static class IisExpressHelper
    {
        private static Process iisProcess;

        private static string pathToIisExpress = @"C:\Program Files (x86)\IIS Express\iisexpress.exe";
        private static string pathToWebsite = @"C:\dev\my\dotNET\suppliers\Suppliers.Web";
        private static int port = 54401;

        public static void StartIis()
        {
            if (iisProcess == null)
            {
                var thread = new Thread(StartIisExpress) { IsBackground = true };
                thread.Start();
            }
        }

        private static void StartIisExpress()
        {
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = string.Format("/path:\"{0}\" /port:{1}", pathToWebsite, port)
            };

            startInfo.FileName = pathToIisExpress;
            try
            {
                iisProcess = new Process { StartInfo = startInfo };
                iisProcess.Start();
                iisProcess.WaitForExit();
            }
            catch
            {
                iisProcess.CloseMainWindow();
                iisProcess.Dispose();
            }
        }

        public static void StopIis()
        {
            if (iisProcess != null)
            {
                iisProcess.CloseMainWindow();
                iisProcess.Dispose();
            }
        }
    }
}
    
        