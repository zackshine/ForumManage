using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ForumManage.Models;
using ForumManage.Models.ViewModels;
using ForumManage.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ForumManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumsController : ControllerBase
    {
        private readonly IRepository<Forum> _repository;
        private readonly IMapper _mapper;
      
        public ForumsController(IRepository<Forum> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
        public async Task<ActionResult<ForumVM>> Post([FromBody] ForumVM forumVM)
        {
            Forum forum = _mapper.Map<Forum>(forumVM);
            Forum result =  await _repository.Add(forum);
            return _mapper.Map<ForumVM>(result);            
        }

        // PUT api/forums/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ForumVM>> Put(long id, [FromBody] ForumVM forumVM)
        {
            var entity = await _repository.GetById(id);

            Forum forum = _mapper.Map<Forum>(forumVM);
            forum.Id = id;
            forum.AddedDate = entity.AddedDate;
            forum.ModifiedDate = entity.ModifiedDate;
            forum.IsDeleted = entity.IsDeleted;

            Forum result = await _repository.Update(id,forum);
            return _mapper.Map<ForumVM>(result);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.Delete(id);
            return Ok(id);
        }
    }
}
