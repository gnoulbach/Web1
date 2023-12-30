using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnBuoi1.Areas.admin.Controllers;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;
using static System.Net.Mime.MediaTypeNames;

namespace OnBuoi1.Areas.client.Controllers
{
    [Area("client")]
    public class trangchuController : Controller
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
        public JsonResult ShowList(string name = "", int index = 1, int size = 7)
        {

            sanphamDAO sanpham = new sanphamDAO();
            int totalp = 0;

            var query = sanpham.Search(out totalp, name, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<div class='col-md-3 p-1'>";
                text += "<div class='card' style='height: 100%; display: flex; flex-direction: column; justify-content: space-between; background: linear-gradient(30deg, #33CCFF, #99FF33); border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); transition: transform 0.3s ease; border: 1px solid #ccc;'>";
                text += " <img src='" + item.Image + "' class='card-img-top' style='object-fit: cover; height: 200px; width: 100%;'>";
                text += " <div class='card-body'>";

                text += "<h5 class='card-title' style='font-size: 18px; font-weight: bold; color: #333;'>" + item.Name + "</h5>";
                text += "<p class='card-text' style='font-size: 14px; color: #555;'>" + item.Price + " $ </p>";


                text +=
                    "<a href='javacript:void(0)' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.Idp + "'><i class='fas fa-eye'></i> xem chi tiet <i class='far fa-eye'></i></a>"; // Thêm thẻ <i> cho biểu tượng con mắt
                text += "</div>";
                text += "</div>";
                text += "</div>";





            }
            string page = Support.InTrang(totalp, index, size);
            return Json(new { data = text, page });
        }
        public JsonResult getSanpham(int id)
        {
            sanphamDAO sanpham = new sanphamDAO();

            var query = sanpham.getItemView(id);
            return Json(new { data = query });
        }




    }
}
