using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaBackup.DataAccess;
using InstaBackup.DataAccess.Repository;
using InstaBackup.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstaBackup.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        public readonly IUnitOfWork UnitOfWork;
        public readonly ITaskRunner Runner;

        public SampleDataController(IUnitOfWork uow, ITaskRunner runner)
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
