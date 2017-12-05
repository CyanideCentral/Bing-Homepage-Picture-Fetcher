using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;

namespace BingPicFetcher {
    class Fetcher {
        static String urlBase = "https://www.bing.com/HPImageArchive.aspx?format=json&idx=0&n=1&mkt=en-US";
        static void Main(string[] args) {
            WebRequest request = WebRequest.Create(urlBase);
            WebResponse xmlResponse = request.GetResponse();
            Stream xmlStream = xmlResponse.GetResponseStream();
            StreamReader reader = new StreamReader(xmlStream);
            String xml = reader.ReadToEnd();
            //BingXmlParser;
        }
    }

    class BingXmlParser {
        string xml;
        string picUrl;
        static String resDefault = "_1366x768";
        static String resSpec = "_1920x1080";
        public BingXmlParser(string xmlContent) {
            xml = xmlContent;
            picUrl = "";
        }

        public string getPicUrl() {
            if (picUrl.Length != 0) return picUrl;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.DocumentElement;
            XmlNode urlNode = root.SelectSingleNode("/images/image/url");
            string urlstr = urlNode.InnerText;
            urlstr.Replace(resDefault, resSpec);
            picUrl = urlstr;
            return picUrl;
        }
    }
}
