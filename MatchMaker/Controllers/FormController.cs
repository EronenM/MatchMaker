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

        public List<string> interestList = new List<string>();
        public List<string> positionList = new List<string>();
        public List<string> technologyList = new List<string>();

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
    }
}
