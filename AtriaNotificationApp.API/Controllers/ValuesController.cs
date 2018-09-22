using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AtriaNotificationApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtriaNotificationApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private static List<StudentDto> students = new List<StudentDto>();

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> Get()
        {
            return students;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<StudentDto> Get(int id)
        {
            var foundStudent = students.FirstOrDefault(x => x.StudentID == id);
            if(foundStudent == null)
            {
                return NotFound();
            }
            return foundStudent;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] StudentDto value)
        {
            students.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
