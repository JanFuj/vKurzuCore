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
using vKurzuCore.ViewModels;

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

        // GET: Person/Details/5
        [Route("detail/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Person/Create
        [Route("new")]

        public ActionResult New()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new")]
        public ActionResult New(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit/{id}")]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete/{id}")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}