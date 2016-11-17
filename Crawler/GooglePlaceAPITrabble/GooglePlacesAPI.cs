using GooglePlaceAPIJson;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GooglePlacesAPI
{
    class RequestPlaces
    {
        
        public void getPlaces()
        {
            //X-> lines
            //Y-> columns
            //max x = 1.467792     min x = 1.240919
            //max y = 104.028248     min y = 103.613171
            //step X 10 m = 0.000100
            //step Y 10 m = 0.000150
            //1.240919, 103.832554 -> south limit
            //1.467792, 103.805089 -> north limit
            //1.360365, 104.028248 -> east limit
            //1.272840, 103.613171 -> west limit
            //1.332625,103.791126

            //String latitude = "-33.8670";
            //String longitude = "151.1957";

            //Double maxX = 1.467800;
            Double maxX = 1.367800;
            //Double maxY = 104.028250;
            Double maxY = 104.008250;
            //Double minX = 1.240900;
            Double minX = 1.300900;
            //Double minY = 103.613150;
            Double minY = 103.813150;
            //Double stepX = 0.000100; //10 m
            Double stepX = 0.001000;
            //Double stepY = 0.000150; //10 m
            Double stepY = 0.001150;
            
            String latitude;
            String longitude;
            //double radius = 1000;
            double radius = 300;
            String types = "food";
            String name = "";
            String apikey = "AIzaSyDsDqIho4W6umBh6rP0QMPDgi0XIMDzczo";
            //String url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + latitude + "," + longitude + "&radius=" + radius + "&types=" + types + "&name=" + name + "&key=" + apikey;
            String url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";

            IList<Place> allPlaces = new List<Place>();
            IList<Place> currentPlaces;
            int numRequest = 0;

            for (Double i = minX; i < maxX; i += stepX)
            {
                latitude = ("" + i).Replace(",", ".");//.Substring(0, 8);
                for (Double j = minY; j < maxY; j += stepY)
                {
                    currentPlaces = new List<Place>();
                    longitude = ("" + j).Replace(",", ".");//.Substring(0, 10);
                    url = url + "location=" + latitude + "," + longitude + "&radius=" + radius + "&types=" + types + "&name=" + name + "&key=" + apikey;

                    HttpWebRequest webRequest = WebRequest.Create(@url) as HttpWebRequest;
                    webRequest.Timeout = 20000;
                    webRequest.Method = "GET";

                    //Console.WriteLine(url);
                    writeResponseInFile(url+"\n", @"c:\temp\googlePlacesUrls.txt");
                    WebResponse response = webRequest.GetResponse();
                    numRequest++;
                    using (var stream = response.GetResponseStream())
                    {
                        var r = new StreamReader(stream);
                        var resp = r.ReadToEnd();
                        //writeResponseInFile(resp, @"c:\temp\googlePlacesApi.txt");
                        GooglePlaceObjectParser objectPlaces = JsonConvert.DeserializeObject<GooglePlaceObjectParser>(@resp);
                        //getPlacesWebSite(objectPlaces);

                        foreach (Place p in objectPlaces.Results) 
                        {
                            if (!allPlaces.Contains(p))
                            {
                                allPlaces.Add(p);
                                currentPlaces.Add(p);
                            }
                        }
                        //Console.Write(resp);
                        //Console.ReadLine();
                    }
                    url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?";
                    writePlacesCsvFile(currentPlaces);
                }
            }
            Console.WriteLine(allPlaces.Count);
        }

        public void getPlacesWebSite(GooglePlaceObjectParser objectPlaces)
        {
            //byte[] buffer = Encoding.ASCII.GetBytes("code=" + 1234 + "&client_id=marcatam.o&client_secret=Incompletude31&redirect_uri=xxxx&grant_type=authorization_code");
            /*
            byte[] buffer = Encoding.ASCII.GetBytes("code=" + 1234 + "&client_id=marcatam.o&client_secret=Incompletude31&grant_type=authorization_code");
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = buffer.Length;

            Stream strm = req.GetRequestStream();
            strm.Write(buffer, 0, buffer.Length);
            strm.Close();

            //HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
             */

            IList<Place> places = objectPlaces.Results;
            String locationGoogle = "sg";
            String name;
            String url = "https://www.google.com." + locationGoogle + "/search?q=";
            String location = "Singapore";
            foreach (Place place in places) 
            {
                name = place.Name;
                url = url + name + " " + location;
                HttpWebRequest webRequest = WebRequest.Create(@url) as HttpWebRequest;
                webRequest.Timeout = 20000;
                webRequest.Method = "GET";

                WebResponse response = webRequest.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    var r = new StreamReader(stream);
                    var resp = r.ReadToEnd();
                    //writeResponseInFile(resp, @"c:\temp\googlePlacesHtml"+name+".html");

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(resp);

                    var findclasses = doc.DocumentNode.Descendants("a").Where(d => d.InnerText.Contains("Website"));

                    string pattern = "(://.*)(.*)(/)";
                    foreach (HtmlAgilityPack.HtmlNode s in findclasses)
                    {
                        if (s.Attributes.Count > 0 && s.InnerText.Length <= "Website".Length)
                        {
                            //Console.WriteLine(url);
                            //Console.WriteLine(name);
                            //Console.WriteLine(pattern);
                            //Console.WriteLine(s.Attributes[1].Value);
                            MatchCollection matches = Regex.Matches(s.Attributes[s.Attributes.Count-1].Value, pattern);
                            int count = 0;
                            foreach (var match in matches)
                            {
                                //Console.WriteLine(match.ToString().Replace("://", ""));
                                //writeResponseInFile(match.ToString().Replace("://", "")+"\n", @"c:\temp\googlePlacesWebSites.txt");
                                place.Website = match.ToString().Replace("://", "");
                                if (count >= 1)
                                {
                                    break;
                                }
                                count++;
                            }
                        }
                    }
                }
                //Console.WriteLine("\n");
                url = "https://www.google.com." + locationGoogle + "/search?q=";
            }
            //Console.ReadLine();
        }

        public void writePlacesCsvFile(IList<Place> places)
        {
            String path;
            foreach (Place place in places) {
                //path = @"c:\temp\"+place.Name.Replace("/", "")+".csv";
                path = @"c:\temp\PlacesSingapore.csv";
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "");
                    File.AppendAllText(path, Place.getNameOfAttributes() + "\n");
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                //File.WriteAllText(path, "");
                File.AppendAllText(path, place.ToString() + "\n");
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
            File.WriteAllText(path, "");
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
