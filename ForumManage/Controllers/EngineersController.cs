using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ForumManage.Models;
using ForumManage.Models.ViewModels;
using ForumManage.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EngineerManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineersController : Controller
    {
        private readonly IRepository<Engineer> _repository;
        private readonly IMapper _mapper;

        public EngineersController(IRepository<Engineer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        // GET api/Engineers
        [HttpGet]
        public ActionResult<List<Engineer>> Get()
        {
            return _repository.GetAll();

        }

        // GET api/Engineers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Engineer>> GetById(long id)
        {
            var Engineer = await _repository.GetById(id);
            if (Engineer == null)
            {
                return NotFound();
            }

            return Engineer;
        }

        // POST api/Engineers
        [HttpPost]
        public async Task<ActionResult<EngineerVM>> Post([FromForm] EngineerVM EngineerVM)
        {
            if (EngineerVM.Image != null)
            {
                byte[] photo = null;
                using (var memoryStream = new MemoryStream())
                {
                    await EngineerVM.Image.CopyToAsync(memoryStream);
                    photo = memoryStream.ToArray();
                }

                Engineer Engineer = _mapper.Map<Engineer>(EngineerVM);
                Engineer.Photo = photo;
                Engineer result = await _repository.Add(Engineer);
                return _mapper.Map<EngineerVM>(result);

            }
            else
            {
                return EngineerVM;
            }          
        }

        // PUT api/Engineers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EngineerVM>> Put(long id, [FromBody] EngineerVM EngineerVM)
        {
            //if (id != EngineerVM.Id)
            //{
            //    return BadRequest();
            //}
            Engineer Engineer = _mapper.Map<Engineer>(EngineerVM);
            Engineer result = await _repository.Update(id, Engineer);
            return _mapper.Map<EngineerVM>(result);
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