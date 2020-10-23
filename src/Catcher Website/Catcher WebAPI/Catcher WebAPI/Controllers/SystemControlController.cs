using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Catcher_WebAPI.Data.CanvasData;
using Catcher_WebAPI.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp.Extensions;

namespace Catcher_WebAPI.Areas.APITester.Controllers
{
    public class SystemControlController : Controller
    {
        private readonly GoCanvasDbContext _context;

        public SystemControlController(GoCanvasDbContext context)
        {
            _context = context;
        }
        // GET: SystemControlController
        [HttpGet]
        public ActionResult Index()
        {
            if (!_context.SystemControls.Any())
            {
                _context.SystemControls.Add(new SystemControl() {Status = 0});
                _context.SaveChanges();
            }
            
            ViewBag.Status = _context.SystemControls.First().Status;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "SuperUser")]
        public ActionResult Index(int ManualSetStatusTo)
        {
            string tblName = typeof(SystemControl).GetAttribute<TableAttribute>().Name;
            if (Request.Form["toggle"] == "0")
            {
                _context.Database.ExecuteSqlRaw($"UPDATE [{tblName}] SET [Status] = 1");
            }
            else
            {
                _context.Database.ExecuteSqlRaw($"UPDATE [{tblName}] SET [Status] = 0");
            }

            _context.SaveChanges();
            _context.SystemControls.Load();
            var systemState = _context.SystemControls.First();
            ViewBag.Status = systemState.Status;
            return View();
        }

        //// GET: SystemControlController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: SystemControlController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SystemControlController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SystemControlController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: SystemControlController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: SystemControlController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: SystemControlController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
