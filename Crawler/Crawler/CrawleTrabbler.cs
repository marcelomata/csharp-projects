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

        protected Uri uri;
        protected String url;
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
            String newUrl;
            String urlCheck;
            foreach (String urlSet in set)
            {
                if (urlSet.Contains(keyWord)) 
                {
                    newUrl = urlSet;
                    urlCheck = url.Replace("www", "").Replace("https://.", "").Replace("http://.", "");
                    if (!newUrl.Contains(urlCheck))
                    {
                        newUrl = url + "/" + urlSet;
                    }
                    urls.Add(newUrl);
                }
            }
            return urls;
        }

        public void setKeyWord(String key)
        {
            this.keyWord = key;
        }

        public abstract ArrayList getDataItens(String url);

        protected String loadHtmlAsString(String url)
        {
            WebClient webClient = new WebClient();
            return webClient.DownloadString(url);
        }

        protected ISet<String> getLinksReference(String url)
        {
            String html = loadHtmlAsString(url);
            ISet<String> linksReference = getStringMatching(html, "(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");
            return linksReference;
        }

        protected ISet<String> getStringMatching(String inString, String word) 
        {
            ISet<String> newLinks = new HashSet<String>();

            Regex regex = new Regex(word);

            foreach (var match in regex.Matches(inString))
            {
                if (!newLinks.Contains(match.ToString()))
                    newLinks.Add(match.ToString());
            }

            return newLinks;
        }

    }

    public class RestaurantCrawler : CrawlerTrabble
    {

        private ArrayList urlsMenu;

        public RestaurantCrawler(String url) : base(url)
        {
            
        }

        public override ArrayList getDataItens(String url)
        {
            ArrayList dataItens = new ArrayList();

            return dataItens;
        }

        public void loadUrlsMenus()
        {
            setKeyWord("menu");
            urlsMenu = getUrlsKeyWord();
        }

        public ArrayList getUrlsMenu()
        {
            return urlsMenu;
        }

    }
}
