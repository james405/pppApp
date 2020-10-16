using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPIMVC.APIClasses;
using WebAPIMVC.Controllers;
using WebAPIMVC.Models;
using System.Net;
using WebAPIMVC.Abstracts;
using System.Web.Http.Cors;
namespace PPP.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";           
            return View();
        }

        public Action StartRequest()
        {

            //currently just a dummy request used for testing.
            //end test

            //VController vals = new VController();
            ////Business b = vals.GetBusinessById(28324);
            //IList<Business> bs = vals.GetBusinessesAsList("ACCENT");

            ////returning a set here will need to be handed by json
            return null;
        }


        //still get a cors error if either program is running in vs2019 debug mode.
        //use browser developer tools instead.
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ActionResult GetBusinessById(int id = 0)
        {
            try
            {
                //need to serialize this object.. getting error.
                VController vals = new VController();
                IList<WebAPIMVC.Models.Business> bs = vals.GetBusinessById(id);
                JsonSerializerSettings jss = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };

                var result = JsonConvert.SerializeObject(bs, Formatting.Indented, jss);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
                       
        }

        public JsonResult GetBusinessesOnlyList(string name = null)
        {

            try
            {
                VController vals = new VController();
                IList<BusinessBase> bsl = vals.GetBusinessesOnlyAsList(name);              
                return Json(bsl, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }

        }

        public JsonResult GetBusinessesList(string name = null)
        {
           
            try
            {
                VController vals = new VController();
                IList<WebAPIMVC.Models.Business> bsl = vals.GetBusinessesAsList(name);
                JsonSerializerSettings jss = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
              
                var result = JsonConvert.SerializeObject(bsl, Formatting.Indented, jss);

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }                      

        }

        public IList<EnumType> GetDataToEnumerate(string type) 
        {
            try
            {
                VController vals = new VController();
                IList<EnumType> items = vals.GetDataToEnumerate<EnumType>(type);
                return items;
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public JsonResult GetStates()
        {

            //get the values
            var tmp = Request["type"].ToString();
            var name = Request["name"].ToString();


            //strip out invalid chars.
            //var idx = tmp.Length;
            //tmp = tmp.Remove(0, 1).Remove(idx-2, 1);
            //idx = name.Length;
            //name = name.Remove(0, 1).Remove(idx - 2, 1);

            //strip out any spaces from the name.
            if(name.Contains(" "))
            {
               name =  name.Replace(" ", "");
            }

            try
            {
                VController vals = new VController();
                IList<WebAPIMVC.APIClasses.State> items = vals.GetStates(tmp, name);
                return Json(items, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
               
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return null;
            }
        }
        //public JsonResult GetBusinessesByDivisionIdAsList(string dName)
        //{
        //    try
        //    {
        //        var id = StateEnumeration.GetOrdinal<T>(dName);

        //        VController vals = new VController();
        //        IList<BusinessBase> bsl = vals.GetBusinessesByDivisionId(id);
        //        return Json(bsl, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
        //        return Json(ex.Message);
        //    }
        //}

    }
}
