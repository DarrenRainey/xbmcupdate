using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using XbmcUpdate.Runtime;


namespace XbmcUpdate.Tools
{

    [Target("GuiNlogTarget")]
    internal sealed class GuiNlogTarget : TargetWithLayout
    {
        protected override void Write(LogEventInfo logEvent)
        {
            string logMessage = CompiledLayout.GetFormattedMessage(logEvent);
            UpdateGui.Log(logMessage);
        }

    }
}
