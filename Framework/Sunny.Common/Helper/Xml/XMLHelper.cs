using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Sunny.Common.Helper
{
    public class XMLHelper
    {
        private static XmlDocument xmlDoc = new XmlDocument();
        private static object locker = new object();

        /// <summary>
        /// 获取XML节点的属性值
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="xpath">Xpath路径</param>
        /// <param name="key">属性名称</param>

        /// <returns>属性值</returns>
        public static string GetXmlNodeAttribute(string filePath, string xpath, string key)
        {
            try
            {
                lock (locker)
                {
                    xmlDoc.Load(filePath);
                    XmlNode node = xmlDoc.SelectSingleNode(xpath);
                    string value = node.Attributes[key].Value;
                    return value;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取XML文件" + filePath + "的节点" + xpath + "属性" + key + "时出现异常:" + ex);
            }

        }
        /// <summary>
        /// 设置XML节点的属性值
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="xpath">Xpath路径</param>
        /// <param name="key">属性名称</param>
        /// <param name="value">要赋的值</param>

        /// <returns>true表示设置成功</returns>
        public static bool SetXmlNodeAttribute(string filePath, string xpath, string key, string value)
        {

            bool flag = false;
            try
            {
                lock (locker)
                {
                    xmlDoc.Load(filePath);
                    XmlNode node = xmlDoc.SelectSingleNode(xpath);
                    node.Attributes[key].Value = value;
                    xmlDoc.Save(filePath);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("设置XML文件" + filePath + "的节点" + xpath + "属性" + key + "时出现异常:" + ex);
            }
            return flag;
        }


        /// <summary>
        /// 获取xml节点值
        /// </summary>
        /// <param name="xmlFile">xml文件</param>
        /// <param name="xpath">xpath访问路径</param>

        /// <returns>节点值</returns>
        public static String GetXmlNodeValue(string xmlFile, string xpath)
        {

            try
            {
                lock (locker)
                {
                    xmlDoc.Load(xmlFile);
                    XmlNode node = xmlDoc.SelectSingleNode(xpath);
                    return node.InnerText;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取XML文件 " + xmlFile + " 的" + xpath + "节点内容时出现异常:" + ex);
            }
        }

        /// <summary>
        /// 设置xml节点值
        /// </summary>
        /// <param name="xmlFile">xml文件</param>
        /// <param name="xpath">xpath访问路径</param>
        /// <param name="value">要设置的值</param>

        /// <returns>true表示设置成功</returns>
        public static bool SetXmlNodeValue(string xmlFile, string xpath, string value)
        {

            bool flag = false;
            try
            {
                lock (locker)
                {
                    xmlDoc.Load(xmlFile);
                    XmlNode node = xmlDoc.SelectSingleNode(xpath);
                    node.InnerText = value;
                    xmlDoc.Save(xmlFile);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("设置XML文件 " + xmlFile + " 的 " + xpath + " 节点值时出现异常:" + ex);

            }
            return flag;
        }


        /// <summary>
        /// 获取xml节点集合的值
        /// </summary>
        /// <param name="xmlFile">xml文件</param>
        /// <param name="xpath">xpath访问路径</param>

        /// <returns>节点值</returns>
        public static List<string> GetXmlNodeCollectValue(string xmlFile, string xpath)
        {


            try
            {
                lock (locker)
                {
                    xmlDoc.Load(xmlFile);
                    XmlNodeList nodes = xmlDoc.SelectNodes(xpath);
                    List<string> list = new List<string>();
                    foreach (XmlNode item in nodes)
                    {
                        list.Add(item.InnerText);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取XML文件 " + xmlFile + " 的" + xpath + "节点集合时出现异常:" + ex);
            }
        }

        /// <summary>
        /// 获取xml节点集合的2个属性值,封装成Hashtable返回
        /// </summary>
        /// <param name="xmlFile">xml文件</param>
        /// <param name="xpath">xpath访问路径</param>
        /// <param name="keyAttrName">属性值作为Hashtable键的属性名称</param>
        /// <param name="valueAttrName">属性值作为Hashtable值的属性名称</param>

        /// <returns>节点值</returns>
        public static Hashtable GetHashtableFromXmlNodeCollectAttr(string xmlFile, string xpath, string keyAttrName, string valueAttrName)
        {

            try
            {
                lock (locker)
                {
                    xmlDoc.Load(xmlFile);
                    XmlNodeList nodes = xmlDoc.SelectNodes(xpath);
                    Hashtable ht = new Hashtable();
                    foreach (XmlNode item in nodes)
                    {
                        ht.Add(item.Attributes[keyAttrName].Value, item.Attributes[valueAttrName].Value);
                    }
                    return ht;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("读取XML文件 " + xmlFile + " 的" + xpath + "节点集合的" + keyAttrName + "," + valueAttrName + "属性时出现异常:" + ex);
            }
        }
    }
}
