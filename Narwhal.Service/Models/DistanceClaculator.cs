using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service.Models
{
    public class DistanceClaculator
    {
        //= 3440.0647948164
        public double GetDistanceBetweenPoints(double radius , TrackingPoint geoPoint1, TrackingPoint geoPoint2)
        {
            var degreeLatitude = DegToRad(geoPoint1.Latitude - geoPoint2.Latitude);
            var degreeLongitude = DegToRad(geoPoint1.Longitude- geoPoint2.Longitude);

            var a = Math.Sin(degreeLatitude / 2) * Math.Sin(degreeLatitude / 2) +
                    Math.Cos(DegToRad(geoPoint1.Latitude)) * Math.Cos(DegToRad(geoPoint2.Latitude)) *
                    Math.Sin(degreeLongitude / 2) * Math.Sin(degreeLongitude / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = radius * c; 
            return d;
        }

        private double DegToRad(double deg)
        {
            return deg * (Math.PI / 180);
        }

    }
}
