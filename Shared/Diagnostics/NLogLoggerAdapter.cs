﻿#region

using NLog;

#endregion

namespace Shared.Diagnostics
{
    public class NLogLoggerAdapter : ILoggerAdapter
    {
        private readonly Logger _logger;

        public NLogLoggerAdapter(string className)
        {
            _logger = LogManager.GetLogger(className);
        }

        #region Implementation of ILoggerAdapter

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        #endregion
    }
}