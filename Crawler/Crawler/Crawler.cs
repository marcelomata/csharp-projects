using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerTrabble
{
    public abstract class Crawler
    {

        private String url;
        private String keyWord;

        public Crawler(String url) 
        {
            this.url = url;
        }

        public ArrayList getUrls()
        {
            ArrayList urls = new ArrayList();



            return urls;
        }

        public void setKeyWord(String key)
        {
            this.keyWord = key;
        }

        public abstract ArrayList getData();


    }

    public class RestaurantCrawler : Crawler
    {

        public RestaurantCrawler(String url) : base(url)
        {
            
        }

        public ArrayList getDataItens(String url)
        {
            ArrayList dataItens = new ArrayList();

            return dataItens;
        }

    }
}
