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

        // GET api/<DatavListController>/5
        [HttpGet("{name}/flops")]
        public ActionResult<Datav> GetFlops(string name)
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

        // POST api/<DatavListController>
        [HttpPost("{name}/stop")]
        public ActionResult StopDatav(string name)
        {
            bool succ = Backend.Instance.Controller().Stop(name);
            return succ ? Ok() : BadRequest();
        }

        // POST api/<DatavListController>
        [HttpPost("{name}/start")]
        public ActionResult StartDatav(string name)
        {
            bool succ = Backend.Instance.Controller().Start(name);
            return succ ? Ok() : BadRequest();
        }

        // POST api/<DatavListController>
        [HttpPost("{name}/pause")]
        public ActionResult PauseDatav(string name)
        {
            bool succ = Backend.Instance.Controller().Pause(name);
            return succ ? Ok() : BadRequest();
        }

        // POST api/<DatavListController>
        [HttpPost("{name}/reset")]
        public ActionResult ResetDatav(string name)
        {
            bool succ = Backend.Instance.Controller().Reset(name);
            return succ ? Ok() : BadRequest();
        }

        // DELETE api/<DatavListController>/5
        [HttpDelete("{name}")]
        public void Delete(int id)
        {

        }
    }
}
