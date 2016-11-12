using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace CrawlerTrabble
{
    public abstract class CrawlerTrabble
    {

        private Uri uri;
        private String url;
        private String keyWord;

        public CrawlerTrabble(String url) 
        {
            this.uri = new Uri(url);
            this.url = url;
        }

        public ArrayList getUrlsKeyWord()
        {
            ArrayList urls = new ArrayList();
            ISet<String> set = getLinksReference(url);
            ISet<String> urlsMenu;
            foreach (String urlSet in set)
            {
                urlsMenu = getSetStringMatching(urlSet, "menu");
                urls.Add(urlsMenu);
            }
            return urls;
        }

        public void setKeyWord(String key)
        {
            this.keyWord = key;
        }

        public abstract ArrayList getDataItens(String url);

        String loadHtmlAsString(String url)
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString(url);
        }

        ISet<String> getLinksReference(String url)
        {
            String html = loadHtmlAsString(url);
            ISet<String> linksReference = getSetStringMatching(html, "(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");
            return linksReference;
        }

        ISet<String> getSetStringMatching(String inString, String word) 
        {
            String html = loadHtmlAsString(url);
            ISet<String> newLinks = new HashSet<String>();

            Regex regexLink = new Regex(word);

            foreach (var match in regexLink.Matches(html))
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
