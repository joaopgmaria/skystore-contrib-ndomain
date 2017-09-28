﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDomain.Logging
{
    /// <summary>
    /// ILoggerFactory implementation to be used when no logging is intended.
    /// Besides unit tests, this should never be used.
    /// </summary>
    public class NullLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Singleton instance for the NullLoggerFactory
        /// </summary>
        public static readonly ILoggerFactory Instance = new NullLoggerFactory();
        static readonly ILogger NullLogger = new NullLogger();

        private NullLoggerFactory() { }

        public ILogger GetLogger(string name)
        {
            return NullLogger;
        }

        public ILogger GetLogger(Type type)
        {
            return NullLogger;
        }
    }

    class NullLogger : ILogger
    {
        public void Debug(string message, params object[] args)
        {
            
        }

        public void Info(string message, params object[] args)
        {
            
        }

        public void Warn(string message, params object[] args)
        {
            
        }

        public void Warn(Exception exception, string message, params object[] args)
        {
            
        }

        public void Error(string message, params object[] args)
        {
            
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            
        }

        public void Fatal(string message, params object[] args)
        {
            
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            
        }
    }

}
