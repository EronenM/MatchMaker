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
        // POST api/<controller>/createpeople
        [HttpPost]
        public IHttpActionResult CreatePeople([FromBody] int? id, string firstname, string lastname, string course, string description, bool usertype)
        {
            MatchMakerEntities dbContext = new MatchMakerEntities();
            People people = new People();

            people.firstname = firstname;
            people.lastname = lastname;
            people.course = course;
            people.description = description;
            people.usertype = usertype;

            if (people != null || people.GetType().GetProperties().Any())
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
            return Ok();
        }
        // Can accept JSON string, so in front-end parse (JSON.stringify) JS object to JSON string and send it.
        // Works fine if we have text to be processed server side
        // POST api/<controller>/create
        [HttpPost]
        public IHttpActionResult Create(People people)
        {
            MatchMakerEntities dbContext = new MatchMakerEntities();
            
            if (people != null || people.GetType().GetProperties().Any())
            {
                people.regdate = DateTime.Now.Date;

                var pwd = people.password;
                people.passwordhash = pwd.GetHashCode();
                people.password = null;

                dbContext.People.Add(people);
            }

            else
            {
                return NotFound();
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
            return Ok();
        }
        // Basic PUT method
        // PUT: api/<controller>/update/id
        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] People people)
        {
            MatchMakerEntities dbContext = new MatchMakerEntities();

            if (people != null || people.GetType().GetProperties().Any())
            {
                var entityMatch = dbContext.People.FirstOrDefault(p => p.person_id == id);

                entityMatch.firstname = people.firstname;
                entityMatch.lastname = people.lastname;
                entityMatch.course = people.course;
                entityMatch.description = people.description;
                entityMatch.usertype = people.usertype;

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
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}