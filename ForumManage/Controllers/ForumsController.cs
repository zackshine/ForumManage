using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumManage.Models;
using ForumManage.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ForumManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumsController : ControllerBase
    {
        private readonly IRepository<Forum> _repository;
        public ForumsController(IRepository<Forum> repository)
        {
            _repository = repository;
        }
        // GET api/forums
        [HttpGet]
        public ActionResult<List<Forum>> Get()
        {
            return _repository.GetAll();

        }

        // GET api/forums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Forum>> GetById(long id)
        {
            var forum = await _repository.GetById(id);
            if (forum == null)
            {
                return NotFound();
            }

            return forum;
        }

        // POST api/forums
        [HttpPost]
        public async Task<ActionResult<Forum>> Post([FromBody] Forum forum)
        {
            return await _repository.Add(forum);
            
        }

        // PUT api/forums/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Forum>> Put(long id, [FromBody] Forum forum)
        {
            if (id != forum.Id)
            {
                return BadRequest();
            }
            return await _repository.Update(id, forum);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return Ok(id);
        }
    }
}
