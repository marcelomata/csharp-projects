using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooglePlaceAPIJson
{
    public class GooglePlaceObjectParser
    {
        public IList<string> Html_attributions { get; set; }
        public IList<Place> Results { get; set; }
        public String Status { get; set; }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach(Place place in Results) 
            {
                if (place != null)
                {
                    builder.Append(place.ToString());
                }
            }
            return builder.ToString();
        }

        public String getNameOfAttributes() {
            StringBuilder builder = new StringBuilder();
            if (Results != null)
            {
                foreach (Place place in Results)
                {
                    builder.Append(Place.getNameOfAttributes());
                    builder.Append(";");
                }
            }
            return builder.ToString();
        }
    }

    public class Place : IEquatable<Place>
    {
        public Geometry Geometry { get; set; }
        public String Icon { get; set; }
        public String Id { get; set; }
        public String Name { get; set; }
        public OpeningHours Opening_hours { get; set; }
        public IList<Photo> Photos { get; set; }
        public String Place_id { get; set; }
        public Double Rating { get; set; }
        public String Reference { get; set; }
        public String Scope { get; set; }
        public IList<String> Types { get; set; }
        public String Vicinity { get; set; }
        public String Website { get; set; }

        public bool Equals(Place place)
        {
            return this.Id.Equals(place.Id); ;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as Place);
        }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Id);
            builder.Append(";");
            builder.Append(Name);
            builder.Append(";");
            if (Geometry != null)
            {
                builder.Append(Geometry.ToString());
            }
            else
            {
                builder.Append(Geometry.printNull());
            }
            builder.Append(Icon);
            builder.Append(";");
            if (Opening_hours != null)
            {
                builder.Append(Opening_hours.ToString());
            }
            else
            {
                builder.Append(OpeningHours.printNull());
            }
            builder.Append("[");
            if (Photos != null)
            {
                foreach (Photo photo in Photos)
                {
                    builder.Append(photo.ToString());
                    builder.Append(",");
                }
            }
            builder.Append("]");
            builder.Append(";");
            builder.Append(Place_id);
            builder.Append(";");
            builder.Append(Rating);
            builder.Append(";");
            builder.Append(Reference);
            builder.Append(";");
            builder.Append(Scope);
            builder.Append(";");
            builder.Append("[");
            if (Types != null)
            {
                foreach (String type in Types)
                {
                    builder.Append(type);
                    builder.Append(",");
                }
            }
            builder.Append("]");
            builder.Append(";");
            builder.Append(Vicinity);
            builder.Append(";");
            builder.Append(Website);
            builder.Append(";");
            return builder.ToString();
        }

        public static String getNameOfAttributes()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Id");
            builder.Append(";");
            builder.Append("Place Name");
            builder.Append(";");
            builder.Append(Geometry.getNameOfAttributes());
            builder.Append("Icon");
            builder.Append(";");
            builder.Append(OpeningHours.getNameOfAttributes());
            builder.Append(Photo.getNameOfAttributes());
            builder.Append(";");
            builder.Append("Rating");
            builder.Append(";");
            builder.Append("Reference");
            builder.Append(";");
            builder.Append("Scope");
            builder.Append(";");
            builder.Append("type");
            builder.Append(";");
            builder.Append("Vicinity");
            builder.Append(";");
            builder.Append("Website");
            builder.Append(";");
            return builder.ToString();
        }

    }

    public class Geometry
    {
        public Coordinates Location { get; set; }
        public Viewport Viewport { get; set; }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Location.ToString());
            if (Viewport != null)
            {
                builder.Append(Viewport.ToString());
            }
            else
            {
                builder.Append(Viewport.printNull());
            }
            return builder.ToString();
        }

        public static String getNameOfAttributes()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Location" + Coordinates.getNameOfAttributes());
            builder.Append("Viewport"+Viewport.getNameOfAttributes());
            return builder.ToString();
        }

        public static String printNull()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Coordinates.printNull());
            builder.Append(Viewport.printNull());
            return builder.ToString();
        }
        
    }

    public class Coordinates
    {
        public string lat { get; set; }
        public string lng { get; set; }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(lat);
            builder.Append(";");
            builder.Append(lng);
            builder.Append(";");
            return builder.ToString();
        }

        public static String getNameOfAttributes()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("lat");
            builder.Append(";");
            builder.Append("lng");
            builder.Append(";");
            return builder.ToString();
        }

        public static String printNull()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("");
            builder.Append(";");
            builder.Append("");
            builder.Append(";");
            return builder.ToString();
        }
    }

    public class Viewport
    {
        public Coordinates Northeast { get; set; }
        public Coordinates Southwest { get; set; }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Northeast.ToString());
            builder.Append(Southwest.ToString());
            return builder.ToString();
        }

        public static String getNameOfAttributes()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Northeast" + Coordinates.getNameOfAttributes());
            builder.Append("Southwest" + Coordinates.getNameOfAttributes());
            return builder.ToString();
        }

        public static String printNull()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Coordinates.printNull());
            builder.Append(Coordinates.printNull());
            return builder.ToString();
        }
    }

    public class OpeningHours
    {
        public Boolean Open_now { get; set; }
        public IList<string> Weekday_text { get; set; }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Open_now);
            builder.Append(";");
            builder.Append("[");
            foreach (String day in Weekday_text)
            {
                builder.Append(day);
                builder.Append(",");
            }
            builder.Append("]");
            builder.Append(";");
            return builder.ToString();
        }

        public static String getNameOfAttributes()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Open_now");
            builder.Append(";");
            
            builder.Append("day");
            builder.Append(";");
            
            return builder.ToString();
        }

        public static String printNull()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("");
            builder.Append(";");
            builder.Append("[");
            builder.Append("]");
            builder.Append(";");
            return builder.ToString();
        }
    }

    public class Photo
    {
        public int Height { get; set; }
        public IList<string> Html_attributions { get; set; }
        public String Photo_reference { get; set; }
        public int Width { get; set; }

        public override String ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Height);
            builder.Append(",");
            builder.Append(Width);
            builder.Append(",");
            builder.Append("[");
            foreach (String s in Html_attributions)
            {
                builder.Append(s);
                builder.Append(",");
            }
            builder.Append("]");
            builder.Append(",");
            builder.Append(Photo_reference);
            return builder.ToString();
        }

        public static String getNameOfAttributes()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Photo Height");
            builder.Append(",");
            builder.Append("Photo Width");
            builder.Append(",");
            
            builder.Append("Html_attributions");
            builder.Append(",");
            
            builder.Append("Photo_reference");
            return builder.ToString();
        }
    }

}
