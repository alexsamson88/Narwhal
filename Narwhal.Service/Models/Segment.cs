using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service.Models
{
    public class Segment
    {
        public Segment(TrackingPoint trackingPoint1,TrackingPoint trackingPoint2)
        {
            this._trackingPoint1 = trackingPoint1;
            this._trackingPoint2 = trackingPoint2;
            //3440.0647948164
            _distance = new DistanceClaculator().GetDistanceBetweenPoints(3440.0647948164, trackingPoint1, trackingPoint2);

        }
        private TrackingPoint _trackingPoint1;
        public TrackingPoint TrackingPoint1 {
            get
            {
                return _trackingPoint1;   
            }
        }
        private TrackingPoint _trackingPoint2;
        public TrackingPoint TrackingPoint2 
        {
            get
            {
                return _trackingPoint2;
            }
        }

        private double _distance;
        public double Distance 
        {
            get
            {
                return _distance;
            }
        }



    }
}
