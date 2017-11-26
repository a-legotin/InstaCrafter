using System.Collections.Generic;
using System.Threading.Tasks;
using InstaCrafter.Web.DataAccess;
using InstaCrafter.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        public readonly IUnitOfWork UnitOfWork;
        public readonly ITaskRunner Runner;

        public UsersController(IUnitOfWork uow, ITaskRunner runner)
        {
            UnitOfWork = uow;
            this.Runner = runner;
        }

        [HttpGet("[action]")]
        public IEnumerable<InstaUser> GetAllUsers()
        {
            return UnitOfWork.UserRepository.GetAll();
        }

        [HttpGet("[action]")]
        public bool StartRunner()
        {
            var res = Task.Run(() => Runner.Run()).Result;
            return res;
        }
    }
}
