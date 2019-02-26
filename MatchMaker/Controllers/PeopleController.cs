using MatchMaker.Models;
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
        // [FromBody] tells ASP.NET Core to us the input formatter to bind the provided JSON to a People model
        // POST api/<controller>/create
        [HttpPost]
        public void Create([FromBody] int? id, string firstname, string lastname, string course, string description, bool usertype)
        {
            MatchMakerEntities dbcontext = new MatchMakerEntities();
            People people = new People();

            people.firstname = firstname;
            people.lastname = lastname;
            people.course = course;
            people.description = description;
            people.usertype = usertype;

            dbcontext.People.Add(people);

            try
            {
                dbcontext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                dbcontext.SaveChanges();
            }
        }
    }
}