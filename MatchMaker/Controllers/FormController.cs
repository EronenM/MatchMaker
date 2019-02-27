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

        // poistaa sekä käyttäjän profiilin, että profiilin takana olevan preferenssin, jos sellainen löytyy
        // DELETE api/form/delete/5
        [HttpDelete]
        public IHttpActionResult DeleteFull(int? id)
        {

            MatchMakerEntities db = new MatchMakerEntities();

            People pl = new People();
            Preferences pr = new Preferences();

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

        // DELETE poistaa vain käyttäjän preferenssit, jos sellaiset löytyy
        // api/form/DeletePreferences
        [HttpDelete]
        public IHttpActionResult DeletePreferences(int? id)
        {

            MatchMakerEntities db = new MatchMakerEntities();

            People pl = new People();
            Preferences pr = new Preferences();

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


    }
}



