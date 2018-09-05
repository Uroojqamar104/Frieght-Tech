using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using FreightTech.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http.ExceptionHandling;

namespace FreightTech.Api
{
    public interface IAppLogger
    {
        void Info(int userId, string message);
        void Warning(int userId, string message);
        void Error(int userId, ExceptionLoggerContext exception);
        void Error(int userId, Exception exception, System.Net.Http.HttpRequestMessage request);
    }

    public class AppLogger : IAppLogger, IDisposable
    {
        FreightTechContext context = new FreightTechContext();

        string ApplicationName = "FreightTechApi";
        public AppLogger()
        {

        }

        static IAppLogger _instance;
        public static IAppLogger Instance {
            get {
                if (_instance == null)
                {
                    _instance = new AppLogger();
                }
                return _instance;
            }
        }

        public void Info(int userId, string message)
        {
            var log = new ApplicationLog
            {
                Application = ApplicationName,
                Type = "Info",
                Message = message
            };
            Log(log);
        }

        public void Warning(int userId, string message)
        {
            var log = new ApplicationLog
            {
                Application = ApplicationName,
                Type = "Warning",
                Message = message
            };
            Log(log);
        }

        public void Error(int userId, ExceptionLoggerContext context)
        {
            var message = new ApplicationLog
            {
                Application = ApplicationName,
                Type = "Error",
                Message = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                RequestUri = context.Request.RequestUri.AbsoluteUri,
                InnerException = context.Exception.InnerException == null ? string.Empty : context.Exception.InnerException.Message
            };
            Log(message);
        }

        public void Error(int userId, Exception exception, System.Net.Http.HttpRequestMessage request)
        {
            var message = new ApplicationLog
            {
                Application = ApplicationName,
                Type = "Error",
                Message = exception.Message,
                StackTrace = exception.StackTrace,
                RequestUri = request.RequestUri.AbsoluteUri,
                InnerException = exception.InnerException == null ? string.Empty : exception.InnerException.Message
            };
            Log(message);
        }

        void Log(ApplicationLog log)
        {
            try
            {
                log.Date = DateTime.Now;

                context.ApplicationLog.Add(log);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return;
            }
        }


        #region Dispose
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        #endregion

    }

}

