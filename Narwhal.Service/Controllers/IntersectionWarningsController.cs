using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Narwhal.Service.Models;
using Narwhal.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narwhal.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntersectionWarningsController : ControllerBase
    {
        private readonly ILogger<TrackingController> _logger;
        private readonly DatabaseService _databaseService;
        private readonly MessagingService _messagingService;

        public IntersectionWarningsController(
            ILogger<TrackingController> logger,
            DatabaseService databaseService,
            MessagingService messagingService)
        {
            _logger = logger;
            _databaseService = databaseService;
            _messagingService = messagingService;
        }

        [HttpGet("Get")]

        public IEnumerable<IntersectionWarning> Get(DateTime? from = null, DateTime? to = null)
        {
            var trackingCollection = _databaseService.GetTrackingCollection();
            var trackingPointList = trackingCollection
                .Find(Builders<BsonDocument>.Filter.Gte("Date", from) & Builders<BsonDocument>.Filter.Lt("Date", to))
                .ToEnumerable()
                .ToArray()
                .Select(b => new TrackingPoint()
                {
                    Vessel = b["Vessel"].AsInt32,
                    Date = b["Date"].AsDateTime,
                    Latitude = b["Position"][1].AsDouble,
                    Longitude = b["Position"][0].AsDouble
                });
            var travels = trackingPointList.GroupBy(tp => tp.Vessel).Select(tpg => new Travel(tpg.Key, tpg.ToList())).ToList();

            var intersections = new Intersection(travels);
            var warnings = intersections.FindIntersections();
            //filter warning for intersection where vessel will intersect within one hour
            //May be that could be a future parameter for the method
            var filteredWarnings =
                warnings.Where(
                    w => {
                        var timeSpanInHours = (w.Vessel1IntersectionCrossingDate - w.Vessel2IntersectionCrossingDate).TotalHours;
                        return (timeSpanInHours > 0 && timeSpanInHours <= 1) || (timeSpanInHours < 0 && timeSpanInHours >= -1);
                    });
            return filteredWarnings;

        }
    }
}
