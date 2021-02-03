using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projects.SeckillFronts.Controllers
{
    /// <summary>
    /// 秒杀商品详情控制器
    /// </summary>
    public class DetailController : Controller
    {
        /// <summary>
        /// 首页展示
        /// </summary>
        /// <param name="seckillId">秒杀编号</param>
        /// <param name="endtime">秒杀时间</param>
        /// <returns></returns>
        public ActionResult Index(int seckillId, string endtime)
        {
            //1.添加页面数据
            ViewData.Add("seckillId", seckillId);
            ViewData.Add("endtime", endtime);
            return View();
        }

        // GET: DetailController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetailController/Create
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

        // GET: DetailController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetailController/Edit/5
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

        // GET: DetailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetailController/Delete/5
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
