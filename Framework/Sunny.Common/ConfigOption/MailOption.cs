namespace Sunny.Common.ConfigOption
{
    /// <summary>
    /// 邮件配置信息
    /// </summary>
    public class MailOption
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        public string EmailHost { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string EmailUserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string EmailPassword { get; set; }
        /// <summary>
        /// IP白名单列表,在列表中的IP发邮件前不执行检查事件
        /// </summary>
        public string[] IPWhiteList { get; set; }
         

    }
}
