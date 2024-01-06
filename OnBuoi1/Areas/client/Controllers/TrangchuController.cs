using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnBuoi1.Areas.admin.Controllers;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace OnBuoi1.Areas.client.Controllers
{
    [Area("client")]
    public class trangchuController : Controller
    {
        private const string KHACHHANG = "Khachhangs";
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
                text += "<div class='col-lg-2 col-md-4 col-sm-6 mb-3'>"; // Adjusted column sizes for responsiveness
                text += "<div class='card h-100' style='border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border: 1px solid #ccc;'>"; // Added Bootstrap classes for card
                text += "<img src='" + item.Image + "' class='card-img-top rounded-top' style='object-fit: cover; height: 200px; border-radius: 8px 8px 0 0;'>"; // Added Bootstrap classes for image
                text += "<div class='card-body'>";
                text += "<h6 class='card-title mb-2' style='font-size: 14px; font-weight: bold; color: #333;'>" + item.Name + "</h6>"; // Adjusted font size and margin for title
                text += "<p class='card-text mb-2' style='font-size: 23px; color: red; font-weight: bold;'>" + item.Price + " <span style='font-weight: bold;'>$</span></p>"; // Adjusted font size and color for price
                text += "<a href='javascript:void(0)' class='btn btn-sm btn-info' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.Idp + "' style='font-size: 12px; text-decoration: none;'>Xem chi tiết <i class='far fa-eye'></i></a>"; // Added Bootstrap button classes for "Xem chi tiết" button
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

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Dangnhap(string username, string password)
        {
            khachhangDAO khachhang = new khachhangDAO();
            if (khachhang.Login(username, password))
            {
                HttpContext.Session.SetString("KHACHHANG", username);
                return Json(new { mess = "Đăng nhập thành công" ,status = 1});

            }
            return Json(new { mess = "Đăng nhập thất bại",status = 0 });
        }

        public IActionResult CheckLogin()
        {
            string text = "";
            var idkh = HttpContext.Session.GetString("KHACHHANG");
            if (idkh != null)
            {
                text = "<form action='/Client/trangchu/Logout' method='post'><button type='submit' class='logout-button'><i class='fas fa-sign-out-alt'></i></button></form>";
            }
            else
            {
                text = "<form action='/Client/trangchu/Login' method='post'><button type='submit' class='logout-button'><i class='fas fa-sign-in-alt'></i></button></form>";
            }
            return Json(new { data = text });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Client/Trangchu/Login"); // Chuyển hướng đến URL cụ thể
        }




    }
}