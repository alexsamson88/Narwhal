using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Narwhal.Service.Models
{
    public class SegmentsIntersectionCalculator
    {

        private readonly Segment _mainSegments;

        private readonly List<Segment> _segmentsToCompare;

        public SegmentsIntersectionCalculator(Segment mainSegments,List<Segment> segmentsToCompare)
        {
            this._mainSegments = mainSegments;
            this._segmentsToCompare = segmentsToCompare;
        }

        public List<IntersectionWarning> GetIntersections() 
        {
            List<IntersectionWarning> output = new List<IntersectionWarning>();
            
            foreach (Segment segment in _segmentsToCompare)
            {
                var result = GetIntersectionPoint(_mainSegments, segment);
                if (result != null) output.Add(new IntersectionWarning(_mainSegments, segment, result));
            }
            
            return output;
        }

        public IntersectionPoint GetIntersectionPoint(Segment segment1, Segment segment2)
        {
            var ax = segment1.TrackingPoint1.Latitude;
            var ay = segment1.TrackingPoint1.Longitude;

            var bx = segment1.TrackingPoint2.Latitude;
            var by = segment1.TrackingPoint2.Longitude;

            var cx = segment2.TrackingPoint1.Latitude;
            var cy = segment2.TrackingPoint1.Longitude;

            var dx = segment2.TrackingPoint2.Latitude;
            var dy = segment2.TrackingPoint2.Longitude;

           

            var a1 = by - ay;
            var b1 = ax - bx;
            var c1 = a1 * ax + b1 * ay;


            var a2 = dy - cy;
            var b2 = cx - dx;
            var c2 = a2 * cx + b2 * cy;

            double det = a1 * b2 - a2 * b1;
            
            double x = (b2 * c1 - b1 * c2) / det;
            double y = (a1 * c2 - a2 * c1) / det;
            double tolerance = 0.0001;
            return (((Math.Min(ax ,bx) - tolerance <= x && Math.Max(ax,bx) + tolerance >= x)  && (Math.Min(ay, by) - tolerance <= y && Math.Max(ay, by) + tolerance >= y)) && 
                ((Math.Min(cx, dx) - tolerance <= x && Math.Max(cx, dx) + tolerance >= x) && (Math.Min(cy, dy) - tolerance <= y && Math.Max(cy, dy) + tolerance >= y))) ?   new IntersectionPoint() { Latitude = x, Longitude = y } : null;


        }

    }
}
