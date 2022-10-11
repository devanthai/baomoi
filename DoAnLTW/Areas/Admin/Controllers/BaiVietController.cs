using DoAnLTW.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoAnLTW.Areas.Admin.Controllers
{

    public class BaiVietController : Controller
    {
        [HttpPost, ValidateInput(false)]
        public string DuyetBaiViet(int ID)
        {
            MessageJs messageJs = null;
            try
            {
                BaoMoi2 baoMoi2 = new BaoMoi2();
                var findBv = baoMoi2.BaiViets.Where(bv => bv.ID == ID).FirstOrDefault();
                if (findBv != null)
                {
                    findBv.IsDuyet = true;
                    baoMoi2.SaveChanges();
                    messageJs = new MessageJs { Error = false, Message = "Cập nhật thành công", Data = null };
                    return Helpper.GetJson(messageJs);
                }
                else
                {
                    messageJs = new MessageJs { Error = true, Message = "Không tìm tháy bài viết này", Data = null };
                    return Helpper.GetJson(messageJs);
                }
            }
            catch
            {
                messageJs = new MessageJs { Error = true, Message = "Lỗi không xác định vui lòng thử lại", Data = null };
                return Helpper.GetJson(messageJs);
            }
        }

        [HttpPost, ValidateInput(false)]
        public string PostDeleteBaiViet(int ID)
        {
            MessageJs messageJs = null;
            try
            {
                BaoMoi2 baoMoi2 = new BaoMoi2();
                var findBv = baoMoi2.BaiViets.Where(bv => bv.ID == ID).FirstOrDefault();
                if (findBv != null)
                {
                    baoMoi2.BaiViets.Attach(findBv);
                    baoMoi2.BaiViets.Remove(findBv);
                    baoMoi2.SaveChanges();
                    messageJs = new MessageJs { Error = false, Message = "Xóa Thành công", Data = null };
                    return Helpper.GetJson(messageJs);
                }
                else
                {
                    messageJs = new MessageJs { Error = true, Message = "Không tìm tháy bài viết này", Data = null };
                    return Helpper.GetJson(messageJs);
                }
            }
            catch
            {
                messageJs = new MessageJs { Error = true, Message = "Lỗi không xác định vui lòng thử lại", Data = null };
                return Helpper.GetJson(messageJs);
            }
        }


        [HttpPost, ValidateInput(false)]
        public string PostAddBaiViet(string Title = null, string Noidung = null, int IdTheLoai = 0, HttpPostedFileBase ImagePost = null)
        {
            MessageJs messageJs = null;
            if (Title == null || Noidung == null || IdTheLoai == null || ImagePost == null)
            {
                messageJs = new MessageJs { Error = true, Message = "Vui lòng nhập đủ thông tin", Data = null };
                return Helpper.GetJson(messageJs);
            }
            string pathData = "";
            if (ImagePost.FileName != null)
            {
                pathData = "~/Content/images/thumbnail/" + DateTime.UtcNow.Ticks + ImagePost.FileName;
                string pathsave = Server.MapPath(pathData);
                ImagePost.SaveAs(pathsave);
            }
            BaoMoi2 baoMoi2 = new BaoMoi2();
            var findLoai = baoMoi2.TheLoais.Where(loai => loai.ID == IdTheLoai).FirstOrDefault();
            if (findLoai != null)
            {
                BaiViet baiViet = new BaiViet();
                baiViet.Author = ((UserAdmin)Session["Admin"]).Name;
                baiViet.Content = Noidung;
                baiViet.ImageThumbnail = pathData;
                baiViet.Title = Title;
                baiViet.EntryTime = DateTime.Now;
                baiViet.TypeCode = findLoai.TypeCode;
                baiViet.CountView = 0;
                baoMoi2.BaiViets.Add(baiViet);
                baoMoi2.SaveChanges();
                messageJs = new MessageJs { Error = false, Message = "Thêm bài viết thành công", Data = null };
                return Helpper.GetJson(messageJs);
            }
            else
            {
                messageJs = new MessageJs { Error = true, Message = "Không tìm thấy loại bài viết này", Data = null };
                return Helpper.GetJson(messageJs);
            }
            messageJs = new MessageJs { Error = false, Message = "Thêm bài viết thành công", Data = null };
            return Helpper.GetJson(messageJs);
        }
        public ActionResult AddBaiViet()
        {
            return View();
        }
        public ActionResult ChuaDuyet()
        {
            return View();
        }
        public ActionResult DaDuyet()
        {
            return View();
        }

    }
}