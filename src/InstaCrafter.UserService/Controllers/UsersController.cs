using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Classes.Models;
using InstaCrafter.UserService.DataProvider;
using InstaCrafter.UserService.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDataAccessProvider<InstagramUserDto> _repo;

        public UsersController(IDataAccessProvider<InstagramUserDto> _repo)
        {
            this._repo = _repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstagramUser>> Get()
        {
            return new ActionResult<IEnumerable<InstagramUser>>(_repo.GetItems().Select(Mapper.Map<InstagramUser>));
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "user #1";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}