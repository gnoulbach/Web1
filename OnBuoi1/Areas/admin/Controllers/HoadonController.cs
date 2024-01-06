using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;
using System.Collections.Generic;
using System.Linq;

namespace OnBuoi1.Areas.admin.Controllers
{
    
    public class Year
    {
        public int year { set; get; }
    }
    public class Month
    {
        public int month { set; get; }
    }
    [Area("admin")]
    public class HoadonController : Controller
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
        [HttpPost]
        
        public JsonResult ShowList(string name = "", int index = 1, int size = 7)
        {

            hoadonDAO hoadon = new hoadonDAO();
            int totalh = 0;

            var query = hoadon.Search(out totalh, name, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.Idh + "</td>";
                text += "<td>" + item.Name + "</td>";
                text += "<td>" + item.Total + "</td>";
                text += "<td>" + item.Date + "</td>";
                text += "<td>" + item.Phone + "</td>";
                text += "<td>" + item.Address + "</td>";

                text += "<td>" +
                    "<a href='javacript:void(0)' onclick='hoadon.xemchitiet(" + item.Idh + ")' data-toggle='modal' data-target='#xemchitiet' data-whatever='" + item.Idh + "' ><i class='fas fa-eye'></i></a></td>";
                text += "<td> <a href='/Admin/hoadon/Edit/" + item.Idh + "'><i class='fa fa-edit' aria-hidden='true'></i></a>";
                text += " <a href='/Admin/hoadon/Delete/" + item.Idh + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }
            string page = Support.InTrang(totalh, index, size);
            return Json(new { data = text, page = page });
        }
        public ActionResult Edit(int? idh)
        {
            hoadonDAO hoadon = new hoadonDAO();
            if (idh == null) return RedirectToAction("Index");
            return View(hoadon.getItemView(idh.Value));
        }
        [HttpPost]
        public ActionResult Edit(hoadonVIEW model)
        {
            hoadonDAO hoadon = new hoadonDAO();
            var item = hoadon.getItem(model.Idh);
            item.Id = model.Id;
            item.Idc = model.Idc;
            item.Total = model.Total;
            item.Date = model.Date;
            item.Name = model.Name;
            item.Phone = model.Phone;
            item.Address = model.Address;
            hoadon.InsertOrUpdate(item);
            return RedirectToAction("Index");
        }
        public ActionResult Chart()
        {
            // Create a list to store Lopchung objects
            List<Lopchung> pagesize = new List<Lopchung>();

            // Add Lopchung objects to the list with different ID values
            pagesize.Add(new Lopchung { ID = 10 });
            pagesize.Add(new Lopchung { ID = 20 });
            pagesize.Add(new Lopchung { ID = 30 });
            pagesize.Add(new Lopchung { ID = 40 });
            pagesize.Add(new Lopchung { ID = 50 });

            // Add current year and month to ViewBag
            ViewBag.CurrentYear = DateTime.Now.Year;
            ViewBag.CurrentMonth = DateTime.Now.Month;

            // Create a list to store recent years, including the current year
            List<Year> recentYears = new List<Year>();

            // Add the current year to the beginning of the list
            recentYears.Add(new Year { year = DateTime.Now.Year });

            // Specify the number of additional recent years you want to include (e.g., 4 years)
            int numberOfRecentYears = 4;

            // Add recent years to the list
            for (int i = 1; i <= numberOfRecentYears; i++)
            {
                recentYears.Add(new Year { year = DateTime.Now.Year - i });
            }

            // Add the list of recent years to ViewBag
            ViewBag.year = recentYears;

            // Create a list to store months
            List<Month> months = new List<Month>();

            // Add the current month to the beginning of the list
            months.Add(new Month { month = DateTime.Now.Month });

            // Add subsequent months to the list in ascending order
            for (int i = DateTime.Now.Month + 1; i <= 12; i++)
            {
                months.Add(new Month { month = i });
            }

            // Add preceding months to the list in descending order
            for (int i = DateTime.Now.Month - 1; i >= 1; i--)
            {
                months.Add(new Month { month = i });
            }

            // Add the list of months to ViewBag
            ViewBag.Months = months;

            // Add the list of Lopchung objects to ViewBag
            ViewBag.Pagesize = pagesize;

            return View();

        }
        public JsonResult Doanhthu(int id)
        {
            hoadonDAO hoadon = new hoadonDAO();
            var rs = hoadon.Doanhthu(id);
            int imax = rs.Max();
            int[] text = rs.ToArray();
            return Json(new { data = text.ToArray(), max = imax });
        }

        public ActionResult ChartProduct()
        {
            List<Year> years = new List<Year>();
            for (int i = 2020; i < 2030; i++)
            {
                years.Add(new Year { year = i });
            }
            ViewBag.year = years;
            List<Month> month = new List<Month>();
            for (int i = 1; i < 13; i++)
            {
                month.Add(new Month { month = i });
            }
            ViewBag.month = month;
            return View();
        }
        [HttpPost]
        public JsonResult Soluongban(int month, int year)
        {
            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            var rs = chitiethoadon.Soluongban(month, year);
            int[] text = rs.Select(item => item.Quantity.Value).ToArray();
            string[] name = rs.Select(item => item.Name).ToArray();
            int max = text.Any() ? text.Max() : 0;
            return Json(new { data = text, name = name, max = max });
        }


    }
}
