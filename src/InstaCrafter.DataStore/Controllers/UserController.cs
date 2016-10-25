using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;
using InstaCrafter.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IDataAccessProvider<InstaUser> _dataAccessProvider;
        public UserController(IDataAccessProvider<InstaUser> dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<InstaUser> Get()
        {
            return _dataAccessProvider.GetItems();
        }

        [HttpGet("{id}")]
        public InstaUser Get(int id)
        {
            return _dataAccessProvider.Get(id);
        }

        [HttpPost]
        public void Post([FromBody]InstaUser post)
        {
            _dataAccessProvider.Add(post);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]InstaUser post)
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
