using System.IO;



namespace Sunny.Common.Helper
{

    public class FileHelper
    {
        /// <summary>  
        /// 写入文件  
        /// </summary>  
        /// <param name="filePath">文件名</param>  
        /// <param name="content">文件内容</param>  
        /// <param name="isAppend">默认是追加,如果要替换原内容传flase</param>
        public static void WriteFile(string filePath, string content, bool isAppend = true)
        {
            var dirInfo = Directory.GetParent(filePath).ToString();
            CreatePathIfNotExists(dirInfo);
            if (isAppend)
            {
                System.IO.File.AppendAllText(filePath, content);
            }
            else
            {
                System.IO.File.WriteAllText(filePath, content);
            }


        }

        /// <summary>  
        /// 读取文件  
        /// </summary>  
        /// <param name="filePath">文件路径</param>  
        /// <returns></returns>  
        public static string ReadFile(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);

        }



        ///// <summary>
        ///// 自动清除24小时以前创建的缓存文件,每一小时运行一次
        ///// </summary>
        //public static void AutoClearCacheFile()
        //{
        //    string cachePath = HttpContext.Current.Server.MapPath(Vars.CacheFilePath);
        //    TimeSpan timeSpan = new TimeSpan(1, 0, 0);//1小时 
        //    while (true)
        //    {
        //        try
        //        {
        //            List<string> fileList = GetFileList(cachePath);
        //            if (fileList != null && fileList.Count > 0)
        //            {
        //                foreach (string item in fileList)
        //                {
        //                    DateTime cacheTime = File.GetCreationTime(item);
        //                    if (DateTime.Now.Subtract(new TimeSpan(24, 0, 0)) > cacheTime)
        //                    {
        //                        File.Delete(item);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Utility.Logger.Error("删除缓存文件时发生异常:" + ex);
        //        }

        //        Thread.Sleep(timeSpan);
        //    }
        //}

        /// <summary>
        /// 创建目录(如果目录不存在)
        /// </summary>
        /// <param name="path">要创建的目录</param>
        public static void CreatePathIfNotExists(string path)
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


        }
    }
}
