using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Tasks.Core.Interfaces.UseCases;
using InstaCrafter.Tasks.Models;
using InstaCrafter.Tasks.Presenters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Tasks.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/ig/account")]
    public class AccountTasksController : ControllerBase
    {
        private readonly ILogger<AccountTasksController> _logger;
        private readonly IAddIgAccountUseCase _addAccountUseCase;
        private readonly AddIgAccountPresenter _addAccountPresenter;

        public AccountTasksController(ILogger<AccountTasksController> logger, IAddIgAccountUseCase addAccountUseCase, AddIgAccountPresenter addAccountPresenter)
        {
            _logger = logger;
            _addAccountUseCase = addAccountUseCase;
            _addAccountPresenter = addAccountPresenter;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Models.AddIgAccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _addAccountUseCase.Handle(new Tasks.Core.Dto.UseCaseRequests.AddIgAccountRequest(request.Username, request.Password), _addAccountPresenter);
            return _addAccountPresenter.ContentResult;
        }
    }
}