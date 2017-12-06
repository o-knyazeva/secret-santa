using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace web.secret_santa.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static Group[] SampleGroups = new[]
        {
            new Group
            {
                Id = 1,
                Name = "NY 2018",
            }
        };

        private static SecretSantaUser[] SampleUsers = new[]
        {
            new SecretSantaUser
            {
                Id = 1,
                GroupId = 1,
                Name = "Olga",
                Email = "o.knyazeva.j@gmail.com",
                Letter = "Dear secret santa..."
            },
            new SecretSantaUser
            {
                Id =2,
                GroupId = 1,
                Name = "Vitaly",
                Email = "permanent256@gmail.com",
                Letter = "Zdravstvuy Dedushka Moroz..."
            }
        };
        
        [HttpGet("[action]")]
        public IEnumerable<Group> Groups()
        {
            return SampleGroups;
        }

        [HttpGet("[action]/{groupId}")]
        public IEnumerable<SecretSantaUser> Users(int groupId)
        {
            return SampleUsers.Where(t => t.GroupId == groupId).ToList();
        }

        public class Group
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class SecretSantaUser
        {
            public int Id { get; set; }
            public int GroupId { get; set; }
             
            public string Name { get; set; }
            public string Email { get; set; }
            public string Letter { get; set; }
        }

        //public class WeatherForecast
        //{
        //    public string DateFormatted { get; set; }
        //    public int TemperatureC { get; set; }
        //    public string Summary { get; set; }

        //    public int TemperatureF
        //    {
        //        get
        //        {
        //            return 32 + (int)(TemperatureC / 0.5556);
        //        }
        //    }
        //}
    }
}
