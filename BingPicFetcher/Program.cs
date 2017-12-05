using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;

namespace BingPicFetcher {
    class PicFetcher {
        const string archiveUrl = "https://www.bing.com/HPImageArchive.aspx?format=json&idx=0&n=1&mkt=en-US";
        const string urlBase = "https://www.bing.com";
        const string fileNameBase = "C:/Users/greatlyr/Pictures/壁纸/BingWallpaper-";
        const string fileNameSuffix = ".jpg";
        static String resDefault = "_1366x768";
        static String resSpec = "_1920x1080";

        static void Main(string[] args) {
            int idx = 0, n = 5;
            if (args.Length == 2) {
                idx = Convert.ToInt32(args[0]);
                n = Convert.ToInt32(args[1]);
            }
            PicFetcher picFetcher = new PicFetcher();
            picFetcher.fetchFromArchive(idx, n);
        }

        void fetchFromArchive(int idx = 0, int n = 1) {
            string modifiedUrl = archiveUrl.Replace("idx=0&n=1", "idx=" + idx + "&n=" + n);
            string xml = WebFetcher.fetchTextFrom(modifiedUrl);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNode root = doc.DocumentElement;
            XmlNodeList imgNodes = root.SelectNodes("/images/image");

            WebClient client = new WebClient();
            foreach (XmlNode imgNode in imgNodes) {
                string urlstr = imgNode.SelectSingleNode("url").InnerText;
                urlstr = urlBase + urlstr.Replace(resDefault, resSpec);
                string date = imgNode.SelectSingleNode("enddate").InnerText;

                client.DownloadFile(urlstr, fileNameBase + formatDate(date) + fileNameSuffix);
            }

            Console.WriteLine("" + imgNodes.Count + " wallpapers downloaded successfully.");
        }

        string formatDate(string date) {
            return date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
        }
    }

    class WebFetcher {
        private WebFetcher() { }

        public static string fetchTextFrom(string url) {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream textStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(textStream);
            String text = reader.ReadToEnd();
            return text;
        }
    }
}
