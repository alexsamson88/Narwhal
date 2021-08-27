using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service.Models
{
    public class Intersection
    {
        private List<Travel> travels;
        public Intersection(List<Travel> travels)
        {
            this.travels = travels;
        }

        public List<IntersectionWarning> FindIntersections()
        {
            List<IntersectionWarning> output = new List<IntersectionWarning>();
            for (int i = 0; i < this.travels.Count - 1; i++)
            {
                for (int j = i + 1; j < this.travels.Count-1; j++)
                {
                    //output.AddRange(CompareTravels(this.travels[i], this.travels[j]));

                    if (DoesTravelIntercepts(this.travels[i], this.travels[j]))
                    {
                        output.AddRange(CompareTravels(this.travels[i], this.travels[j]));
                    }
                }
            }
            return output;
        }

        private bool DoesTravelIntercepts(Travel travel1, Travel travel2)
        {
            return (travel2.MaxLatitude >= travel1.MinLatitude && travel2.MinLatitude <= travel1.MaxLatitude) && (travel2.MaxLongitude >= travel1.MinLongitude && travel2.MinLongitude <= travel1.MaxLongitude);
        }

        private List<IntersectionWarning> CompareTravels(Travel travel1, Travel travel2)
        {
            var startDate = travel1.StartDate;
            return GetMatchingSegments(travel1, travel2);
            
        }

        private List<IntersectionWarning> GetMatchingSegments(Travel travel1,Travel travel2)
        {
            //Try to find matching segments on the second travel at some point on time to reduce the number of iteration
            List<Segment> segments =  travel1.GetSegments();
            var  intersectionWarnings = new List<IntersectionWarning>();
            foreach (var segment in segments)
            {
                var startDate = segment.TrackingPoint1.Date;
                var endDate = segment.TrackingPoint2.Date;
                //Add a gap to cover the one our gap
                var diff = (endDate - startDate).TotalHours;
                if (diff < 1)
                {
                    var timeToAdd = diff / 2;
                    startDate.AddHours(timeToAdd * -1);
                    endDate.AddHours(timeToAdd);
                }

                var segmentsToCompare = travel2.GetSegmentsByDates(startDate, endDate);
                //if no comparable segments found skip compare
                if (!segmentsToCompare.Any()) continue;
                var segmentInterceptionCalculator = new SegmentsIntersectionCalculator(segment, segmentsToCompare);
                intersectionWarnings.AddRange(segmentInterceptionCalculator.GetIntersections());
            }
            return intersectionWarnings;
        

           
            
            
        }
        

    }
}
