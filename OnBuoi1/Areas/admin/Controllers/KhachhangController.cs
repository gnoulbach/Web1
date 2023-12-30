using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;
using static System.Net.Mime.MediaTypeNames;

namespace OnBuoi1.Areas.admin.Controllers
{
    [Area("admin")]
    public class KhachhangController : Controller
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
        public JsonResult Create(string name, int age, string phone, string address, string username,string password , string image)
        {

            khachhangDAO khachhang = new khachhangDAO();
            Khachhang item = new Khachhang();
            item.Name = name;
            item.Age = age;
            item.Phone = phone;
            item.Address = address;
            item.Username = username;
            item.Password = password;
            item.Image = image;
            khachhang.InsertOrUpdate(item);
            return Json(new { mess = "Them khach hang thanh cong" });
        }
        public JsonResult Update(int idc, string name, int age, string phone, string address, string username, string password, string image)
        {

            khachhangDAO khachhang = new khachhangDAO();
            var item = khachhang.getItem(idc);
            item.Name = name;
            item.Age = age;
            item.Phone = phone;
            item.Address = address;
            item.Username = username;
            item.Password = password;
            item.Image = image;
            Khachhang.InsertOrUpdate(item);
            return Json(new { mess = "Chinh sua khach hang thanh cong" });
        }
        public JsonResult ShowList(string name = "", int index = 1, int size = 7)
        {

            khachhangDAO khachhang = new khachhangDAO();
            int totalp = 0;

            var query = khachhang.Search(out totalp, name, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.Idc + "</td>";
                text += "<td>" + item.Name + "</td>";
                text += "<td>" + item.Age + "</td>";
                text += "<td>" + item.Phone + "</td>";
                text += "<td>" + item.Address + "</td>";
                text += "<td>" + item.Username + "</td>";
                text += "<td>" + item.Password + "</td>";
                text += "<td><img src='" + item.Image + "' alt='Hình ảnh khach hang' style='width: 50px; height: 50px; border-radius: 50%; object-fit: cover;'></td>";


                text += "<td>" +
                    "<a href='javacript:void(0)'  data-toggle='modal' data-target='#update' data-whatever='" + item.Idc + "'><i class='fa fa-edit'></i></a>" + "</td>";

                text += " <td>" + "<a href = 'javacript:void(0)'  onclick='khachhang.delete(" + item.Idc + ")' ><i class='fa fa-trash'></i></a>" + "</td>";

                text += "</tr>";
            }
            string page = Support.InTrang(totalp, index, size);
            return Json(new { data = text, page = page });
        }
        public JsonResult getKhachhang(int id)
        {
            khachhangDAO khachhang = new khachhangDAO();

            var query = khachhang.getItemView(id);
            return Json(new { data = query });
        }
        public JsonResult Delete(int id)
        {
            khachhangDAO x = new khachhangDAO();
            x.Detele(id);
            return Json(new { mess = "Xoa khach hang thanh cong" });

        }
        
    }
}
