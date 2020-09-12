using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Minesweeper_Web_Application.Services.Utility
{
    public class MineSweeperLogger : ILogger
    {
        private static MineSweeperLogger _instance;
        private static Logger logger;

        public static MineSweeperLogger GetInstance()
        {
            if(_instance == null)
            {
                _instance = new MineSweeperLogger();
            }
            return _instance;
        }

        private Logger GetLogger(string logger)
        {
            if(MineSweeperLogger.logger == null)
            {
                MineSweeperLogger.logger = LogManager.GetLogger("fileLogger");
            }
            return MineSweeperLogger.logger;
        }

        public void Debug(string message)
        {
            GetLogger("fileLogger").Debug(message);
        }

        public void Error(Exception e, string message)
        {
            GetLogger("fileLogger").Error(e, message);
        }

        public void Info(string message)
        {
            GetLogger("fileLogger").Info(message + " " + CallingType());
        }

        public void Warning(string message)
        {   
            GetLogger("fileLogger").Warn(message);
        }

        private static string CallingType()
        {
            StackFrame frame = new StackFrame(2);
            string result = frame.GetMethod().Name + " HI " + frame.GetMethod().DeclaringType.Name;
            return result;
        }
    }
}