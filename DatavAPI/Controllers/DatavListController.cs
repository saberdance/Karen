using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatavAPI.Models;
using DatavSimulator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatavAPI.Controllers
{
    [Route("api/datav")]
    [ApiController]
    public class DatavListController : ControllerBase
    {
        // GET: api/<DatavListController>
        [HttpGet]
        public ActionResult<List<Datav>> Get()
        {
            return Backend.Instance.Controller().GetRunningDatavs();
        }

        // GET api/<DatavListController>/5
        [HttpGet("{name}")]
        public ActionResult<Datav> Get(string name)
        {
            var datav = Backend.Instance.Controller().GetDatav(name);
            if (datav.IsEmpty())
            {
                return NotFound();
            }
            return datav;
        }

        // POST api/<DatavListController>
        [HttpPost]
        public ActionResult Post(string name)
        {
            return Backend.Instance.Controller().NewDatav(name) ? Ok() : BadRequest();
        }

        // PUT api/<DatavListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DatavListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
