using System;
using System.Collections.Generic;
using System.Text;
using Jyi.Entity;
using System.Xml;

namespace Jyi.Config
{
    class BaseConfig
    {
        public static BaseConfigInfo GetBaseConfig()
        {
            BaseConfigInfo objBaseConfig = new BaseConfigInfo();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(MainForm.configFilePath);
            XmlNode xnBaseConfig = xmlDoc.SelectSingleNode("/JyiConfig");
            #region Proxy
            XmlNodeList xnlProxys = xnBaseConfig.SelectSingleNode("Proxy").ChildNodes;

            List<ProxyInfo> objProxyList = new List<ProxyInfo>();
            foreach (XmlNode xnf in xnlProxys)
            {
                ProxyInfo objProxy = new ProxyInfo();
                XmlElement xe = (XmlElement)xnf;
                objProxy.Name = xnf.InnerText;
                objProxy.Address = xnf.Attributes["Address"].Value;
                objProxy.Port = int.Parse(xnf.Attributes["Port"].Value);
                objProxyList.Add(objProxy);
            }
            objBaseConfig.ProxyList = objProxyList;
            #endregion

            return objBaseConfig;
        }
    }
}
