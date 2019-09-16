using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.CrafterJobs.DataProvider;
using InstaCrafter.CrafterJobs.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.CrafterJobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IDataAccessProvider<InstaCrafterJobDto> _repository;

        public JobsController(IDataAccessProvider<InstaCrafterJobDto> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstaCrafterJobDto>> Get()
        {
            return _repository.GetItems();
        }

        [HttpGet("{id}")]
        public ActionResult<InstaCrafterJobDto> Get(int id)
        {
            return _repository.Get(id);
        }
    }
}