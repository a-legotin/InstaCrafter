using System.Collections.Generic;
using InstaCrafter.Classes.Database;
using InstaCrafter.DataStore.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.DataStore.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IDataAccessProvider<InstaMediaDb> _dataAccessProvider;

        public PostController(IDataAccessProvider<InstaMediaDb> dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<InstaMediaDb> Get()
        {
            return _dataAccessProvider.GetItems();
        }

        [HttpGet("{code}")]
        public InstaMediaDb Get(string code)
        {
            return _dataAccessProvider.Get(code);
        }

        [HttpPost]
        public void Post([FromBody] InstaMediaDb media)
        {
            _dataAccessProvider.Add(media);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] InstaMediaDb media)
        {
            if (id < 0) return;
            media.Id = id;
            _dataAccessProvider.Update(id, media);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dataAccessProvider.Delete(id);
        }
    }
}