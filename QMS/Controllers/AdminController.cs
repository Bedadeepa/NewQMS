using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Script.Serialization;

namespace QMS.Controllers
{
    public class AdminController : BaseController
    {
        private IRepository<TokenRegistration> _TokenRegistration = null;
        private IRepository<DepartmentMaster> _DepartmentMaster = null;
        private IRepository<CounterMaster> _CounterMaster = null;
        //static JavaScriptSerializer _ser = new JavaScriptSerializer();
        public AdminController()
        {
            this._TokenRegistration = new Repository<TokenRegistration>();
            this._DepartmentMaster = new Repository<DepartmentMaster>();
            this._CounterMaster = new Repository<CounterMaster>();

        }

        // GET: Admin
        public ActionResult UserPanel()
        {
            if (GetUser() != null && _userIdentity.RoleID == 0)
            {
                UpdateLogger(_userIdentity.UserId, "Logged by mobileno- " + _userIdentity.UserMobile.ToString());
                ViewData["DepartmentList"] = _DepartmentMaster.SelectAll().Select(x => new DepartmentMaster { DeptID = x.DeptID, DepartmentName = x.DepartmentName }).ToList();
                ViewData["CounterList"] = _CounterMaster.SelectAll().Select(x => new CounterMaster { ID = x.ID, CounterName = x.CounterName }).ToList();

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Registration");
            }
        }
        #region  Token Registration
        public ActionResult TokenRegistration()
        {
            ViewData["DepartmentList"] = _DepartmentMaster.SelectAll().Select(x => new DepartmentMaster { DeptID = x.DeptID, DepartmentName = x.DepartmentName }).ToList();
            ViewData["CounterList"] = _CounterMaster.SelectAll().Select(x => new CounterMaster { ID = x.ID, CounterName = x.CounterName }).ToList();
            return View();

        }
        
        [HttpPost]
        public ActionResult TokenRegistrationSave(TokenRegistration model)
       
        {
            if (GetUser() != null)
                {
                TokenRegistration reg = new TokenRegistration();
                reg.Name = model.Name;
                reg.CounterNumberID = model.CounterNumberID;
                reg.TokenNumber = GetTokenNo(model.CounterNumberID).ToString();
                reg.Date = System.DateTime.Now;
                reg.DepartmentID = model.DepartmentID;
                reg.Gender = model.Gender;
                reg.Mobile = model.Mobile;
                reg.Payment = model.Payment;
                reg.Age = model.Age;
                reg.IsActive = true;
                _TokenRegistration.Add(reg);
                _TokenRegistration.Save();
                return Json("Successfully Updated^"+reg.TokenNumber.ToString());
                
            }
            
            return View();

        }
        [HttpPost]
        public ActionResult TokenDetailsupdate( int tokenId)
        {
            TokenRegistration reg = _TokenRegistration.SelectAll().Where(x => x.Id == tokenId).FirstOrDefault();
            reg.IsActive = false;
            _TokenRegistration.Save();
            return null;
        }
        public  string GetTokenNo(int CounterID)
        {
           int TokenNo= _TokenRegistration.SelectAll().Where(x => x.CounterNumberID == CounterID && x.Date.ToShortDateString() == DateTime.Now.ToShortDateString()).Count()+1;
            string NewToken = "";
            if (TokenNo.ToString().Length ==1)
            {
                NewToken = "A00" + TokenNo.ToString();
            }
            else if (TokenNo.ToString().Length == 2)
            {
                NewToken = "A0" + TokenNo.ToString();
            }
            else
            {
                NewToken = TokenNo.ToString();
            }
            return NewToken;


        }
        
        public ActionResult CounterReport ()
        {
            var reg = Session["UserDetails"] as Registration;
                ViewData["TokenData"] = _TokenRegistration.SelectAll().Where(x => x.IsActive == true && x.CounterNumberID == reg.CounterID).ToList();
            
            return View();
         
        }
        public ActionResult Counter2Report()
        {
            ViewData["Counter2TokenData"] = _TokenRegistration.SelectAll().Where(x => x.IsActive == true && x.CounterNumberID ==2 ).ToList();

            return View();
        }
        //[HttpPost]
        //public ActionResult CounterReport(int UserId)
        //{

        //    if(UserId == 1)
        //    { ViewData["TokenData"] = _TokenRegistration.SelectAll().Where(x => x.IsActive == true && x.CounterNumberID == 1).ToList();
        //    }
        //    if(UserId == 2)
        //    {
        //        ViewData["TokenData"] = _TokenRegistration.SelectAll().Where(x => x.IsActive == true && x.CounterNumberID == 2).ToList();
        //    }
        //    if (UserId == 3)
        //    {
        //        ViewData["TokenData"] = _TokenRegistration.SelectAll().Where(x => x.IsActive == true && x.CounterNumberID == 3).ToList();
        //    }
        //    if (UserId == 4)
        //    {
        //        ViewData["TokenData"] = _TokenRegistration.SelectAll().Where(x => x.IsActive == true && x.CounterNumberID == 4).ToList();
        //    }

        //    return View();
        //}
        //private static int CalculateAge(DateTime dateOfBirth)
        //{
        //    int age = 0;
        //    age = DateTime.Now.Year - dateOfBirth.Year;
        //    if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
        //        age = age - 1;

        //    return age;
        //}
        #endregion
    }
}