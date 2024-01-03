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
        public JsonResult ShowList(string name = "", int index = 1, int size = 100)
        {

            sanphamDAO sanpham = new sanphamDAO();
            int totalp = 0;

            var query = sanpham.Search(out totalp, name, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<div class='col-lg-2 col-md-12 col-sm-12 mb-3'>"; // Set padding to 0
                text += "<div class='card mb-3' style='height: 360px; width: 200px; border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); transition: transform 0.3s ease; border: 1px solid #ccc;'>"; // Added margin bottom class mb-3

                // Adjusted the image style to maintain its aspect ratio
                text += " <img src='" + item.Image + "' class='card-img-top' style='object-fit: cover; height: 200px; border-top-left-radius: 8px; border-top-right-radius: 8px;'>";

                text += " <div class='card-body' style='padding: 8px;'>";
                text += "<h6 class='card-title' style='font-size: 14px; font-weight: bold; color: #333; margin-bottom: 8px;'>" + item.Name + "</h6>";
                text += "<p class='card-text' style='font-size: 23px; margin-bottom: 8px; color:red;font-weight: bold;'>" + item.Price + " <span style='font-weight: bold;'>$</span></p>";

                text += "<a href='javascript:void(0)' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.Idp + "' style='font-size: 12px; color: #333; text-decoration: none;'>Xem chi tiết <i class='far fa-eye'></i></a>";
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
