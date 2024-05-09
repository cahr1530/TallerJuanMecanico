
using System.Web;
using System.Web.Mvc;
using TallerJuanMecanico.Controllers;
using TallerJuanMecanico.Models;

namespace TallerJuanMecanico.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContex)
        {
            var oUser = (Usuarios)HttpContext.Current.Session["User"];

            if (oUser == null)
            {
                if(filterContex.Controller is AccessController ==  false)
                {
                    filterContex.HttpContext.Response.Redirect("~/Access/Index");
                }
            }
            else
            {
                if(filterContex.Controller is AccessController ==true)
                {


                    filterContex.HttpContext.Response.Redirect("~/Home/Index");
                }
            }
            base.OnActionExecuting(filterContex);
        }
        
    }
}