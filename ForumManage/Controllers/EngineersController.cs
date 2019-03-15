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
            var engineer = await _repository.GetById(id);
            if (engineer == null)
            {
                return NotFound();
            }

            return engineer;
        }

        // POST api/Engineers
        [HttpPost]
        public async Task<ActionResult<EngineerVM>> Post([FromForm] EngineerVM engineerVM)
        {
            if (engineerVM.Image != null)
            {
                byte[] photo = null;
                using (var memoryStream = new MemoryStream())
                {
                    await engineerVM.Image.CopyToAsync(memoryStream);
                    photo = memoryStream.ToArray();
                }

                Engineer engineer = _mapper.Map<Engineer>(engineerVM);
                engineer.Photo = photo;
                Engineer result = await _repository.Add(engineer);
                return _mapper.Map<EngineerVM>(result);

            }
            else
            {
                return engineerVM;
            }          
        }

        // PUT api/Engineers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<EngineerVM>> Put(long id, [FromForm] EngineerVM engineerVM)
        {
            if (engineerVM.Image != null)
            {
                byte[] photo = null;
                using (var memoryStream = new MemoryStream())
                {
                    await engineerVM.Image.CopyToAsync(memoryStream);
                    photo = memoryStream.ToArray();
                }

                var entity = await _repository.GetById(id);

                Engineer engineer = _mapper.Map<Engineer>(engineerVM);
                engineer.Id = id;
                engineer.Photo = photo;
                engineer.AddedDate = entity.AddedDate;
                engineer.ModifiedDate = entity.ModifiedDate;
                engineer.IsDeleted = entity.IsDeleted;

                Engineer result = await _repository.Update(id, engineer);
                return _mapper.Map<EngineerVM>(result);

            }
            else
            {
                return engineerVM;
            }
           
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