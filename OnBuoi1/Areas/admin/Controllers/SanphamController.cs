using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;
using static System.Net.Mime.MediaTypeNames;

namespace OnBuoi1.Areas.admin.Controllers
{
    [Area("admin")]
    public class SanphamController : Controller
    {
        private const string KHACHHANG = "KHACHHANG";
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
        public JsonResult Create(string name, int price, string image , int quantity, string info)
        {

            sanphamDAO sanpham = new sanphamDAO();
            Sanpham item = new Sanpham();
            item.Name = name;
            item.Price = price;
            item.Image = image;
            item.Quantity = quantity;
            item.Info = info;
            sanpham.InsertOrUpdate(item);
            return Json(new { mess = "Them san pham thanh cong" });
        }
        public JsonResult Update(int idp, string name, int price, string image, int quantity, string info)
        {

            sanphamDAO sanpham = new sanphamDAO();
            var item = sanpham.getItem(idp);
            item.Name = name;
            item.Price = price;
            item.Image = image;
            item.Quantity = quantity;
            item.Info = info;
            sanpham.InsertOrUpdate(item);
            return Json(new { mess = "Chinh sua san pham thanh cong" });
        }
        public JsonResult ShowList(string name = "", int index = 1, int size = 7)
        {

            sanphamDAO sanpham = new sanphamDAO();
            int totalp = 0;

            var query = sanpham.Search(out totalp, name, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                
                text += "<td>" + item.Idp + "</td>";
                text += "<td><img src='" + item.Image + "' alt='Hình ảnh sản phẩm' style='width: 50px; height: 50px; border-radius: 50%; object-fit: cover;'></td>";
                text += "<td>" + item.Name + "</td>";
                text += "<td>" + item.Price + "</td>";
                text += "<td>" + item.Quantity + "</td>";
                text += "<td>" + item.Info + "</td>";
                text += "<td>" +
                    "<a href='javacript:void(0)'  data-toggle='modal' data-target='#update' data-whatever='" + item.Idp + "'><i class='fa fa-edit'></i></a>" + "</td>";

                text += " <td>" + "<a href = 'javacript:void(0)'  onclick='sanpham.delete(" + item.Idp + ")' ><i class='fa fa-trash'></i></a>" + "</td>";

                text += "</tr>";
            }
            string page = Support.InTrang(totalp, index, size);
            return Json(new { data = text, page = page });
        }
        public JsonResult getSanpham(int id)
        {
            sanphamDAO sanpham = new sanphamDAO();

            var query = sanpham.getItemView(id);
            return Json(new { data = query });
        }
        public JsonResult Delete(int id)
        {
            sanphamDAO x = new sanphamDAO();
            x.Detele(id);
            return Json(new { mess = "Xoa san pham thanh cong" });
            
        }

        
    }
}
