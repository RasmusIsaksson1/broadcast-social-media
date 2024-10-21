using BroadcastSocialMedia.Data;
using BroadcastSocialMedia.Models;
using BroadcastSocialMedia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BroadcastSocialMedia.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userMagager, ApplicationDbContext dbContext)
		{
			_logger = logger;
			_userManager = userMagager;
			_dbcontext = dbContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		public async Task<IActionResult> Broadcast(HomeBroadcastViewModel viewModel)
		{
			var user = await _userManager.GetUserAsync(User);
			var broadcast = new Broadcast()
			{
				Message = viewModel.Message,
				User = user
			};

			_dbcontext.Broadcasts.Add(broadcast);

			await _dbcontext.SaveChangesAsync();

			return Redirect("/");
		}
	}
}
