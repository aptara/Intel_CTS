using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntelGameAPI
{

    public static class Logger
    {
        private static log4net.ILog Log { get; set; }

        public static void Init(string configPath)
        {
            log4net.Config.XmlConfigurator.Configure(
                new System.IO.FileInfo(configPath));
             Log = LogManager.GetLogger("Common");
        }

        static Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        public static void Error(object msg)
        {
            Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public static void Info(object msg)
        {
            Log.Info(msg);
        }
    }
}