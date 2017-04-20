using System.Collections.Generic;
using InstaCrafter.Classes.Database;
using InstaCrafter.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IDataAccessProvider<InstaPostDb> _dataAccessProvider;

        public PostController(IDataAccessProvider<InstaPostDb> dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<InstaPostDb> Get()
        {
            return _dataAccessProvider.GetItems();
        }

        [HttpGet("{code}")]
        public InstaPostDb Get(string code)
        {
            return _dataAccessProvider.Get(code);
        }

        [HttpPost]
        public void Post([FromBody] InstaPostDb post)
        {
            _dataAccessProvider.Add(post);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] InstaPostDb post)
        {
            if (id < 0) return;
            post.Id = id;
            _dataAccessProvider.Update(id, post);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dataAccessProvider.Delete(id);
        }
    }
}