using MatchMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MatchMaker.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //lisätty
        // GET: api/Tuote

        //People pl = new People();
        //MatchMakerEntities db = new MatchMakerEntities();
        //GET api/values
        //public List<People> Get()
        //{
            
        //    var tuotteet = (from t in db.People
        //                    select t).ToList();
        //    return tuotteet;
        //}


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
