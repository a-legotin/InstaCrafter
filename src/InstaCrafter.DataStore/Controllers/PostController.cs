using System.Collections.Generic;
using InstaCrafter.DataAccess.Database;
using InstaCrafter.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IDataAccessProvider<InstaPost> _dataAccessProvider;

        public PostController(IDataAccessProvider<InstaPost> dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<InstaPost> Get()
        {
            return _dataAccessProvider.GetItems();
        }

        [HttpGet("{code}")]
        public InstaPost Get(string code)
        {
            return _dataAccessProvider.Get(code);
        }

        [HttpPost]
        public void Post([FromBody] InstaPost post)
        {
            _dataAccessProvider.Add(post);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] InstaPost post)
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