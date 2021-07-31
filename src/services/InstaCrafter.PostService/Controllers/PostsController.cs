using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Models;
using InstaCrafter.PostService.DataProvider;
using InstaCrafter.PostService.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.PostService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IDataAccessProvider<InstagramPostDto> _repository;

        public PostsController(IDataAccessProvider<InstagramPostDto> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstagramPostDto>> Get()
        {
            return _repository.GetItems();
        }

        [HttpGet("{id}")]
        public ActionResult<InstagramPostDto> Get(long id)
        {
            return _repository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] InstagramPostDto post)
        {
            _repository.Add(post);
        }

        [HttpPut("{id}")]
        public void Put(long id, [FromBody] InstagramPostDto post)
        {
            _repository.Update(id, post);
        }

        [HttpDelete("{id}")]
        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}