using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Windows_Service.Operations
{
    internal class OMGWindowsService
    {
        private bool isStopRequestReceived;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern uint SetThreadExecutionState(uint esFlags);

        private const uint ES_CONTINUOUS = 0x80000000;
        private const uint ES_SYSTEM_REQUIRED = 0x00000001;
        private const uint ES_DISPLAY_REQUIRED = 0x00000002;

        public void OnStart()
        {
            Task.Run(() => InitService());
        }

        public void OnStop()
        {
            isStopRequestReceived = true;
        }

        public void OnPause()
        {
            isStopRequestReceived = true;
        }

        public void OnContinue()
        {
            isStopRequestReceived = false;
        }

        private void InitService()
        {
            SetThreadExecutionState(ES_CONTINUOUS | ES_SYSTEM_REQUIRED | ES_DISPLAY_REQUIRED);

            while (!isStopRequestReceived)
            {
                try
                {
                    int SleepTime = Convert.ToInt32(ConfigurationManager.AppSettings["ServiceWorkTime"]);
                    Operation.SendDokum();
                    Thread.Sleep(SleepTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }
            }

            SetThreadExecutionState(ES_CONTINUOUS);
        }
    }
}
