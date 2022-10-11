using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnLTW.Models;
namespace DoAnLTW.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return Redirect("/Admin/Login");
           
        }
        [HttpPost]
        public string PostLogin(UserAdmin User)
        {

            MessageJs messageJs = null;
            try
            {
                string hPassword = Helpper.HashSha256(User.Password);
                BaoMoi2 baoMoi2 = new BaoMoi2();
                var user = baoMoi2.UserAdmin.Where(a => a.Username == User.Username).FirstOrDefault();
                if (user == null)
                {
                    messageJs = new MessageJs { Data = null, Message = "Thông tin tài khoản hoặc mật khẩu không chính xác!", Error = true };
                    return Helpper.GetJson(messageJs);
                }
                else if (hPassword.Equals(user.Password))
                {
                    Session["Admin"] = user;
                    messageJs = new MessageJs { Data = null, Message = "Đăng nhập thành công!", Error = false };
                    return Helpper.GetJson(messageJs);
                }
                else
                {

                    messageJs = new MessageJs { Data = null, Message = "Thông tin tài khoản hoặc mật khẩu không chính xác!", Error = true };
                    return Helpper.GetJson(messageJs);
                }
            }
            catch (Exception ex)
            {
                messageJs = new MessageJs { Data = null, Message = "Lỗi không xác định vui lòng thử lại!", Error = true };
                return Helpper.GetJson(messageJs);
            }
            return "";
        }
    }
}