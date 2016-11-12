using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using CrawlerTrabble;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class RestaurantCrawlerTest
    {
        [TestMethod]
        public void TestGetMenuName()
        {
            String url = "http://www.carnivore.com.sg/";
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            ArrayList urlsMenu = getUrlsMenu(crawler);
            ArrayList data;
            ArrayList expectedData = getExpectedItens();

            foreach (String urlMenu in urlsMenu)
            {
                data = crawler.getData(urlMenu);
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
            String url = "http://www.carnivore.com.sg/";
            RestaurantCrawler crawler = new RestaurantCrawler(url);
            //http://www.carnivore.com.sg/churrasco-menu
            ArrayList urlsMenu = getUrlsMenu(crawler);
            int expectedNumUrls = 1;
            Assert.AreEqual(expectedNumUrls, urlsMenu.Count, 0, "Number of urls does not match.");

            String expectedDescription = "Boneless Leg of Lamb";
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
            //http://www.carnivore.com.sg/churrasco-menu
            ArrayList urlsMenu = crawler.getUrlsKeyWord();
            int expectedNumUrls = 1;
            Assert.AreEqual(expectedNumUrls, urlsMenu.Count, 0, "Number of urls does not match.");

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
