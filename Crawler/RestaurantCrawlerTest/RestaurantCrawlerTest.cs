using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using CrawlerTrabble;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class RestaurantCrawlerTest
    {
        private String url;

        [TestInitialize()]
        public void Initialize() 
        { 
            //this.url = "http://www.carnivore.com.sg/"; //403 error to access this url. Need check why.
            this.url = "http://www.basilico.net/";
        }

        [TestMethod]
        public void TestCheckNumUrlMenu()
        {
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            ArrayList urlsMenu = getUrlsMenu(crawler);
            //http://www.basilico.net/lunch_menu.html
            //http://www.basilico.net/dinner_menu.html
            //http://www.basilico.net/catering_menu.html
            //http://www.basilico.net/funeral_luncheon.html  the simple crawler will not take this menu for now
            //http://basilico.net/fathers_day_menu.html image, need OCR
            //http://basilico.net/valentines_day_menu.html image, need OCR
            int expectedNumUrls = 6;
            Assert.AreEqual(expectedNumUrls, urlsMenu.Count, 0, "Number of urls does not match.");        
        }

        [TestMethod]
        public void TestGetMenuName()
        {
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            ArrayList urlsMenu = getUrlsMenu(crawler);
            ArrayList data;
            ArrayList expectedData = getExpectedItens();

            foreach (String urlMenu in urlsMenu)
            {
                data = crawler.getDataItens(urlMenu);
                int expectedNumItens = 8;
                Assert.AreEqual(expectedNumItens, data.Count, 0, "Number of itens in the menu does not match.");
                for (int i = 0; i < data.Count; i++)
                {
                    Assert.AreEqual((String)expectedData[i], (String)data[i], true, "Number of itens in the menu does not match.");
                }
            }
        }

        [TestMethod]
        public void TestGetMenuDescriptionBeef()
        {
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            //http://www.basilico.net/lunch_menu.html
            //http://www.basilico.net/dinner_menu.html
            //http://www.basilico.net/catering_menu.html
            //http://www.basilico.net/funeral_luncheon.html  the simple crawler will not take this menu for now
            //http://basilico.net/fathers_day_menu.html image, need OCR
            //http://basilico.net/valentines_day_menu.html image, need OCR
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

        private ArrayList getExpectedItens()
        {
            ArrayList expectedData = new ArrayList();
            expectedData.Add("BEEF");
            expectedData.Add("LAMB");
            expectedData.Add("CHICKEN");
            expectedData.Add("FISH");
            expectedData.Add("PORK");
            expectedData.Add("OTHERS");
            expectedData.Add("CODIMENTS");

            return expectedData;
        }

        private ArrayList getUrlsMenu(RestaurantCrawler crawler)
        {
            crawler.setKeyWord("menu");
            ArrayList urlsMenu = crawler.getUrlsKeyWord();
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
    }
}
