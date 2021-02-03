using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillFronts.Controllers
{
    /// <summary>
    /// 秒杀列表控制器
    /// </summary>
    public class SeckillController : Controller
    {
        // GET: SeckillController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SeckillController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SeckillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeckillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SeckillController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SeckillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SeckillController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SeckillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
