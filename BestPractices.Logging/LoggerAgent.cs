﻿using Serilog;
using System;
using System.IO;

namespace BestPractices.Logging
{
    public class LoggerAgent : ILoggerAgent
    {
        //placeholder to write to App_Center
        private readonly string APP_CENTER_SECRET = "";

        public LoggerAgent()
        {
            var logpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/log.txt";

            if (!File.Exists(logpath))
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/log.txt")
                    //.WriteTo.AppCenterSink(
                    //   new Serilog.Core.LoggingLevelSwitch(
                    //     Serilog.Events.LogEventLevel.Debug),
                    //     Serilog.Events.LogEventLevel.Debug,
                    //     AppCenterTarget.ExceptionsAsCrashesAndEvents,
                    //      APP_CENTER_SECRET)
                    .CreateLogger();
            }
        }
        public void Debug(string debugInformation)
        {
            Log.Logger.Debug(debugInformation);
        }
        public void Information(string informationMessage)
        {
            Log.Logger.Information(informationMessage);
        }
        public void Warning(string warningMessage)
        {
            Log.Logger.Warning(warningMessage);
        }
        public void Error(string errorMessage)
        {
            Log.Logger.Error(errorMessage);
        }
    }
}
