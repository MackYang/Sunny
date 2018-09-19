using Microsoft.Extensions.Options;
using Sunny.Common.ConfigOption;
using Sunny.Common.Enum;
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
                catch (InvalidOperationException) { }
            }

            // Adding is completed so just log the message
            WriteMessage(message);
        }

        // for testing
        internal virtual void WriteMessage(LogData log)
        {
            //Console.WriteLine($"NetXXX:Message{message.Message},Level:{message.LevelString}")

            AddLog(log.LevelString, log.Message, null, log.Exception?.StackTrace);
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

            try
            {
                var res = NetHelper.PostWithJson(option.Url, JsonHelper.ToJsonString(jsonObj));
                OPResult opRes = JsonHelper.FromJsonString<OPResult>(res);
                if (opRes.State == 1)
                {
                    flag = true;
                }
                else
                {
                    //TODO
                    // Logger.Error("调用日志系统失败:" + res + ",extensionData=" + JsonConvert.SerializeObject(dicArgs));
                }
            }
            catch (Exception ex)
            {
                //TODO
                // Logger.Error("调用日志系统失败:" + ex.Message + ",extensionData=" + JsonConvert.SerializeObject(dicArgs), ex);
            }

            return flag;

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
                catch { }
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
