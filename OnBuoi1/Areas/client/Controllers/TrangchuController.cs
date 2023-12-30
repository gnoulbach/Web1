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
                text += "<tr>";
                text += "<td>" + item.Idp + "</td>";
                text += "<td>" + item.Name + "</td>";
                text += "<td>" + item.Price + "</td>";
                text += "<td><img src='" + item.Image + "' alt='Hình ảnh sản phẩm' style='width: 50px; height: 50px; border-radius: 50%; object-fit: cover;'></td>";


                text += "<td>" +
                    "<a href='javacript:void(0)'  data-toggle='modal' data-target='#update' data-whatever='" + item.Idp + "'><i class='fa fa-edit'></i></a>" + "</td>";

                text += " <td>" + "<a href = 'javacript:void(0)'  onclick='sanpham.delete(" + item.Idp + ")' ><i class='fa fa-trash'></i></a>" + "</td>";

                text += "</tr>";
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
