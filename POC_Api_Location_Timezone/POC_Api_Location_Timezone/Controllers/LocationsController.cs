using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using POC_Api_Location_Timezone.Models;

namespace LocationAvailabilityAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private static List<Location> locations = new List<Location>
        {
            new Location { Name = "Pharmacy", Availability = new List<string> { "10:00 AM", "12:00 PM", "03:00 PM" } },
            new Location { Name = "Bakery", Availability = new List<string> { "10:30 AM", "11:30 AM", "12:30 PM" } },
            new Location { Name = "Barber Shop", Availability = new List<string> { "10:15 AM", "11:15 AM", "12:15 PM" } },
            new Location { Name = "Supermarket", Availability = new List<string> { "10:00 AM", "11:00 AM", "12:00 PM" } },
            new Location { Name = "Cinema complex", Availability = new List<string> { "10:30 AM", "11:30 AM", "12:30 PM" } },
            new Location { Name = "Candy Store", Availability = new List<string> { "10:15 AM", "11:15 AM", "02:00 PM" } },
            new Location { Name = "Book Shop", Availability = new List<string> { "10:00 AM" } },
            new Location { Name = "Petrol Station", Availability = new List<string> { "10:30 AM", "11:30 AM", "12:30 PM" } },
            new Location { Name = "Pizza Shop", Availability = new List<string> { "10:15 AM", "11:15 AM", "12:15 PM" } },
            new Location { Name = "Tea Stall", Availability = new List<string> { "09:00 AM", "09:15 AM" } }
        };

        [HttpGet]
        public IActionResult GetLocations()
        {
            List<Location> availableLocations = new List<Location>();

            foreach (var location in locations)
            {
                List<string> availableTimes = new List<string>();

                foreach (var timeSlot in location.Availability)
                {
                    if (IsTimeWithinRange(timeSlot, "10:00 AM", "1:00 PM"))
                    {
                        availableTimes.Add(timeSlot);
                    }
                }

                if (availableTimes.Count > 0)
                {
                    availableLocations.Add(new Location { Name = location.Name, Availability = availableTimes });
                }
            }

            if (availableLocations.Count == 0)
            {
                return NotFound("No locations available between 10:00 AM and 1:00 PM.");
            }

            return Ok(availableLocations);
        }

        //Need to modify below method as per the requirment
        private bool IsTimeWithinRange(string time, string startTime, string endTime)
        {
            var timeToCheck = DateTime.ParseExact(time, "h:mm tt", null);
            var startTimeToCheck = DateTime.ParseExact(startTime, "h:mm tt", null);
            var endTimeToCheck = DateTime.ParseExact(endTime, "h:mm tt", null);
            return (timeToCheck >= startTimeToCheck && timeToCheck <= endTimeToCheck);
        }
    }
}
