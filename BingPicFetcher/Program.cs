using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace BingPicFetcher {
    class Fetcher {
        static String urlBase = "https://www.bing.com/HPImageArchive.aspx?format=json&idx=0&n=1&mkt=en-US";
        static String resDefault = "_1366x768";
        static String resSpec = "_1920x1080";
        static void Main(string[] args) {
            WebRequest request = WebRequest.Create(urlBase);

        }
    }
}
