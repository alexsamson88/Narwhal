using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service.Models
{
    interface IDistanceCalculator
    {
        public Distance GetDistanceBetweenPoints(TrackingPoint geoPoint1, TrackingPoint geoPoint2);
    }
}
