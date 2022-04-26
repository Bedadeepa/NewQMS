using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QMS.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<TokenRegistration> _TokenRegistration = null;
       
        public HomeController()
        {
            this._TokenRegistration = new Repository<TokenRegistration>();
        }
        
        public ActionResult Index()
        {
            ViewData["TokenData"] = _TokenRegistration.SelectAll().ToList();
           
            return View();
        }

        public ActionResult CounterReport()
        {
            ViewData["TokenData"] = _TokenRegistration.SelectAll().Where(x => x.IsActive == true /*&& x.Date == System.DateTime.Today*/).ToList();
           return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();

        }

    }
}
           
    
    

