using Igloo.History;
using Igloo.Logger;
using System;

namespace IglooBrowser.Startup
{
    public class ClosingEvents
    {
        /// <summary>
        /// Runs this thread when the application is going to close.
        /// </summary>
        public static void RunClosingEvents()
        {
            ILogger.WriteLog();
            IHistory.WriteHistory();
            Environment.Exit(0);
        }
    }
}
