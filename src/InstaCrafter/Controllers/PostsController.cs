using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;
        public PostsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<InstaPost> Get()
        {
            return _dataAccessProvider.GetPosts();
        }

        [HttpGet("{id}")]
        public InstaPost Get(int id)
        {
            return _dataAccessProvider.GetPost(id);
        }

        [HttpPost]
        public void Post([FromBody]InstaPost post)
        {
            _dataAccessProvider.AddPost(post);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]InstaPost post)
        {
            _dataAccessProvider.UpdatePost(id, post);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dataAccessProvider.DeletePost(id);
        }
    }
}
