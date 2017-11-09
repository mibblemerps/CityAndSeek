using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Common
{
    public class LatLng
    {
        public double Latitude;
        public double Longitude;

        public LatLng(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return Latitude + ", " + Longitude;
        }
    }
}
