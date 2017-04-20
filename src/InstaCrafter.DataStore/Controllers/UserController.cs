using System.Collections.Generic;
using InstaCrafter.Classes.Database;
using InstaCrafter.Providers;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IDataAccessProvider<InstaUserDb> _dataAccessProvider;

        public UserController(IDataAccessProvider<InstaUserDb> dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<InstaUserDb> Get()
        {
            return _dataAccessProvider.GetItems();
        }

        [HttpGet("{id}")]
        public InstaUserDb Get(string name)
        {
            return _dataAccessProvider.Get(name);
        }

        [HttpPost]
        public void Post([FromBody] InstaUserDb post)
        {
            _dataAccessProvider.Add(post);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] InstaUserDb post)
        {
            _dataAccessProvider.Update(id, post);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _dataAccessProvider.Delete(id);
        }
    }
}