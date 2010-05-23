using System;
using System.Runtime.InteropServices;
using NLog;

namespace XbmcUpdate.UpdateEngine
{
    static class PowerManager
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        private enum EXECUTION_STATE : uint
        {
            ES_SYSTEM_REQUIRED = 0x00000001,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_CONTINUOUS = 0x80000000,
        }

        internal static void PreventStandBy()
        {
            SetState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }

        internal static void Resume()
        {
            SetState(EXECUTION_STATE.ES_CONTINUOUS);
        }

        private static void SetState(EXECUTION_STATE state)
        {
            Logger.Info("Setting System State to [{0}]", EXECUTION_STATE.ES_CONTINUOUS);
            try
            {
                SetThreadExecutionState(state);
            }
            catch (Exception e)
            {
                Logger.Error("An error has occurred while setting system state. {0}", e.ToString());
            }
        }
    }
}
