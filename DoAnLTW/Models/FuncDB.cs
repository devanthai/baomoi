using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAnLTW.Models
{
    public class FuncDB
    {

        public static BaoMoi2 baoMoiDB = new BaoMoi2();


        public static List<BaiViet> GetBaiVietCounts( int count)
        {
            List<BaiViet> lists = new List<BaiViet>();
            lists = baoMoiDB.Set<BaiViet>().Where(x=>x.IsDuyet == true).Take(count).OrderByDescending(z => z.CountView).ToList<BaiViet>();
            return lists;
        }
        public static List<BaiViet> GetAllBaiViets()
        {
            List<BaiViet> lists = new List<BaiViet>();
            lists = baoMoiDB.Set<BaiViet>().ToList<BaiViet>();
            return lists;
        }
        public static List<BaiViet> GetAllBaiVietDaDuyet()
        {
            List<BaiViet> lists = new List<BaiViet>();
            lists = baoMoiDB.Set<BaiViet>().Where(x => x.IsDuyet == true).ToList<BaiViet>();
            return lists;
        }
        public static List<BaiViet> GetAllBaiVietChuaDuyet()
        {
            List<BaiViet> lists = new List<BaiViet>();
            lists = baoMoiDB.Set<BaiViet>().Where(x => x.IsDuyet == false).ToList<BaiViet>();
            return lists;
        }


        public static List<BaiViet> GetBaiVietNew()
        {
            List<BaiViet> lists = new List<BaiViet>();         
            lists = baoMoiDB.Set<BaiViet>().Where(x=> x.IsDuyet == true).Take(3).OrderByDescending(z=>z.ID).ToList<BaiViet>();
            return lists;
        }
        public static List<TheLoai> GetTheLoais()
        {
            List<TheLoai> lists = new List<TheLoai>();          
            lists = baoMoiDB.Set<TheLoai>().ToList<TheLoai>();
            return lists;
        }
        public static List<BaiViet> GetBaiVietLoaiCount(string type,int count)
        {
            List<BaiViet> lists = new List<BaiViet>();
            lists = baoMoiDB.Set<BaiViet>().Where(x => x.TypeCode == type && x.IsDuyet == true).Take(count).OrderByDescending(z => z.ID).ToList<BaiViet>();
            return lists;
        }
        public static List<BaiViet> GetBaiVietByLoai(string type)
        {
            List<BaiViet> lists = new List<BaiViet>();       
            lists = baoMoiDB.Set<BaiViet>().Where(x => x.TypeCode == type && x.IsDuyet==true).ToList<BaiViet>();
            return lists;
        }
        public static BaiViet GetBaiVietByID(int ID)
        {
            var baiviet = new BaiViet();         
            baiviet = baoMoiDB.Set<BaiViet>().Where(x => x.ID == ID && x.IsDuyet == true).FirstOrDefault();

            if(baiviet!=null)
            {
                baiviet.CountView += 1;
                baoMoiDB.SaveChanges();
            }

            return baiviet;
        }
        public static List<BaiViet> GetBaiVietsChuaDuyet()
        {
            List<BaiViet> lists = new List<BaiViet>();       
            lists = baoMoiDB.Set<BaiViet>().Where(bv=>bv.IsDuyet == false).ToList<BaiViet>();
            return lists;
        }

        public static List<BaiViet> GetBaiVietsDaDuyet()
        {
            List<BaiViet> lists = new List<BaiViet>();       
            lists = baoMoiDB.Set<BaiViet>().Where(bv => bv.IsDuyet == true).ToList<BaiViet>();
            return lists;
        }

        public static Setting GetSetting()
        {
            BaoMoi2 baoMoiDB = new BaoMoi2();
            var setting = baoMoiDB.Setting.FirstOrDefault();
            return setting;
        }
    }
}