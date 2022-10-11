using System.Web.Mvc;

namespace DoAnLTW.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
             "AdminLogin",
             "Admin/Login",
             new { controller = "Auth", action = "Login", id = UrlParameter.Optional }
         );
            context.MapRoute(
             "Admin_default",
             "Admin/{controller}/{action}/{id}",
             new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
             namespaces:new[] {"DoAnLTW.Areas.Admin.Controllers"}
         );

            
        }
    }
}