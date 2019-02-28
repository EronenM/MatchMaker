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
    public class FormController : ApiController
    {
        //GET action for fieldofinterests
        // api/form/fieldofinterest
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult FieldOfInterest()
        {
            List<string> interestList = new List<string>();

            interestList.Add("Health Care");
            interestList.Add("Education and Social Services");
            interestList.Add("Arts and Communications");
            interestList.Add("Trades and Transportation");
            interestList.Add("Management, Business, and Finance");
            interestList.Add("Architecture and Civil Engineering");
            interestList.Add("Industry");

            return Ok(interestList);
        }

        //GET action for positions
        // api/form/position
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult Position()
        {
            List<string> positionList = new List<string>();

            positionList.Add("Backend Developer");
            positionList.Add("Frontend Developer");
            positionList.Add("Data analyst");
            positionList.Add("Software Architect");
            positionList.Add("Database Administrator");
            positionList.Add("DevOps Engineer");
            positionList.Add("Cloud Services Developer");
            positionList.Add("Network Architect");
            positionList.Add("Application Support");
            positionList.Add("Application Sales");
            positionList.Add("UI Designer");
            positionList.Add("UX Designer");
            
            return Ok(positionList);
        }

        //GET action for technologies
        // api/form/technologies
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        public IHttpActionResult Technologies()
        {
            List<string> technologyList = new List<string>();

            technologyList.Add("C#");
            technologyList.Add("JavaScript");
            technologyList.Add("Java");
            technologyList.Add("Visual Basic");
            technologyList.Add("React");
            technologyList.Add("HTML, XHTML");
            technologyList.Add("MariaDB");
            technologyList.Add("Ruby");
            technologyList.Add("Python");
            technologyList.Add("PHP");
            technologyList.Add("Angular");
            technologyList.Add("Vue.js");
            
            return Ok(technologyList);
        }

        // Get action for all persons in people datatable
        // api/form/Getall
        [HttpGet]
        [ResponseType(typeof(IEnumerator<People>))]
        public IHttpActionResult GetAll()
        {
            MatchMakerEntities db = new MatchMakerEntities();

            List<People> peopleList = new List<People>();

            People pl = new People();
            Preferences pr = new Preferences();

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
        public IHttpActionResult DeleteFull(int id)
        {

            MatchMakerEntities db = new MatchMakerEntities();

            People pl = new People();
            Preferences pr = new Preferences();

            if (id <= 0)
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

        // DELETE poistaa vain käyttäjän preferenssit, jos sellaiset löytyy
        // api/form/DeletePreferences
        [HttpDelete]
        public IHttpActionResult DeletePreferences(int id)
        {

            MatchMakerEntities db = new MatchMakerEntities();

            People pl = new People();
            Preferences pr = new Preferences();

            if (id <= 0)
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

        //adds preferences for person
        //api/form/CreatePreferences
        [HttpPost]
        public IHttpActionResult CreatePreferences(Preferences pref)
        {

            MatchMakerEntities db = new MatchMakerEntities();
            MatchMakerEntities dbContext = new MatchMakerEntities();

            if (db.People.Find(pref.person_id) != null && dbContext.Preferences.Find(pref.person_id) == null)
            {
                db.Preferences.Add(pref);
            }
            else
            {
                return BadRequest("Preferences with given person_id already exists");
            }

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                db.SaveChanges();
            }
            return Ok();
        }

        //Update preferences for given ID
        //api/form/updatepreferences
        [HttpPut]
        public IHttpActionResult UpdatePreferences(int id, Preferences preferences)
        {
            MatchMakerEntities db = new MatchMakerEntities();

            var prefs = db.Preferences.Where(x => x.person_id == id).FirstOrDefault();

            if (prefs == null)
            {
                return BadRequest("No preferences exists for given ID");
            }
            else
            {
                prefs.fieldofinterest1 = preferences.fieldofinterest1;
                prefs.fieldofinterest2 = preferences.fieldofinterest2;
                prefs.fieldofinterest3 = preferences.fieldofinterest3;

                prefs.position1 = preferences.position1;
                prefs.position2 = preferences.position2;
                prefs.position3 = preferences.position3;

                prefs.technology1 = preferences.technology1;
                prefs.technology2 = preferences.technology2;
                prefs.technology3 = preferences.technology3;
            }
            db.SaveChanges();

            return Ok("Updated");
        }
    }
}



