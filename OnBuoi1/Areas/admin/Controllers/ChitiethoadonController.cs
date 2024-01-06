using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;

namespace OnBuoi1.Areas.admin.Controllers
{
    public class ChitiethoadonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Create(int idp, int idh, int price, int quantity)
        {

            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            Chitiethoadon item = new Chitiethoadon();
            item.Idp = idp;
            item.Idh = idh;
            item.Price = price;
            item.Quantity = quantity;
            chitiethoadon.InsertOrUpdate(item);
            return Json(new { mess = "Thêm sản phẩm thành công" });
        }
        public JsonResult ShowList(int id)
        {

            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();


            var query = chitiethoadon.Search(id);
            string text = "";

            foreach (var item in query)
            {
                text += "<tr>";
                text += "<td>" + item.Idct + "</td>";
                text += "<td>" + item.Idh + "</td>";
                text += "<td>" + item.Name + "</td>";
                text += "<td>" + item.Price + "</td>";
                text += "<td>" + item.Quantity + "</td>";
                text += "<td>" + item.Total + "</td>";

                text += "<td> <a href='/Admin/chitiethoadon/Edit/" + item.Idct + "'><i class='fa fa-edit' aria-hidden='true'></i></a>";
                text += " <a href='/Admin/chitiethoadon/Delete/" + item.Idct + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
                text += "</tr>";
            }

            return Json(new { data = text });
        }
        public ActionResult Delete(int id)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            x.Detele(id);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            if (id == null) return RedirectToAction("Index");
            return View(x.getItemView(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(chitiethoadonVIEW model)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            var item = x.getItem(model.Idct);
            item.Idp = model.Idp;
            item.Idh = model.Idh;
            item.Price = model.Price;
            item.Quantity = model.Quantity;
            x.InsertOrUpdate(item);
            return RedirectToAction("Index");
        }
    }
}
