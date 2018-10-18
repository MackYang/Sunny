using Microsoft.AspNetCore.Http;
using Sunny.Common.Enum;
using Sunny.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sunny.Common.Helper.Net
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
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <returns>失败返回null</returns>
        public static string GetClientIP(HttpContext httpContext)
        {

            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            string result = null;
            //try
            //{

            //    result = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //    //否则直接读取REMOTE_ADDR获取客户端IP地址
            //    if (string.IsNullOrWhiteSpace(result))
            //    {
            //        result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //    }
            //    else
            //    {
            //        result = result.ToString().Split(',')[0].Trim();
            //    }
            //    //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            //    if (string.IsNullOrWhiteSpace(result))
            //    {
            //        result = HttpContext.Current.Request.UserHostAddress;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("获取客户端IP地址时出现异常:" + ex);
            //}

            ////最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            //if (!string.IsNullOrWhiteSpace(result) && result.IsIPAddress())
            //{
            //    return result;
            //}
            return null;

        }


        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="smsInfo">短信实体</param>
        /// <returns></returns>
        private static void SendSMS(SMSInfo smsInfo)
        {
            //记得加上短信内容长度截取的代码,以免恶意用户发送超长短信导致不必费用.
            try
            {

            
                //if (smsInfo != null)
                //{
                //    smsInfo.SMSContent = smsInfo.SMSContent.AutoSubstring(0, 70);

                //    string url = Vars.SMSAPI;

                //    string gbkStr = smsInfo.SMSContent;

                //    url = url.Replace("$TOPHONE", smsInfo.ToPhone);
                //    url = url.Replace("$CONTENT", HttpUtility.UrlEncode(smsInfo.SMSContent, Encoding.GetEncoding("gbk")));

                //    string responseStr = RequestNew(url, null, Enums.RequestType.Get);

                //    if (!string.IsNullOrWhiteSpace(responseStr))
                //    {
                //        //<response><result>0</result></response>
                //        responseStr = responseStr.Replace("<response><result>", "");
                //        responseStr = responseStr.Replace("</result></response>", "");

                //        int flag = Utility.GetValidData(responseStr, -717);
                //        if (flag != 0)
                //        {
                //            Dictionary<int, string> dicReason = new Dictionary<int, string>();
                //            dicReason.Add(-99, "其它故障");
                //            dicReason.Add(5, "含有禁止发送的内容");
                //            dicReason.Add(-1, "用户名或密码不正确");
                //            dicReason.Add(-2, "余额不够");
                //            dicReason.Add(-3, "帐号没有注册");
                //            dicReason.Add(-4, "内容超长");
                //            dicReason.Add(-5, "账号路由为空");
                //            dicReason.Add(-6, "手机号码超过1000个（或手机号码非法或错误");
                //            dicReason.Add(-8, "扩展号超长");
                //            dicReason.Add(-12, "Key值要是32位长的英文，建议32个a");
                //            dicReason.Add(-13, "定时时间错误或者小于当前系统时间");
                //            dicReason.Add(-17, "手机号码为空");
                //            dicReason.Add(-18, "号码不是数字或者逗号不是英文逗号");
                //            dicReason.Add(-19, "短信内容为空");

                //            string info = "短信发送失败：";

                //            if (dicReason.ContainsKey(flag))
                //            {
                //                info = info + dicReason[flag];
                //            }
                //            else
                //            {
                //                info = info + flag + "(该代码没在错误类型中。)[" + responseStr + "]";
                //            }
                //            Utility.Logger.Error(info);
                //        }
                //    }
                //    else
                //    {
                //        Utility.Logger.Error("发送短信接口返回的响应字符串为空");
                //    }
                //}
                //if (OnRecordSMS != null)
                //{
                //    OnRecordSMS.Invoke(smsInfo);
                //}
            }
            catch (Exception ex)
            {
                // Utility.Logger.Error("系统发送短信时发生异常:" + ex);
            }

        }
        /// <summary>
        /// 发送邮件，为异步发送邮件而写的内部方法
        /// </summary>
        ///<param name="miData">邮件信息实体</param>
        /// <returns></returns>
        private static void SendEmail(MailInfo miData)
        {
            try
            {
                //if (miData != null)
                //{
                //    MailMessage mailMessage = new MailMessage();
                //    mailMessage.Priority = MailPriority.Normal;
                //    mailMessage.IsBodyHtml = true;
                //    mailMessage.From = new MailAddress(Vars.EmailID);
                //    mailMessage.To.Add(miData.ToMail);
                //    mailMessage.Subject = miData.Title;
                //    mailMessage.Body = miData.Content;
                //    SmtpClient smtp = new SmtpClient();
                //    smtp.Host = Vars.EmailHost;
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Credentials = new NetworkCredential(Vars.EmailID, Vars.EmailPassword);
                //    smtp.Send(mailMessage);

                //    if (OnRecordMail != null)
                //    {
                //        OnRecordMail.Invoke(miData);
                //    }
                //}

            }
            catch (Exception ex)
            {
                // Utility.Logger.Error("系统发送邮件时发生异常:" + ex);
            }
        }
        /// <summary>
        /// 异步发送邮件
        /// </summary>
        ///<param name="mi">邮件信息实体</param>

        public static void AsyncSendEmail(MailInfo mi)
        {
            //if (opRes != null && opRes.State == Enums.OPState.Success)
            //{
            //    opRes.State = Enums.OPState.Fail;
            //    opRes.Data = "邮件发送前未通过系统检查";

            //    if (string.IsNullOrWhiteSpace(mi.OperaterIP))
            //    {
            //        mi.OperaterIP = GetClientIP(null);
            //    }

            //    if (string.IsNullOrWhiteSpace(mi.OperaterID))
            //    {
            //        OPResult uiObj = Utility.GetCurrentUserInfo();
            //        if (uiObj.State == Enums.OPState.Success)
            //        {
            //            mi.OperaterID = ((UserInfo)uiObj.Data).UserID;
            //        }
            //    }


            //    if (Vars.IPWhiteList.Contains(mi.OperaterIP))//如果IP在白名单中,则不用检查
            //    {
            //        opRes.State = Enums.OPState.Success;
            //    }
            //    else//否则调用检查事件检查是能否发送
            //    {
            //        if (OnCheckMail != null)
            //        {
            //            OnCheckMail.Invoke(mi, opRes);
            //        }
            //    }
            //    if (opRes.State == Enums.OPState.Success)
            //    {
            //        mi.Title = mi.Title + "[" + Vars.SiteName + "]";
            //        string temp = mi.Content;
            //        mi.Content = Vars.EmailTemplate.Replace("$[MailContent]", temp).Replace("$[SiteName]", Vars.SiteName).Replace("$[SendTime]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).Replace("$[SiteUrl]", Vars.DoMain);
            //        ThreadPool.QueueUserWorkItem(new WaitCallback(x => { SendEmail(mi); }));
            //    }
            //    opRes.ClearData();
        }


        /// <summary>
        /// 异步发送短信
        /// </summary>
        ///<param name="smsInfo">短信信息实体</param>

        public static void AsyncSendSMS(SMSInfo smsInfo)
        {
            //            if (opRes != null && opRes.State == Enums.OPState.Success)
            //            {
            //                opRes.State = Enums.OPState.Fail;
            //                opRes.Data = "短信发送前未通过系统检查";
            //                if (smsInfo.ToPhone.IsPhoneNum() && !string.IsNullOrWhiteSpace(smsInfo.SMSContent))
            //                {
            //                    if (string.IsNullOrWhiteSpace(smsInfo.OperaterIP))
            //                    {
            //                        smsInfo.OperaterIP = GetClientIP(null);
            //                    }

            //                    if (string.IsNullOrWhiteSpace(smsInfo.OperaterID))
            //                    {
            //                        OPResult uiObj = Utility.GetCurrentUserInfo();
            //                        if (uiObj.State == Enums.OPState.Success)
            //                        {
            //                            smsInfo.OperaterID = ((UserInfo)uiObj.Data).UserID;
            //                        }
            //                    }

            //                    if (Vars.IPWhiteList.Contains(smsInfo.OperaterIP))//如果IP在白名单中,则不用检查
            //                    {
            //                        opRes.State = Enums.OPState.Success;
            //                    }
            //                    else//否则调用检查事件检查是能否发送
            //                    {
            //                        if (OnCheckSMS != null)
            //                        {
            //                            OnCheckSMS.Invoke(smsInfo, opRes);
            //                        }
            //                    }
            //                    if (opRes.State == Enums.OPState.Success)
            //                    {
            //                        smsInfo.SMSContent = smsInfo.SMSContent;
            //#if DEBUG
            //                        return;
            //#endif
            //                        ThreadPool.QueueUserWorkItem(new WaitCallback(x => { SendSMS(smsInfo); }));
            //                    }

            //                    opRes.ClearData();
            //                }
        }


        /// <summary>
        /// 查询IP信息
        /// </summary>
        /// <param name="ip">要查询的IP</param>
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <returns>IP信息实体</returns>
        public static IPInfo QueryIPInfoIP138(string ip, bool throwException = false)
        {

            return null;
            //            return ExceptionHelper.ExceptionRecord(() =>
            //            {
            //                try
            //                {

            //                    //Utility.Logger.Debug("开始查询IP:"+ip);
            //                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Vars.IPQueryURL + ip);
            //                    //request.Proxy = new WebProxy("127.0.0.1",8888);
            //                    request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B137 Safari/601.1";
            //                    WebResponse response = request.GetResponse();
            //                    Stream stream = response.GetResponseStream();
            //                    Encoding en = Encoding.GetEncoding("utf-8");
            //                    StreamReader sr = new StreamReader(stream, en);
            //                    string tmp = sr.ReadToEnd();
            //                    sr.Close();
            //                    sr.Dispose();
            //                    stream.Close();
            //                    stream.Dispose();
            //                    response.Close();
            //                    //Utility.Logger.Debug("查询IP的URL为:" + request.RequestUri);
            //                    //Utility.Logger.Debug("查询IP的Agent为:" + request.UserAgent);
            //                    //Utility.Logger.Debug("查询IP结果为:" + tmp);
            //                    if (!string.IsNullOrWhiteSpace(tmp))
            //                    {
            //                        //Regex r = new Regex("(?<=本站数据：).*?(?=</li>)");IP138不能查了,收费了
            //                        //Regex r = new Regex("(?<=所在地理位置：<code>).*?(?=</code>)");
            //                        Regex r = new Regex("(?<=所在地理位置：).*?(?=</p>)");

            //                        IPInfo ipInfo = new IPInfo();
            //                        ipInfo.IP = ip;
            //                        ipInfo.FullAddress = r.Match(tmp).ToString();
            //                        // Utility.Logger.Debug("IP解析结果为:" + ipInfo.FullAddress);
            //                        return ipInfo;
            //                    }
            //                    else
            //                    {
            //#if ! DEBUG
            //                        Utility.Logger.Error("查询IP信息为空");
            //#endif
            //                        return null;
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    Utility.Logger.Debug("查询IP出现异常:" + ex.Message);
            //                    throw new Exception("查询IP信息时出现异常:IP=" + ip + ",异常信息:" + ex);
            //                }
            //            }, opRes, throwException);
        }

        /// <summary>
        /// 查询IP信息
        /// </summary>
        /// <param name="ip">要查询的IP</param>
        /// <param name="throwException">出现异常时是否抛出</param>
        /// <returns>IP信息实体</returns>
        public static IPInfo QueryIPInfoBaiDu(string ip, bool throwException = false)
        {
            return null;
            //            return ExceptionHelper.ExceptionRecord(() =>
            //            {
            //                try
            //                {
            //                    WebRequest request = WebRequest.Create(Vars.IPQueryURL + ip);
            //                    WebResponse response = request.GetResponse();
            //                    Stream stream = response.GetResponseStream();
            //                    Encoding en = Encoding.GetEncoding("utf-8");
            //                    StreamReader sr = new StreamReader(stream, en);
            //                    string tmp = sr.ReadToEnd();
            //                    sr.Close();
            //                    sr.Dispose();
            //                    stream.Close();
            //                    stream.Dispose();
            //                    response.Close();
            //                    /*
            //                     {"address":"CN|吉林|长春|None|CERNET|1|None","content":{"address_detail":{"province":"吉林省","city":"长春市","district":"","street":"","street_number":"","city_code":53},"address":"吉林省长春市","point":{"y":"5419815.34","x":"13950002.65"}},"status":0}
            //                     */
            //                    JsonData data = tmp.ToJsonData();
            //                    if (data["status"].ToString() == "0")
            //                    {
            //                        IPInfo ipInfo = new IPInfo();
            //                        ipInfo.IP = ip;
            //                        ipInfo.Province = data["content"]["address_detail"]["province"].ToString();
            //                        ipInfo.City = data["content"]["address_detail"]["city"].ToString();
            //                        ipInfo.District = data["content"]["address_detail"]["district"].ToString();
            //                        ipInfo.Street = data["content"]["address_detail"]["street"].ToString();
            //                        ipInfo.StreetNum = data["content"]["address_detail"]["street_number"].ToString();
            //                        ipInfo.FullAddress = data["content"]["address"].ToString();
            //                        return ipInfo;
            //                    }
            //                    else
            //                    {
            //#if ! DEBUG
            //                        Utility.Logger.Error("查询IP信息时出出:IP=" + ip + ",信息:" + data["message"].ToString());
            //#endif
            //                        return null;
            //                    }
            //                }
            //                catch (Exception ex)
            //                {
            //                    throw new Exception("查询IP信息时出现异常:IP=" + ip + ",异常信息:" + ex);
            //                }
            //            }, opRes, throwException);
        }

        public static async Task<string> PostWithJson(string url, string data)
        {
            HttpContent content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await new HttpClient().PostAsync(url, content).Result.Content.ReadAsStringAsync();
        }

       
        #region 辅助类

        public class IPInfo
        {
            public string IP { get; set; }
            public string Province { get; set; }
            public string City { get; set; }
            /// <summary>
            /// 区县名称
            /// </summary>
            public string District { get; set; }
            /// <summary>
            /// 街道名称
            /// </summary>
            public string Street { get; set; }
            /// <summary>
            /// 门牌号
            /// </summary>
            public string StreetNum { get; set; }
            /// <summary>
            /// IP所在地址,就是将省,市..等拼起来
            /// </summary>
            public string FullAddress { get; set; }

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
}