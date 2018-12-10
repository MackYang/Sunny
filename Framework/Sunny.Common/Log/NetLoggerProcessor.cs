using Sunny.Common.ConfigOption;
using Sunny.Common.Helper;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Sunny.Common.Log
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
                // stackInfo = stackInfo 如果是异常,消息里已经包含了堆栈了,不用再添加
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
                string offlineFilePath = $@"{option.OfflineLogPath}\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

                string data = $"Time:{DateTime.Now.ToNormalString()}\r\n\r\nOriginalLogInfo:{originalLogInfo}";
                if (offlineExInfo != null)
                {
                    data += $"\r\n\r\nOfflineExceptionInfo:{$"Message:{offlineExInfo.Message},Stack:{offlineExInfo.StackTrace}"}";
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
                catch (Exception ex) { WriteOffLineLog("AddLogQueue Err", ex); }
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
