using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TallerJuanMecanico.Models;

namespace TallerJuanMecanico.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Enter(string user, string password)
        {
            try
            {
                password = Helpers.MD5Helper.CalculateMD5Hash(password);
                using (TallerJuanMecanicoEntities db = new TallerJuanMecanicoEntities())
                {
                              var lst = from d in db.Usuarios
                              where d.Email == user && d.Password== password && d.activo == true
                              select d; 
                    if(lst.Count() > 0 )
                    {
                        Usuarios oUser = lst.First();
                        Session["User"] = lst.First();
                        return Content("1");
                    }
                    else
                    {
                        return Content("Credenciales invalidas");
                    }
                }


              
            } catch(Exception ex)
            {
                return Content("Ocurrio un error: ( "+ex.Message);
            }

        }
    }
}