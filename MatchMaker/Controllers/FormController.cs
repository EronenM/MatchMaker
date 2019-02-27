using MatchMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MatchMaker.Controllers
{
    public class FormController : ApiController
    {
        //instantiate needed variables
        public List<string> interestList = new List<string>();
        public List<string> positionList = new List<string>();
        public List<string> technologyList = new List<string>();
        public List<People> peopleList = new List<People>();

        People pl = new People();
        Preferences pr = new Preferences();

        MatchMakerEntities db = new MatchMakerEntities();

        //GET action for fieldofinterests
        // api/form/fieldofinterest
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult FieldOfInterest()
        {
            interestList.Add("Computers and Technology");
            interestList.Add("Health Care and Allied Health");
            interestList.Add("Education and Social Services");
            interestList.Add("Arts and Communications");
            interestList.Add("Trades and Transportation");
            interestList.Add("Management, Business, and Finance");
            interestList.Add("Architecture and Civil Engineering");
            interestList.Add("Science");

            return Ok(interestList);
        }

        //GET action for positions
        // api/form/position
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult Position()
        {
            positionList.Add("Cloud Architect");
            positionList.Add("Cloud Services Developer");
            positionList.Add("Cloud Software and Network Engineer");
            positionList.Add("Computer and Information Research Scientist");
            positionList.Add("IT Analyst");
            positionList.Add("Network Architect");
            positionList.Add("Database Administrator");
            positionList.Add("Application Support Analyst");
            positionList.Add("Application Developer");
            positionList.Add("Java Developer");
            positionList.Add("Software Architect");
            positionList.Add("Front End Developer");
            positionList.Add("Web Developer");

            return Ok(positionList);
        }

        //GET action for technologies
        // api/form/technologies
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult Technologies()
        {
            technologyList.Add("Java");
            technologyList.Add("Visual Basic");
            technologyList.Add("JavaScript");
            technologyList.Add("Unix Shell Scripting");
            technologyList.Add("HTML, XHTML");
            technologyList.Add("MySQL");
            technologyList.Add("Ruby on Rails");
            technologyList.Add("Python");
            technologyList.Add("C#");
            technologyList.Add("PHP");

            return Ok(technologyList);
        }

        // Get action for all persons in people datatable
        // api/form/Getall
        [HttpGet]
        [ResponseType(typeof(IEnumerator<People>))]
        public IHttpActionResult GetAll()
        {
            var people = from p in db.People
                         orderby p.person_id
                         select p;

            foreach (var person in people)
            {
                peopleList.Add(person);
            }

            return Ok(peopleList);
        }
        // GET: Current user email + pwd, returns People object
        // /api/{controller}/login
        [HttpGet]
        [ResponseType(typeof(People))]
        public IHttpActionResult Login(People people)
        {
            MatchMakerEntities dbContext = new MatchMakerEntities();
            var entityMatch = dbContext.People.First(p => p.email == people.email);

            var clientHash = people.password.GetHashCode();
            if (clientHash == entityMatch.passwordhash && people.email == entityMatch.email)
            {
                return Ok(entityMatch);
            }
            else
            {
                return BadRequest("Password hash did not match with given information");
            }
        }

        // poistaa sekä käyttäjän profiilin, että profiilin takana olevan preferenssin, jos sellainen löytyy
        // DELETE api/form/delete/5
        [HttpDelete]
        public IHttpActionResult DeleteFull(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("not a valid id");
            }
            else if (db.People.Find(id) == null)
            {
                return BadRequest("no account with given id exists");
            }
            else
            {
                pl = db.People.Find(id);
                db.People.Remove(pl);
                pr = db.Preferences.Where(x => x.person_id == id).FirstOrDefault();

                if (pr != null)
                {
                    db.Preferences.Remove(pr);
                }
                db.SaveChanges();
            }
            return Ok("account deleted");
        }

        [HttpDelete]
        public IHttpActionResult DeletePreferences(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("not a valid id");
            }
            else if (db.People.Find(id) == null)
            {
                return BadRequest("no account with given id exists");
            }
            else
            {
                pl = db.People.Find(id);
                pr = db.Preferences.Where(x => x.person_id == id).FirstOrDefault();

                if (pr == null)
                {
                    return BadRequest("no preferences for given account");
                }
                else
                {
                    db.Preferences.Remove(pr);
                }
                db.SaveChanges();

            }
            return Ok("preferences deleted");
        }
    }
}



