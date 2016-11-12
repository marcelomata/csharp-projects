using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrawlerTrabble;
//using MisterHex.WebCrawling;
//using System.Reactive.Linq;

namespace CrawlerTrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            CrawlerTrabble crawler = new RestaurantCrawler("http://www.carnivore.com.sg/");
            ISet<String> set = crawler.getNewLinks("http://www.carnivore.com.sg/");
            
            
            //IObservable<Uri> observable1 = crawler.Crawl(new Uri("http://www.carnivore.com.sg/"));

            //observable1.Subscribe(onNext: Console.WriteLine, onCompleted: () => Console.WriteLine("Crawling completed"));

            //Console.ReadLine();
            
        }
    }
}
