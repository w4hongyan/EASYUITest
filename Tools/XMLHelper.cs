using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tools
{
    public class XMLHelper
    {
        public static void ReadXML(string filePath,string rootNodePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode root = doc.SelectSingleNode(rootNodePath);
            XmlNodeList nodeList = root.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                XmlNodeList singleNodeList = node.ChildNodes;
                foreach (XmlNode item in singleNodeList)
                {
                   //TODO
                }
            }
        }
    }
}
