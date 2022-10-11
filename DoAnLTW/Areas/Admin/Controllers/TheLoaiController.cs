using DoAnLTW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DoAnLTW.Areas.Admin.Controllers
{
    public class TheLoaiController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string GetTheLoai(int ID)
        {
            BaoMoi2 baoMoi2 = new BaoMoi2();
            var theloai = baoMoi2.TheLoais.Where(loai => loai.ID == ID).FirstOrDefault();

            var THELOAI = new TheLoai { ID = ID, TypeName = theloai.TypeName, Note = theloai.Note,TypeCode=theloai.TypeCode };

            return new JavaScriptSerializer().Serialize(THELOAI);
        }

        [HttpPost]
        public string ConfirmChangeTheLoai(TheLoai TheLoaii)
        {
            BaoMoi2 baoMoi2 = new BaoMoi2();
            var theloai = baoMoi2.TheLoais.Where(loai => loai.ID == TheLoaii.ID).FirstOrDefault();
            if (theloai != null)
            {
                theloai.TypeName = TheLoaii.TypeName;
                theloai.Note = TheLoaii.Note;
                baoMoi2.SaveChanges();
            }

            var THELOAI = new TheLoai { ID = theloai.ID, TypeName = theloai.TypeName, Note = theloai.Note, TypeCode = theloai.TypeCode };
            return new JavaScriptSerializer().Serialize(THELOAI);
        }

        [HttpPost]
        public string ChangeTheLoai(int ID)
        {
            BaoMoi2 baoMoi2 = new BaoMoi2();
            var theloai = baoMoi2.TheLoais.Where(loai => loai.ID == ID);
            return new JavaScriptSerializer().Serialize(theloai);
        }
        [HttpPost]
        public string XoaTheLoai(int ID, string TypeCode)
        {
            MessageJs messageJs = null;
            try
            {
                BaoMoi2 baoMoi2 = new BaoMoi2();
                var baiviets = FuncDB.GetBaiVietByLoai(TypeCode);
                foreach (var baiviet in baiviets)
                {
                    var bv = new BaiViet { ID = baiviet.ID };
                    baoMoi2.BaiViets.Attach(bv);
                    baoMoi2.BaiViets.Remove(bv);
                    baoMoi2.SaveChanges();
                }
                var loai = new TheLoai { ID = ID, TypeCode = TypeCode };
                baoMoi2.TheLoais.Attach(loai);
                baoMoi2.TheLoais.Remove(loai);
                baoMoi2.SaveChanges();
            }
            catch (Exception ex)
            {
                messageJs = new MessageJs { Error = true, Message = "Xóa không thành công " + ex.ToString(), Data = null };
                return Helpper.GetJson(messageJs);
            }
            messageJs = new MessageJs { Error = false, Message = "Xóa thành công", Data = null };
            return Helpper.GetJson(messageJs);
        }
        [HttpPost]
        public string PostAddTheLoai(string NoteLoai = null, string NameType = null, string TypeCode = null)
        {
            MessageJs messageJs = null;
            if (NoteLoai == null || NameType == null || TypeCode == null)
            {
                messageJs = new MessageJs { Error = true, Message = "Vui lòng nhập đủ thông tin", Data = null };
                return Helpper.GetJson(messageJs);
            }
            try
            {
                //Bỏ kí tự
                TypeCode = Regex.Replace(TypeCode, "[@,\\.\";'\\\\]", string.Empty);
                //Bỏ dấu tiếng việt
                TypeCode = Helpper.RemoveUnicode(TypeCode);
                TheLoai theLoai = new TheLoai();
                theLoai.Note = NoteLoai;
                theLoai.TypeName = NameType;
                theLoai.TypeCode = TypeCode;
                BaoMoi2 baoMoi2 = new BaoMoi2();
                baoMoi2.TheLoais.Add(theLoai);
                baoMoi2.SaveChanges();
                messageJs = new MessageJs { Error = false, Message = "Thêm thành công " + theLoai.TypeName, Data = theLoai };
                return Helpper.GetJson(messageJs);
            }
            catch (Exception ex)
            {
                messageJs = new MessageJs { Error = true, Message = ex.ToString(), Data = null };
                return Helpper.GetJson(messageJs);
            }
        }
    }
}