using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrawlerTrabble;
using System.Collections;
//using MisterHex.WebCrawling; Can not be used in this version of Visual Studio. The package used (System.Reactive) is not supported by this version
//using System.Reactive.Linq;

namespace CrawlerTrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            //String url = "http://www.carnivore.com.sg/";
            String url = "http://www.basilico.net/";
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            crawler.setKeyWord("menu");
            crawler.loadUrlsMenus();
            ArrayList urlsMenu = crawler.getUrlsMenu();
            Console.WriteLine(urlsMenu.Count);

            //using MisterHex.WebCrawling;
            //IObservable<Uri> observable1 = crawler.Crawl(new Uri("http://www.carnivore.com.sg/"));
            //observable1.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));
            //Console.ReadLine();
            
        }
    }
}
