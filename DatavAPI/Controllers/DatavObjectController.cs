using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatavAPI.Models;
using DatavSimulator;
using DatavSimulator.DatavObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DatavAPI.Controllers
{
    [Route("api/obj")]
    [ApiController]
    public class DatavObjectController : ControllerBase
    {
        // GET: api/<DatavObjectController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DatavObjectController>/5
        [HttpGet("updateflop")]
        public string Get(string datavName, string flopName, int startNumber, int variation, int changeInterval)
        {
            Flop flop = new Flop(flopName, datavName, startNumber, startNumber, variation, changeInterval);
            Flop existFlop = Backend.Instance.Controller().GetFlop(datavName, flopName);
            FlopReturnValue ret = new FlopReturnValue("", 0);
            if (existFlop == null)
            {
                bool succ = Backend.Instance.Controller().NewObj(datavName, flop);
                if (succ)
                {
                    ret.value = flop.CurrentNumber;
                }
            }
            else
            {
                if (flop.Same(existFlop))
                {
                    ret.value = existFlop.CurrentNumber;
                }
                else
                {
                    Backend.Instance.Controller().UpdateObj(datavName, flop);
                    ret.value = startNumber;
                }
            }
            return ret.toJsonString();
        }

        // POST api/<DatavObjectController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DatavObjectController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DatavObjectController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
