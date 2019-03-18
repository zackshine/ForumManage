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
using Microsoft.AspNetCore.Hosting;

namespace EngineerManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EngineersController : Controller
    {
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IRepository<Engineer> _repository;
        private readonly IMapper _mapper;

        public EngineersController(IRepository<Engineer> repository, IMapper mapper, IHostingEnvironment hostEnv)
        {
            _repository = repository;
            _mapper = mapper;
            _hostingEnv = hostEnv;
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
        public async Task<ActionResult<Engineer>> Post([FromForm] EngineerVM engineerVM)
        {
            if (engineerVM.Image != null)
            {
                var fileName = Path.GetFileName(engineerVM.Image.FileName);
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "images\\Avatars",fileName);

                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await engineerVM.Image.CopyToAsync(fileSteam);
                }
                
                //byte[] photo = null;
                //using (var memoryStream = new MemoryStream())
                //{
                //    await engineerVM.Image.CopyToAsync(memoryStream);
                //    photo = memoryStream.ToArray();
                //}
                Engineer engineer = _mapper.Map<Engineer>(engineerVM);
                engineer.ImageName = fileName;
                var result = await _repository.Add(engineer);
                return result;
            }
            else
            {
                return BadRequest();
            }          
        }

        // PUT api/Engineers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Engineer>> Put(long id, [FromForm] EngineerVM engineerVM)
        {
            if (engineerVM.Image != null)
            {
                var fileName = Path.GetFileName(engineerVM.Image.FileName);
                var filePath = Path.Combine(_hostingEnv.WebRootPath, "images\\Avatars", fileName);
               
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await engineerVM.Image.CopyToAsync(fileSteam);
                }

                var entity = await _repository.GetById(id);

                Engineer engineer = _mapper.Map(engineerVM,entity);
                engineer.ImageName = fileName;

                Engineer result = await _repository.Update(id, engineer);
                return result;
            }
            else
            {
                return BadRequest();
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