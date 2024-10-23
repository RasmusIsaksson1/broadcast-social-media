using BroadcastSocialMedia.Data;
using BroadcastSocialMedia.Models;
using BroadcastSocialMedia.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace BroadcastSocialMedia.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
		private readonly UserManager<ApplicationUser> _userManager;


		public UsersController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbcontext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(UsersIndexViewModel viewModel)
        {
            if(viewModel.Search != null)
            {
                var users = await _dbcontext.Users.Where(u => u.Name.Contains(viewModel.Search))
                                .ToListAsync();
                viewModel.Result = users;
            }
            

            return View(viewModel);
        }

        [Route("/Users/{id}")]
        public async Task<IActionResult> ShowUser(string id)
        {
            var broadcasts = await _dbcontext.Broadcasts.Where(b => b.User.Id == id)
                .OrderByDescending(b => b.Published)
                .ToListAsync();
            var user = await _dbcontext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            var viewModel = new UsersShowUserViewModel
            {
                Broadcasts = broadcasts,
                User = user
            };

            return View(viewModel);
        }

        [HttpPost, Route("/Users/Listen")]
        public async Task<IActionResult> ListenToUser(UsersListenToUserViewModel viewModel)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);
            var userToListenTo = await _dbcontext.Users.Where(u => u.Id == viewModel.UserId)
                .FirstOrDefaultAsync();

            loggedInUser.ListeningTo.Add(userToListenTo);

            await _userManager.UpdateAsync(loggedInUser);
            await _dbcontext.SaveChangesAsync();

            return Redirect("/");
        }
    }
}
