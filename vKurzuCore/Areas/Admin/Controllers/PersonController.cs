using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vKurzuCore.Data;
using vKurzuCore.Helpers;
using vKurzuCore.ViewModels.Admin;

namespace vKurzuCore.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Roles.Admin)]
    [Area(Constants.Roles.Admin)]
    [Route("admin/[controller]")]
    public class PersonController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly vKurzuDbContext _context;
        public PersonController(UserManager<IdentityUser> userManager,
            vKurzuDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: Person
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new PersonViewModel();
            var admins = new List<IdentityUser>();
            var lectors = new List<IdentityUser>();
            var users = new List<IdentityUser>();
            foreach (var user in _context.Users)
            {
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Admin))
                {
                    admins.Add(user);
                }
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.Lector))
                {
                    lectors.Add(user);
                }
                if (await _userManager.IsInRoleAsync(user, Constants.Roles.User))
                {
                    users.Add(user);
                }
            }

            viewModel.Admins = admins;
            viewModel.Lectors = lectors;
            viewModel.Users = users;

            return View(viewModel);
        }
   
        [Route("tolector/{id}")]
        public async Task<IActionResult> ToLector(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound();
            await _userManager.RemoveFromRoleAsync(user, Constants.Roles.User);
            await _userManager.AddToRoleAsync(user, Constants.Roles.Lector);
            return RedirectToAction(nameof(Index));
        }
       
        [Route("touser/{id}")]
        public async Task<IActionResult> ToUser(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound();
            await _userManager.RemoveFromRoleAsync(user, Constants.Roles.Lector);
            await _userManager.AddToRoleAsync(user, Constants.Roles.User);
            return RedirectToAction(nameof(Index));
        }


        // GET: Person/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return NotFound();
            var viewModel = new PersonViewModel()
            {
                Id = user.Id,
                Name = user.Email
            };
            return View(viewModel);
        }

        // POST: Person/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(PersonViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == viewModel.Id);
                if (user == null) return NotFound();
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }
    }
}