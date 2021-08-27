using Narwhal.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Narwhal.Tests.SegmentIntersection
{
    public class SegmentIntersectionCalculatorTest
    {
        //segmentsIntersectionCalculator.
        [Fact]
        public void SegmentIntersectionCalculator_ShouldReturnIntersection()
        {
            SegmentsIntersectionCalculator segmentsIntersectionCalculator = new SegmentsIntersectionCalculator(null, null);
            var result = segmentsIntersectionCalculator.GetIntersectionPoint(
                new Segment(new TrackingPoint()
                {
                    Latitude = 11,
                    Longitude = 10
                },
                new TrackingPoint()
                {
                    Latitude = 1,
                    Longitude = 1
                }
                ),
                new Segment(new TrackingPoint()
                {
                    Latitude = 11,
                    Longitude = 1
                },
                new TrackingPoint()
                {
                    Latitude = 1,
                    Longitude = 10
                }
                )
            );
        }
        [Fact]
        public void SegmentIntersectionCalculator_ShouldReturnNull()
        {
            SegmentsIntersectionCalculator segmentsIntersectionCalculator = new SegmentsIntersectionCalculator(null, null);
            var actual = segmentsIntersectionCalculator.GetIntersectionPoint(
                new Segment(new TrackingPoint()
                {
                    Latitude = 11,
                    Longitude = 10
                },
                new TrackingPoint()
                {
                    Latitude = 11,
                    Longitude = 1
                }
                ),
                new Segment(new TrackingPoint()
                {
                    Latitude = 1,
                    Longitude = 1
                },
                new TrackingPoint()
                {
                    Latitude = 4,
                    Longitude = 10
                }
                )
            );
            Assert.Equal(null, actual);
        }
    }
}
