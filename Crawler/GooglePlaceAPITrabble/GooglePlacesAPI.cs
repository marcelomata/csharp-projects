using GooglePlaceAPIJson;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GooglePlacesAPI
{
    class RequestPlaces
    {
        
        public void getPlaces()
        {
            //1.332625,103.791126
            String latitude = "1.3310614";
            //String latitude = "-33.8670";
            String longitude = "103.7912269";
            //String longitude = "151.1957";
            double radius = 5000000;
            String types = "food";
            String name = "";
            //String name = "cruise";
            String apikey = "AIzaSyDsDqIho4W6umBh6rP0QMPDgi0XIMDzczo";
            String url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location="+latitude+","+longitude+"&radius="+radius+"&types="+types+"&name="+name+"&key="+apikey;
            //String url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=-33.8670,151.1957&radius=500&types=food&name=cruise&key=AIzaSyDsDqIho4W6umBh6rP0QMPDgi0XIMDzczo";

            HttpWebRequest webRequest = WebRequest.Create(@url) as HttpWebRequest;
            webRequest.Timeout = 20000;
            webRequest.Method = "GET";

            WebResponse response = webRequest.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                var r = new StreamReader(stream);
                var resp = r.ReadToEnd();
                writeResponseInFile(resp, @"c:\temp\googlePlacesApi.txt");
                GooglePlaceObjectParser objectPlaces = JsonConvert.DeserializeObject<GooglePlaceObjectParser>(@resp);
                getPlacesWebSite(objectPlaces);
                //Console.Write(resp);
                Console.ReadLine();
            }
        }

        public void getPlacesWebSite(GooglePlaceObjectParser objectPlaces)
        {
            IList<Place> places = objectPlaces.Results;
            String locationGoogle = "sg";
            String name;
            String url = "https://www.google.com." + locationGoogle + "/search?q=";
            String location = "Singapore";
            foreach (Place place in places) 
            {
                name = place.Name;
                url = url + name + location;
                HttpWebRequest webRequest = WebRequest.Create(@url) as HttpWebRequest;
                webRequest.Timeout = 20000;
                webRequest.Method = "GET";

                WebResponse response = webRequest.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    var r = new StreamReader(stream);
                    var resp = r.ReadToEnd();
                    writeResponseInFile(resp, @"c:\temp\googlePlacesHtml"+name+".html");

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(resp);

                    var findclasses = doc.DocumentNode.Descendants("a").Where(d => d.InnerText.Contains("Website"));
                    
                    foreach (HtmlAgilityPack.HtmlNode s in findclasses)
                    {
                        Console.WriteLine(s.Attributes[1].Value);
                    }
                    
                    Console.ReadLine();
                }
                url = "https://www.google.com." + locationGoogle + "/search?q=";
            }
        }

        private IEnumerable<HtmlAgilityPack.HtmlNode> getHtmlNodeByClasses(String currentUrl, String className)
        {
            WebClient webClient = new WebClient();
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(currentUrl);

            var findclasses = doc.DocumentNode.Descendants("div").Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains(className));

            return findclasses;
        }

        private void writeResponseInFile(String resp, String path)
        {           
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "");
            }

            // This text is always added, making the file longer over time
            // if it is not deleted.
            File.AppendAllText(path, resp);          
        }

        private String readFileString(String path)
        {
            // Open the file to read from.
            string readText = File.ReadAllText(path);
            return readText;
        }

    }
}
