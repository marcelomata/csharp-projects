using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooglePlaceAPIJson
{
    class GooglePlaceObjectParser
    {
        public IList<string> Html_attributions { get; set; }
        public IList<Place> Results { get; set; }
        public String Status { get; set; }
    }

    public class Place
    {
        public Geometry Geometry { get; set; }
        public String Icon { get; set; }
        public String Id { get; set; }
        public String Name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public IList<Photo> Photos { get; set; }
        public String Place_id { get; set; }
        public Double Rating { get; set; }
        public String Reference { get; set; }
        public String Scope { get; set; }
        public IList<String> Types { get; set; }
        public String Vicinity { get; set; }
    }

    public class Geometry
    {
        public Coordinates Location { get; set; }
        public Viewport Viewport { get; set; }
    }

    public class Coordinates
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class Viewport
    {
        public Coordinates Northeast { get; set; }
        public Coordinates Southwest { get; set; }
    }

    public class OpeningHours
    {
        public Boolean Open_now { get; set; }
        public IList<string> Weekday_text { get; set; }
    }

    public class Photo
    {
        public int Height { get; set; }
        public IList<string> Html_attributions { get; set; }
        public String Photo_reference { get; set; }
        public int Width { get; set; }
    }

}
