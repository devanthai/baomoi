using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnLTW.Controllers
{
    public class BaiVietController : Controller
    {
        // GET: BaiViet
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ViewBaiViet(string ID)
        {
            ViewData["ID"] = ID;
            return View();
        }
        public ActionResult ViewType(string type)
        {
            ViewData["type"] = type;
            return View();
        }
    }
}