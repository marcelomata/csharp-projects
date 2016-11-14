using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrawlerTrabble;
using System.Collections;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Net;
//using MisterHex.WebCrawling; Can not be used in this version of Visual Studio. The package used (System.Reactive) is not supported by this version
//using System.Reactive.Linq;

namespace CrawlerTrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            //String url = "http://www.carnivore.com.sg/";
            String url = "http://www.basilico.net/";
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            crawler.setKeyWord("menu");
            crawler.loadUrlsMenus();
            ArrayList urlsMenu = crawler.getUrlsMenu();
            Console.WriteLine(urlsMenu.Count);
             */

            /*
            String url = "http://www.basilico.net/lunch_menu.html";
            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();           
            HtmlAgilityPack.HtmlDocument doc = web.Load(url);
            var findclasses = doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("menu_category"));

            foreach (HtmlAgilityPack.HtmlNode s in findclasses)
            {
                Console.WriteLine(s.InnerText.Trim());
            }
            Console.ReadLine();          
            */


            /*
            String url = "http://www.basilico.net/lunch_menu.html";
            WebClient webClient = new WebClient();
            String inString = webClient.DownloadString(url);
                
            //string pattern = "(<div.*>)(.*)(<\\/div>)";
            //<div class="menu_category">Antipasti</div>
            string pattern = "(<div class=\"menu_category\">)(.*)([\\s]*)(.*)(<\\/div>)";
            MatchCollection matches = Regex.Matches(inString, pattern);
            Console.WriteLine("Matches found: {0}", matches.Count);

            if (matches.Count > 0)
                foreach (Match m in matches)
                    Console.WriteLine("Inner DIV: {0}", m.Groups[2]);

            Console.ReadLine();
            */

            /*
            String url = "http://www.basilico.net/lunch_menu.html";
            String word = "(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))";
            WebClient webClient = new WebClient();
            String inString = webClient.DownloadString(url);
            ISet<String> newLinks = new HashSet<String>();
            Regex regex = new Regex(word);
            foreach (var match in regex.Matches(inString))
            {
                if (!newLinks.Contains(match.ToString()))
                    newLinks.Add(match.ToString());
            }
            */

            //using MisterHex.WebCrawling;
            //IObservable<Uri> observable1 = crawler.Crawl(new Uri("http://www.carnivore.com.sg/"));
            //observable1.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));
            //Console.ReadLine();
            
        }
    }
}
