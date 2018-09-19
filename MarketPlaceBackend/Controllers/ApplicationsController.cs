using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarketPlaceBackend.Models;
using MarketPlaceBackend.Contracts;

namespace MarketPlaceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _service;
        public ApplicationsController(IApplicationService service)
        {
            _service = service;
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<IEnumerable<Application>> GetApplication()
        {
            var applications = await _service.getAllApplications();
            return applications;
        }

        [HttpGet("all")]
        public async Task<IEnumerable<Object>> GetAllApplicationsWithoutId()
        {
            var applications = await _service.getAllApplicationsWithoutId();
            return applications;
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _service.getApplicationById(id);

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        //PUT: api/Applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication([FromRoute] string id, [FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != application.Id)
            {
                return BadRequest();
            }

            var result = await _service.updateApplication(application);
            if(result == "Not Found")
            {
                return NotFound();
            } else
            {
                return NoContent();
            }
        }

        // POST: api/Applications
        [HttpPost]
        public async Task<IActionResult> PostApplication([FromBody] Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _service.addApplication(application);

            return CreatedAtAction("GetApplication", new { id = application.Id }, application);
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _service.deleteApplicationById(id);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }
    }
}