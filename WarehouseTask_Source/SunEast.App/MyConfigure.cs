namespace SunEast.App
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Xml;

    internal class MyConfigure
    {
        public static bool ReadMyArributeValue(string sFile, string sKeyPath, string sArributeName, out string sValue, out string sErr)
        {
            bool flag = false;
            bool flag2 = false;
            XmlNode newChild = null;
            sErr = "";
            sValue = "";
            if (File.Exists(sFile))
            {
                XmlAttribute attribute;
                Exception exception2;
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(sFile);
                }
                catch (Exception exception)
                {
                    flag = false;
                    sErr = exception.Message;
                    return flag;
                }
                XmlNode node2 = null;
                node2 = document.SelectSingleNode(sKeyPath);
                if (node2 != null)
                {
                    attribute = null;
                    attribute = node2.Attributes[sArributeName];
                    if (attribute != null)
                    {
                        sValue = attribute.Value;
                    }
                    else
                    {
                        flag2 = true;
                        attribute = document.CreateAttribute(sArributeName);
                        attribute.Value = "";
                        node2.Attributes.Append(attribute);
                    }
                }
                else
                {
                    attribute = null;
                    try
                    {
                        string xpath = "";
                        string[] strArray = sKeyPath.Split(new char[] { '/' });
                        if ((strArray != null) && (strArray.Length > 0))
                        {
                            xpath = strArray[0];
                            newChild = document.SelectSingleNode(xpath);
                            if (newChild == null)
                            {
                                newChild = document.CreateNode(XmlNodeType.Element, xpath, "");
                                document.AppendChild(newChild);
                            }
                            if (strArray.Length > 1)
                            {
                                for (int i = 1; i < strArray.Length; i++)
                                {
                                    node2 = newChild.SelectSingleNode(strArray[i]);
                                    if (node2 == null)
                                    {
                                        node2 = document.CreateNode(XmlNodeType.Element, strArray[i], "");
                                        newChild.AppendChild(node2);
                                    }
                                    newChild = node2;
                                }
                            }
                            node2 = newChild;
                        }
                    }
                    catch (Exception exception3)
                    {
                        exception2 = exception3;
                        sErr = exception2.Message;
                        return false;
                    }
                    flag2 = true;
                    attribute = document.CreateAttribute(sArributeName);
                    attribute.Value = "";
                    node2.Attributes.Append(attribute);
                }
                flag = true;
                if (flag2)
                {
                    try
                    {
                        document.Save(sFile);
                    }
                    catch (Exception exception4)
                    {
                        exception2 = exception4;
                        sErr = exception2.Message;
                        flag = false;
                    }
                }
                return flag;
            }
            sErr = "文件：" + sFile + " 不存在！";
            return false;
        }

        public static bool WriteMyArributeValue(string sFile, string sKeyPath, string sArributeName, string sValue, out string sErr)
        {
            bool flag = false;
            bool flag2 = false;
            sErr = "";
            flag = File.Exists(sFile);
            if (flag)
            {
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(sFile);
                }
                catch (Exception exception)
                {
                    flag = false;
                    sErr = exception.Message;
                    return flag;
                }
                XmlNode node = null;
                node = document.SelectSingleNode(sKeyPath);
                if (node != null)
                {
                    XmlAttribute attribute = null;
                    attribute = node.Attributes[sArributeName];
                    if (attribute != null)
                    {
                        attribute.Value = sValue;
                        flag2 = true;
                    }
                    else
                    {
                        attribute = document.CreateAttribute(sArributeName);
                        attribute.Value = sValue;
                        node.Attributes.Append(attribute);
                        flag2 = true;
                    }
                }
                if (flag2)
                {
                    try
                    {
                        document.Save(sFile);
                        flag = true;
                    }
                    catch (Exception exception2)
                    {
                        sErr = exception2.Message;
                        flag = false;
                    }
                }
                return flag;
            }
            sErr = "文件：" + sFile + " 不存在！";
            return false;
        }
    }
}

