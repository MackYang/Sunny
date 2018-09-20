using Microsoft.Extensions.Options;
using Sunny.Common.ConfigOption;
using Sunny.Common.Enum;
using Sunny.Common.Helper.File;
using Sunny.Common.Helper.Net;
using Sunny.Common.Helper.String;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sunny.Common.Helper.Log
{
    class NetLoggerProcessor : IDisposable
    {
        private const int _maxQueuedMessages = 1024;

        private readonly BlockingCollection<LogData> _messageQueue = new BlockingCollection<LogData>(_maxQueuedMessages);
        private readonly Thread _outputThread;
        private readonly NetLoggerOption option;


        public NetLoggerProcessor(NetLoggerOption option)
        {
            this.option = option;
            // Start Console message queue processor
            _outputThread = new Thread(ProcessLogQueue)
            {
                IsBackground = true,
                Name = "Net logger queue processing thread"
            };
            _outputThread.Start();
        }

        public virtual void EnqueueMessage(LogData message)
        {
            if (!_messageQueue.IsAddingCompleted)
            {
                try
                {
                    _messageQueue.Add(message);
                    return;
                }
                catch (InvalidOperationException ex) { WriteOffLineLog("AddLogQueue Err", ex); }
            }

            // Adding is completed so just log the message
            WriteMessage(message);
        }

        // for testing
        internal virtual async void WriteMessage(LogData log)
        {
            //Console.WriteLine($"NetXXX:Message{message.Message},Level:{message.LevelString}")

            await AddLog(log.LevelString, log.Message, null, log.Exception?.StackTrace);
        }




        private async Task<bool> AddLog(string logLevel, string logMessage, string attData, string stackInfo)
        {
            bool flag = false;

            var jsonObj = new
            {
                systemId = option.SystemId,
                logLevel = logLevel,
                logMessage = logMessage,
                attData = attData,
                stackInfo = stackInfo
            };
            string json = JsonHelper.ToJsonString(jsonObj);
            try
            {
                var res = await NetHelper.PostWithJson(option.Url, json);
                OPResult opRes = JsonHelper.FromJsonString<OPResult>(res);
                if (opRes.State == 1)
                {
                    flag = true;
                }
                else
                {
                    WriteOffLineLog(json);
                }
            }
            catch (Exception ex)
            {
                WriteOffLineLog(json, ex);
            }

            return flag;

        }



        private void WriteOffLineLog(string originalLogInfo, Exception offlineExInfo = null)
        {

            try
            {
                string offlineFilePath = $@"C:\SunnyOfflineLog\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

                string data = $"Time:{DateTime.Now.ToNormalString()}\r\n\r\nOriginalLogInfo:{originalLogInfo}";
                if (offlineExInfo != null)
                {
                    data += $"\r\n\r\nOfflineExceptionInfo:{JsonHelper.ToJsonString(offlineExInfo)}";
                }
                data += "\r\n\r\n\r\n\r\n\r\n";

                FileHelper.WriteFile(offlineFilePath, data);

            }
            catch (Exception ex)
            {
                Console.WriteLine(JsonHelper.ToJsonString(ex));

            }

        }

        private class OPResult
        {
            public int State { get; set; }

            public object Data { get; set; }

        }




        private void ProcessLogQueue()
        {
            try
            {
                foreach (var message in _messageQueue.GetConsumingEnumerable())
                {
                    WriteMessage(message);
                }
            }
            catch
            {
                try
                {
                    _messageQueue.CompleteAdding();
                }
                catch (Exception ex){ WriteOffLineLog("AddLogQueue Err",ex); }
            }
        }

        public void Dispose()
        {
            _messageQueue.CompleteAdding();

            try
            {
                _outputThread.Join(1500); // with timeout in-case Console is locked by user input
            }
            catch (ThreadStateException) { }
        }
    }
}
