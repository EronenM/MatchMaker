using MatchMaker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MatchMaker.Controllers
{
    public class PeopleController : ApiController
    {
        // [FromBody] Web API uses the Content-Type header to select a formatter
        // POST api/<controller>/create
        [HttpPost]
        public void Create([FromBody] int? id, string firstname, string lastname, string course, string description, bool usertype)
        {
            MatchMakerEntities dbContext = new MatchMakerEntities();
            People people = new People();

            people.firstname = firstname;
            people.lastname = lastname;
            people.course = course;
            people.description = description;
            people.usertype = usertype;

            if (people != null || !people.GetType().GetProperties().Any())
            {
                dbContext.People.Add(people);
            }

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                dbContext.SaveChanges();
            }
        }
        // Can accept JSON string, so in front-end parse (JSON.stringify) JS object to JSON string and send it.
        // Works fine if we have text to be processed server side
        // POST api/<controller>/createpeople
        [HttpPost]
        public void CreatePeople(People people/*, HttpRequestMessage request*/)
        {
            MatchMakerEntities dbContext = new MatchMakerEntities();
            //var requestString = request.Content.ReadAsStringAsync().Result;
            //var json = requestString;

            //people = JsonConvert.DeserializeObject<People>(json);

            if (people != null || !people.GetType().GetProperties().Any())
            {
                dbContext.People.Add(people);
            }

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                dbContext.SaveChanges();
            }
        }
    }
}