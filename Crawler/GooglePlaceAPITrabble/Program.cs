using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GooglePlacesAPI;

namespace GooglePlaceAPITrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestPlaces request = new RequestPlaces();
            request.getPlaces();
        }
    }
}
