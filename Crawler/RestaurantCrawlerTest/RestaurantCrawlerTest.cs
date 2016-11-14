using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using CrawlerTrabble;
using System;

namespace UnitTestProject1
{
    /*
     * Test if the RestarauntCrawler is working properly.
     * In case of the website change these tests are going to figure about it
     * 
     * The website used in this test is "http://www.basilico.net/"
     * 
     * It menu is organized as follows
     * Page (1 page per Menu. Menus to different events) -> Menu categories -> Menu itens -> Menu item descriptions
     * 
     */
    [TestClass]
    public class RestaurantCrawlerTest
    {
        private String url;
        private RestaurantCrawler crawler;

        /*
         * This method initialize the general variables used in all tests case
         */
        [TestInitialize()]
        public void Initialize() 
        { 
            //this.url = "http://www.carnivore.com.sg/"; //403 error to access this url. Need check why.

            //The structure of the menu of this restaurent website is
            //Page (manu name) -> Menu categories -> Menu itens -> Menu item descriptions
            this.url = "http://www.basilico.net/";
            crawler = new RestaurantCrawler(url);
        }

        /*
         * Check if the number of pages with menu informations are like the expected
         */
        [TestMethod]
        public void TestCheckNumUrlMenu()
        {
            ArrayList urlsMenu = getUrlsMenu(crawler);
            ArrayList urlsMenuExpected = geExpectedUrlsMenu();
            //http://www.basilico.net/lunch_menu.html
            //http://www.basilico.net/dinner_menu.html
            //http://www.basilico.net/catering_menu.html
            //http://www.basilico.net/private_events_menu.html
            //http://www.basilico.net/funeral_luncheon.html  the simple crawler will not take this menu for now
            //http://basilico.net/fathers_day_menu.html image, need OCR
            //http://basilico.net/valentines_day_menu.html image, need OCR
            Assert.AreEqual(urlsMenuExpected.Count, urlsMenu.Count, 0, "Number of urls does not match.");
        }

        /*
         * Check the urls that have manu information
         */
        [TestMethod]
        public void TestCheckUrlMenu()
        {
            ArrayList urlsMenu = getUrlsMenu(crawler);
            ArrayList urlsMenuExpected = geExpectedUrlsMenu();
            
            Assert.AreEqual(urlsMenuExpected.Count, urlsMenu.Count, 0, "Number of urls does not match.");

            String foundUrl;
            for (int i = 0; i < urlsMenu.Count; i++)
            {
                foundUrl = ((String)urlsMenu[i]).Replace("///", "/").Replace("//", "/").Replace("http:", "http:/");
                Assert.AreEqual((String)urlsMenuExpected[i], foundUrl, true, "Found the url " + urlsMenu[i] + " instead the expected " + urlsMenuExpected[i]);
            }
        }

        [TestMethod]
        public void TestNumLunchMenuCategories()
        {
            ArrayList urlsMenu = getUrlsMenu(crawler);
            String urlMenu = getUrlLunchMenu(urlsMenu);
            crawler.setCurrentUrlMenu(urlMenu);
            crawler.loadCategoriesMenu();
            ArrayList categoriesMenu = crawler.getCategoriesMenu();
            ArrayList expectedCategories = getExpectedCategoriesLunchMenu();

            Assert.AreEqual(expectedCategories.Count, categoriesMenu.Count, 0, "Name of the category different of the expected.");        
        }

        [TestMethod]
        public void TestCategoryNameLunchMenu()
        {
            ArrayList urlsMenu = getUrlsMenu(crawler);
            String urlMenu = getUrlLunchMenu(urlsMenu);
            crawler.setCurrentUrlMenu(urlMenu);
            crawler.loadCategoriesMenu();
            ArrayList categoriesMenu = crawler.getCategoriesMenu();
            ArrayList expectedCategories = getExpectedCategoriesLunchMenu();

            Assert.AreEqual(expectedCategories.Count, categoriesMenu.Count, 0, "Name of the category different of the expected.");

            for (int i = 0; i < urlsMenu.Count; i++)
            {
                Assert.AreEqual((String)expectedCategories[i], (String)categoriesMenu[i], true, "Found the category " + categoriesMenu[i] + " instead the expected " + expectedCategories[i]);
            }
        }

        [TestMethod]
        public void TestGetAntipastiCategoryItens()
        {
            ArrayList urlsMenu = getUrlsMenu(crawler);
            String category = getAntipastiCategory(urlsMenu);
            ArrayList itens = crawler.getCategoryItens(category);
            ArrayList expectedItens = getExpectedAntipastiItens();

            Assert.AreEqual(expectedItens.Count, itens.Count, 0, "Number of itens to category Antipasti doesn't match.");

            for (int i = 0; i < urlsMenu.Count; i++)
            {
                Assert.AreEqual((String)expectedItens[i], (String)itens[i], true, "Found the item " + itens[i] + " instead the expected " + expectedItens[i]);
            }
        }

        [TestMethod]
        public void TestGetMenuDescriptionBeef()
        {
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            ArrayList urlsMenu = getUrlsMenu(crawler);
            int expectedNumUrls = 6;
            Assert.AreEqual(expectedNumUrls, urlsMenu.Count, 0, "Number of urls does not match.");

            //String expectedDescription = "Boneless Leg of Lamb";
            String description;
            foreach (String urlMenu in urlsMenu)
            {
                //start index in 0
                description = getDescriptionItem(1, crawler, urlMenu);
            }
        }

        private ArrayList getExpectedCategoriesLunchMenu()
        {
            ArrayList expectedData = new ArrayList();
            expectedData.Add("Antipasti");
            expectedData.Add("Minestre");
            expectedData.Add("Insalate");
            expectedData.Add("Sandwiches and Wraps");
            expectedData.Add("Pasta and Risotto");
            expectedData.Add("Entrees");
            expectedData.Add("Basilico House Specials");

            return expectedData;
        }

        private ArrayList getExpectedAntipastiItens()
        {
            ArrayList expectedData = new ArrayList();
            
            expectedData.Add("Antipasto Italiano");
            expectedData.Add("Baked Clams (half dozen)");
            expectedData.Add("Zucchini Fritti");
            expectedData.Add("Carciofi Ripieni");

            expectedData.Add("Prosciutto con Melon");
            expectedData.Add("Bruschetta (toasted bread)");
            expectedData.Add("Sausage & Escarole");

            expectedData.Add("Calamari Fritti or Genovese");
            expectedData.Add("Cozze alla Marinara");
            expectedData.Add("Shrimp Cocktail");

            return expectedData;
        }

        private ArrayList getUrlsMenu(RestaurantCrawler crawler)
        {
            crawler.loadUrlsMenus();
            ArrayList urlsMenu = crawler.getUrlsMenu();
            return urlsMenu;
        }

        private String getDescriptionItem(int index, RestaurantCrawler crawler, String url)
        {
            String description = "";

            ArrayList data = crawler.getDataItens(url);
            int expectedNumItens = 8;
            Assert.AreEqual(expectedNumItens, data.Count, 0, "Number of itens in the menu does not match.");

            return description;
        }

        private String getUrlLunchMenu(ArrayList urls)
        {
            String urlResult = null;
            foreach (String urlMenu in urls)
            {
                if (urlMenu.Contains("lunch"))
                {
                    urlResult = urlMenu;
                }
            }

            return urlResult;
        }

        private ArrayList geExpectedUrlsMenu()
        {
            ArrayList expectedUrlMenu = new ArrayList();

            expectedUrlMenu.Add("http://www.basilico.net/lunch_menu.html");
            expectedUrlMenu.Add("http://www.basilico.net/dinner_menu.html");
            expectedUrlMenu.Add("http://www.basilico.net/private_events_menu.html");
            expectedUrlMenu.Add("http://www.basilico.net/catering_menu.html");
            expectedUrlMenu.Add("http://basilico.net/valentines_day_menu.html");
            expectedUrlMenu.Add("http://basilico.net/fathers_day_menu.html");

            return expectedUrlMenu;
        }

        private String getAntipastiCategory(ArrayList categories) 
        {
            String categoryResult = null;
            foreach (String catebory in categories)
            {
                if (catebory.Contains("Antipasti"))
                {
                    return catebory;
                }
            }

            return categoryResult;
        }
    }
}
