using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service.Models
{
    public class IntersectionWarning
    {
        public IntersectionWarning(Segment segment1,Segment segment2,IntersectionPoint intersection)
        {
            Vessel1 = segment1.TrackingPoint1.Vessel;
            Vessel2 = segment2.TrackingPoint1.Vessel;
            this.IntersectionPoint = intersection;
            var vessel1DistanceToIntersection = new DistanceClaculator().GetDistanceBetweenPoints(3440.0647948164, segment1.TrackingPoint1, new TrackingPoint() { Latitude = IntersectionPoint.Latitude, Longitude = IntersectionPoint.Longitude });
            var vessel2DistanceToIntersection = new DistanceClaculator().GetDistanceBetweenPoints(3440.0647948164, segment2.TrackingPoint1, new TrackingPoint() { Latitude = IntersectionPoint.Latitude, Longitude = IntersectionPoint.Longitude });
            var timeSpanBetweenStartAndEndDate = segment1.TrackingPoint2.Date - segment1.TrackingPoint1.Date;
            var segment1AvgSpeed = segment1.Distance / timeSpanBetweenStartAndEndDate.TotalHours;

            var hourToAdd1 = vessel1DistanceToIntersection / segment1AvgSpeed;

            Vessel1IntersectionCrossingDate = segment2.TrackingPoint1.Date.AddHours(hourToAdd1);

            var timeSpanBetweenStartAndEndDate2 = segment2.TrackingPoint2.Date - segment2.TrackingPoint1.Date;
            var segment2AvgSpeed = segment2.Distance / timeSpanBetweenStartAndEndDate2.TotalHours;
            var hourToAdd2 = vessel2DistanceToIntersection / segment1AvgSpeed;
            Vessel2IntersectionCrossingDate = segment2.TrackingPoint1.Date.AddHours(hourToAdd2);




        }
        public int Vessel1 { get; set; }

        public int Vessel2 { get; set; }
        public DateTime Vessel1IntersectionCrossingDate { get; set; }

        public DateTime Vessel2IntersectionCrossingDate { get; set; }
        public IntersectionPoint IntersectionPoint { get; set; }
    }
}
