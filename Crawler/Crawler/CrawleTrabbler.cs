using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CrawlerTrabble
{
    public abstract class CrawlerTrabble
    {

        private Uri uri;
        private String keyWord;

        public CrawlerTrabble(String url) 
        {
            this.uri = new Uri("http://www.carnivore.com.sg/");
            ISet<String> set = getNewLinks(url);
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

        public abstract ArrayList getDataItens(String url);

        public ISet<string> getNewLinks(string content)
        {
            Regex regexLink = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            ISet<string> newLinks = new HashSet<string>();
            foreach (var match in regexLink.Matches(content))
            {
                if (!newLinks.Contains(match.ToString()))
                    newLinks.Add(match.ToString());
            }

            return newLinks;
        }


    }

    public class RestaurantCrawler : CrawlerTrabble
    {

        public RestaurantCrawler(String url) : base(url)
        {
            
        }

        public override ArrayList getDataItens(String url)
        {
            ArrayList dataItens = new ArrayList();

            return dataItens;
        }

    }
}
