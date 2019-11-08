using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vKurzuCore.Helpers;
using vKurzuCore.Models;
using vKurzuCore.Repositories;

namespace vKurzuCore.Controllers.Admin.Svg
{
    [Authorize(Roles = Contants.Admin + "," + Contants.Lector)]
    public class SvgController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SvgController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Svg
        [Route("admin/svg")]
        public ActionResult Index()
        {
            return View("/Views/Admin/Svg/Index.cshtml", _unitOfWork.Svgs.GetAll().ToList()); ;
        }

        // GET: Svg/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Svg/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Svg/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Models.Svg svg)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(svg);
                }
                var exist = _unitOfWork.Svgs.Find(c => c.Name == svg.Name);

                if (exist != null)
                {
                    ModelState.AddModelError("svg.Name", "Zadany nazev již existuje");
                    return View(svg);
                }
                _unitOfWork.Svgs.Add(svg);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Svg/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Svg/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Svg/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Svg/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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