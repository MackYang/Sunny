using Microsoft.AspNetCore.Http;
using Sunny.Common.ConfigOption;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Sunny.Common.Helper
{
    /// <summary>
    /// 网络相关的辅助类
    /// </summary>
    public class NetHelper
    {
        #region 发送前检查事件
        /// <summary>
        /// 可定义符合此委托的方法传递给OnCheckMail事件,在邮件发送前,被传递的方法将被调用,以检查是否能发送该邮件,仅当返回结果为true时才发送
        /// </summary>
        /// <param name="mailInfo">邮件信息实体</param>
        public delegate bool CheckMail(MailInfo mailInfo);
        /// <summary>
        /// 检查能否发送邮件的事件,如果订阅,将在邮件发送前被调用
        /// </summary>
        static public event CheckMail OnCheckMail;
        /// <summary>
        /// 可定义符合此委托的方法传递给OnCheckSMS事件,在邮件发送前,被传递的方法将被调用,以检查是否能发送该邮件,仅当返回结果为true时才发送
        /// </summary>
        /// <param name="smsInfo">短信信息实体</param>
        public delegate bool CheckSMS(SMSInfo smsInfo);
        /// <summary>
        /// 检查能否发送短信的事件,如果订阅,将在短信发前后被调用
        /// </summary>
        static public event CheckSMS OnCheckSMS;
        #endregion

        #region 发送后记录事件
        /// <summary>
        /// 可定义符合此委托的方法传递给OnRecordMail事件,当邮件发送后,所定义的方法将被调用以记录所发送的邮件
        /// </summary>
        /// <param name="mailInfo">邮件信息实体</param>
        public delegate void RecordMail(MailInfo mailInfo);
        /// <summary>
        /// 记录邮件的事件,如果订阅,将在邮件发送后被调用
        /// </summary>
        static public event RecordMail OnRecordMail;
        /// <summary>
        /// 可定义符合此委托的方法传递给OnRecordSMS事件,当短信发送后,所定义的方法将被调用以记录所发送的短信
        /// </summary>
        /// <param name="smsInfo">短信信息实体</param>
        public delegate void RecordSMS(SMSInfo smsInfo);
        /// <summary>
        /// 记录短信的事件,如果订阅,将在短信发送后被调用
        /// </summary>
        static public event RecordSMS OnRecordSMS;

        #endregion

        /// <summary>
        /// 获取IP的方法
        /// </summary>

        /// <returns>失败返回null</returns>
        public static string GetClientIP(HttpContext httpContext)
        {
            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            string result = null;
            try
            {

                result = httpContext.Request.Headers["HTTP_X_FORWARDED_FOR"];

                //否则直接读取REMOTE_ADDR获取客户端IP地址
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = httpContext.Connection.RemoteIpAddress.ToString();
                }
                else
                {
                    result = result.ToString().Split(',')[0].Trim();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("获取客户端IP地址时出现异常:" + ex);
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrWhiteSpace(result) && result.IsIPAddress())
            {
                return result;
            }
            return null;

        }


        ///// <summary>
        ///// 发送短信
        ///// </summary>
        ///// <param name="smsInfo">短信实体</param>
        ///// <returns></returns>
        //private static async void SendSMS(SMSInfo smsInfo, SmsOption smsOption)
        //{
        //    //记得加上短信内容长度截取的代码,以免恶意用户发送超长短信导致不必费用.
        //    try
        //    {


        //        if (smsInfo != null)
        //        {
        //            smsInfo.SMSContent = smsInfo.SMSContent.AutoSubstring(0, 70);

        //            string url = smsOption.ApiUrl;

        //            string gbkStr = smsInfo.SMSContent;

        //            url = url.Replace("$TOPHONE", smsInfo.ToPhone);
        //            url = url.Replace("$CONTENT", HttpUtility.UrlEncode(smsInfo.SMSContent, Encoding.UTF8));
        //            string responseStr = await Get(url);

        //            if (!string.IsNullOrWhiteSpace(responseStr))
        //            {
        //                //<response><result>0</result></response>
        //                responseStr = responseStr.Replace("<response><result>", "");
        //                responseStr = responseStr.Replace("</result></response>", "");

        //                int flag = responseStr.ConvertTo(-717);
        //                if (flag != 0)
        //                {
        //                    Dictionary<int, string> dicReason = new Dictionary<int, string>();
        //                    dicReason.Add(-99, "其它故障");
        //                    dicReason.Add(5, "含有禁止发送的内容");
        //                    dicReason.Add(-1, "用户名或密码不正确");
        //                    dicReason.Add(-2, "余额不够");
        //                    dicReason.Add(-3, "帐号没有注册");
        //                    dicReason.Add(-4, "内容超长");
        //                    dicReason.Add(-5, "账号路由为空");
        //                    dicReason.Add(-6, "手机号码超过1000个（或手机号码非法或错误");
        //                    dicReason.Add(-8, "扩展号超长");
        //                    dicReason.Add(-12, "Key值要是32位长的英文，建议32个a");
        //                    dicReason.Add(-13, "定时时间错误或者小于当前系统时间");
        //                    dicReason.Add(-17, "手机号码为空");
        //                    dicReason.Add(-18, "号码不是数字或者逗号不是英文逗号");
        //                    dicReason.Add(-19, "短信内容为空");

        //                    string info = "短信发送失败：";

        //                    if (dicReason.ContainsKey(flag))
        //                    {
        //                        info = info + dicReason[flag];
        //                    }
        //                    else
        //                    {
        //                        info = info + flag + "(该代码没在错误类型中。)[" + responseStr + "]";
        //                    }
        //                    throw new Exception(info);
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("发送短信接口返回的响应字符串为空");
        //            }
        //        }
        //        if (OnRecordSMS != null)
        //        {
        //            OnRecordSMS.Invoke(smsInfo);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("系统发送短信时发生异常:" + ex);
        //    }

        //}
        /// <summary>
        /// 发送邮件，为异步发送邮件而写的内部方法
        /// </summary>
        ///<param name="miData">邮件信息实体</param>
        /// <returns></returns>
        private static void SendEmail(MailInfo miData, MailOption mailOption)
        {

            try
            {
                if (miData != null)
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.Priority = MailPriority.Normal;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.From = new MailAddress(mailOption.EmailUserName);
                    mailMessage.To.Add(miData.ToMail);
                    mailMessage.Subject = miData.Title;
                    mailMessage.Body = miData.Content;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = mailOption.EmailHost;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(mailOption.EmailUserName, mailOption.EmailPassword);
                    smtp.Send(mailMessage);

                    if (OnRecordMail != null)
                    {
                        OnRecordMail.Invoke(miData);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("系统发送邮件时发生异常:" + ex);
            }
        }
        /// <summary>
        /// 异步发送邮件
        /// </summary>
        ///<param name="mi">邮件信息实体</param>

        public static void AsyncSendEmail(MailInfo mi, MailOption mailOption)
        {
            bool canSend = true;

            if (!mailOption.IPWhiteList.Contains(mi.OperaterIP))//如果不IP在白名单中,则用检查
            {
                if (OnCheckMail != null)
                {
                    canSend = OnCheckMail.Invoke(mi);
                }
            }
            if (canSend)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(x => { SendEmail(mi, mailOption); }));
            }

        }


        ///// <summary>
        ///// 异步发送短信
        ///// </summary>
        /////<param name="smsInfo">短信信息实体</param>

        //public static void AsyncSendSMS(SMSInfo smsInfo, SmsOption smsOption)
        //{
        //    if (smsInfo.ToPhone.IsPhoneNum() && !string.IsNullOrWhiteSpace(smsInfo.SMSContent))
        //    {
        //        bool canSend = true;

        //        if (!smsOption.IPWhiteList.Contains(smsInfo.OperaterIP))//如果IP不在白名单中,则检查
        //        {

        //            if (OnCheckSMS != null)
        //            {
        //                canSend = OnCheckSMS.Invoke(smsInfo);
        //            }
        //        }


        //        if (canSend)
        //        {

        //            ThreadPool.QueueUserWorkItem(new WaitCallback(x => { SendSMS(smsInfo, smsOption); }));
        //        }



        //    }
        //}


        /// <summary>
        /// 查询IP信息
        /// </summary>
        /// <param name="ip">要查询的IP</param>
        /// <returns>IP信息实体</returns>
        public static IPInfo QueryIpInfo(string ip, IpInfoQueryOption option)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(option.ApiUrl + ip);
                request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B137 Safari/601.1";
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding en = Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(stream, en);
                string tmp = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                stream.Close();
                stream.Dispose();
                response.Close();

                if (!string.IsNullOrWhiteSpace(tmp))
                {
                    Regex r = new Regex("(?<=所在地理位置：<code>).*?(?=</code></p>)");

                    IPInfo ipInfo = new IPInfo();
                    ipInfo.IP = ip;
                    ipInfo.Info = r.Match(tmp).ToString();
                    return ipInfo;
                }
                else
                {
                    throw new Exception("查询到的IP信息为空");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("查询IP信息时出现异常:IP=" + ip + ",异常信息:" + ex);
            }

        }


        public static async Task<string> PostWithJson(string url, string data)
        {
            HttpContent content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await new HttpClient().PostAsync(url, content).Result.Content.ReadAsStringAsync();
        }

        public static async Task<string> Get(string url)
        {
            return await new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync();
        }


    }

    #region 辅助类

    public class IPInfo
    {
        /// <summary>
        /// ip地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 查询到的IP信息
        /// </summary>
        public string Info { get; set; }

    }

    /// <summary>
    /// 邮件信息类,供异步发送邮件时对邮件信息的封装,使其满足线程参数的要求
    /// </summary>
    public class MailInfo
    {
        /// <summary>
        /// 邮件接收者
        /// </summary>
        public string ToMail { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 操作者ID
        /// </summary>
        public string OperaterID { get; set; }
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperaterIP { get; set; }
    }
    /// <summary>
    /// 短信信息类,代异步发送短信时对短信数据的封装,使其满足线程参数的要求
    /// </summary>
    public class SMSInfo
    {
        /// <summary>
        /// 短信内容
        /// </summary>
        public string SMSContent { get; set; }
        /// <summary>
        /// 接收号码
        /// </summary>
        public string ToPhone { get; set; }

        /// <summary>
        /// 操作者ID
        /// </summary>
        public string OperaterID { get; set; }
        /// <summary>
        /// 操作者IP
        /// </summary>
        public string OperaterIP { get; set; }
    }

    #endregion

}