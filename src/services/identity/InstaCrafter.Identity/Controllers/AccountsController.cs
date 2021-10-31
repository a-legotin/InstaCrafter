using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Identity.Data;
using InstaCrafter.Identity.Helpers;
using InstaCrafter.Identity.Models.Entities;
using InstaCrafter.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Identity.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountsController(UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            await _appDbContext.Customers.AddAsync(new Customer {IdentityId = userIdentity.Id});
            await _appDbContext.SaveChangesAsync();

            return new OkObjectResult("Account created");
        }
    }
}