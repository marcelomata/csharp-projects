using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


/*
 *  This name space implements the crawler to different purposes. 
 *
 */
namespace CrawlerTrabble
{
    /*
     * This abstract class implement some common functions used by the specific crawlers
     */
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


    /*
     * This class implements the crawler to get specific informations of restaurants
     *  
     * The first version of this class was implemented to extract the menus of the website bellow:
     * "http://www.basilico.net/"
     * 
     * It menu is organized as follows
     * Page (1 page per Menu. Menus to different events) -> Menu categories -> Menu itens -> Menu item descriptions
     * 
     */
    public class RestaurantCrawler : CrawlerTrabble
    {

        private ArrayList urlsMenu;
        private ArrayList categories;
        private String currentUrlMenu;
        private Dictionary<String, ArrayList> categoryItensMap;

        public RestaurantCrawler(String url) : base(url)
        {
            this.urlsMenu = new ArrayList();
            this.categories = new ArrayList();
            this.categoryItensMap = new Dictionary<String, ArrayList>();
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

        public String getUrlMenu()
        {
            return currentUrlMenu;
        }

        public void setCurrentUrlMenu(String urlMenu)
        {
            currentUrlMenu = urlMenu;
        }

        public void loadCategoriesMenu()
        {

        }

        public ArrayList getCategoriesMenu()
        {
            return categories;
        }

        public Dictionary<String, ArrayList> getCategoryItens() 
        {
            return categoryItensMap;
        }

        public ArrayList getCategoryItens(String category)
        {
            ArrayList itens = new ArrayList();
            categoryItensMap.TryGetValue(category, out itens);
            return itens;
        }
    }
}
