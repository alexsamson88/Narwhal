using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service.Models
{
    public class Travel
    {
        public Travel(int vessel,List<TrackingPoint> trackingPoints)
        {
            this.Vessel = vessel;
            this._orderedTrackingPoints = trackingPoints.OrderBy(tp => tp.Date).ToList();
            this._maxLatitude = trackingPoints.Max(tp => tp.Latitude);
            this._minLatitude = trackingPoints.Min(tp => tp.Latitude);
            this._maxLongitude = trackingPoints.Max(tp => tp.Longitude);
            this._minLongitude = trackingPoints.Min(tp => tp.Longitude);
            this._orderedSegments = AddOrderedSegments();
        }
        public int Vessel { get; set; }

        public List<Segment> GetSegments()
        {
            return _orderedSegments.ToList();
        }
        public List<Segment> GetSegmentsByDates(DateTime startDate, DateTime endDate)
        {
            return _orderedSegments.Where(s => (s.TrackingPoint1.Date <= startDate && s.TrackingPoint2.Date >= startDate) || 
            (s.TrackingPoint1.Date >= startDate && s.TrackingPoint2.Date <= endDate) || 
            (s.TrackingPoint1.Date < endDate && s.TrackingPoint2.Date >= endDate)).ToList(); 
        }
        private List<Segment> AddOrderedSegments()
        {
            List<Segment> output = new List<Segment>();
            for (int i = 0; i < this.trackingPoints.Count - 2; i++)
            {
                output.Add(new Segment(_orderedTrackingPoints[i],_orderedTrackingPoints[i + 1]));
            }
            return output;
        }

        private List<Segment> _orderedSegments;
        public List<Segment> OrderedSegments 
        {
            get
            {
                return _orderedSegments;
            }
        }


        private List<TrackingPoint> _orderedTrackingPoints;
        public List<TrackingPoint> trackingPoints
        {
            get 
            {
                return _orderedTrackingPoints;
            }
        }

        public DateTime StartDate 
        { 
            get 
            {
                return _orderedTrackingPoints.FirstOrDefault().Date;
            } 
        }
        public DateTime EndDate 
        {
            get
            {
                return _orderedTrackingPoints.LastOrDefault().Date;
            }
            
        }

        private double _totalDistance;
        public double TotalDistance 
        {
            get
            {
                return this._orderedSegments.Sum(s => s.Distance);    
            }
        }

        private double _maxLatitude;
        public double MaxLatitude 
        {
            get
            {
                return _maxLatitude;
            }
        }

        private double _minLatitude;
        public double MinLatitude
        {
            get
            {
                return _minLatitude;
            }
        }

        private double _maxLongitude;
        public double MaxLongitude
        {
            get
            {
                return _maxLongitude;
            }
        }

        private double _minLongitude;
        public double MinLongitude
        {
            get
            {
                return _minLongitude;
            }
        }

        public double AverageSpeed
        {
            get 
            {
                var timeSpanBetweenStartAndEndDate = EndDate - StartDate;
                return TotalDistance / timeSpanBetweenStartAndEndDate.TotalHours;
            }

        }

    }
}
