using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;

namespace OnBuoi1.Areas.admin.Controllers
{
    public class Lopchung
    {   
        public int ID { set; get; }
        public string Name { set; get; }
    }
    [Area("admin")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            List<Lopchung> pagesize = new List<Lopchung>();
            pagesize.Add(new Lopchung { ID = 7 });
            pagesize.Add(new Lopchung { ID = 10 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 30 });
            pagesize.Add(new Lopchung { ID = 40 });
            pagesize.Add(new Lopchung { ID = 50 });
            ViewBag.Pagesize = pagesize;
            return View();
        }
        public JsonResult Create(string name, string age, string address)
        {

            userDAO nhanvien = new userDAO();
            User item = new User();
            item.Name = name;
            item.Age = age;
            item.Address = address;
            nhanvien.InsertOrUpdate(item);
            return Json(new { mess = "Them user thanh cong" });
        }
        public JsonResult Update(int id, string name, string age, string address)
        {

            userDAO nhanvien = new userDAO();
            var item = nhanvien.getItem(id);
            item.Name = name;
            item.Age = age;
            item.Address = address;
            nhanvien.InsertOrUpdate(item);
            return Json(new { mess = "Chinh sua user thanh cong" });
        }
        public JsonResult ShowList(string name="", int index = 1, int size = 7)
        {

            userDAO nhanvien = new userDAO();
            int total = 0;

            var query = nhanvien.Search( out total, name, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.Id + "</td>";
                text += "<td>" + item.Name + "</td>";
                text += "<td>" + item.Age + "</td>";
                text += "<td>" + item.Address + "</td>";
                
                text += "<td>" +
                    "<a href='javacript:void(0)'  data-toggle='modal' data-target='#update' data-whatever='" + item.Id + "'><i class='fa fa-edit'></i></a>" + "</td>";

                text += " <td>" + "<a href = 'javacript:void(0)'  onclick='nhanvien.delete(" + item.Id + ")' ><i class='fa fa-trash'></i></a>" + "</td>";
                text += "</tr>";
            }
            string page = Support.InTrang(total, index, size);
            return Json(new { data = text ,page = page });
        }
        public JsonResult getNhanvien(int id)
        {
            userDAO nhanvien = new userDAO();

            var query = nhanvien.getItemView(id);
            return Json(new { data = query });
        }
        public JsonResult Delete(int id)
        {
            userDAO x = new userDAO();
            x.Detele(id);
            return Json(new { mess = "Xoa nhan vien thanh cong" });

        }
    }
}
