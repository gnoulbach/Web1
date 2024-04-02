﻿using GUIs.Support;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnBuoi1.Areas.admin.Controllers;
using OnBuoi1.Models.DAO;
using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;
using System;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Runtime.Intrinsics.Arm;
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

            
            var idkh = HttpContext.Session.GetString("KHACHHANG");
            string link = "<a href=\"/client/Trangchu/Login/\" class=\"btn btn-primary\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Đăng nhập vào tài khoản của bạn\" style=\"background: linear-gradient(40deg,#CC00FF, #FFCC00); color: #fff; text-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);\">\r\n  <i class=\"fas fa-sign-in-alt\"></i> Đăng nhập\r\n</a>\r\n";
            if (idkh != null)
            {
                link = "<p id='ten'> </p>";
                link += "<a href=\"/Client/trangchu/Logout\" method=\"post\" class=\"btn btn-danger logout-button\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"Đăng xuất\">\r\n  <i class=\"fas fa-sign-out-alt\"></i> Đăng xuất\r\n</a>";
            }
            ViewBag.Login = link;
            return View();
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

        public JsonResult ShowList(string name = "", int index = 1, int size = 100)
        {

            sanphamDAO sanpham = new sanphamDAO();
            int totalp = 0;

            var query = sanpham.Search(out totalp, name, index, size);
            string text = "";

            foreach (var item in query)
            {
                text += "<div class='col-lg-2 col-md-4 col-sm-6 mb-3' >"; 
                text += "<div class='card h-100' style='border-radius: 8px; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1); border: 1px solid #ccc;'>"; 
                text += "<img src='" + item.Image + "' class='card-img-top rounded-top' style='object-fit: cover; height: 200px; border-radius: 8px 8px 0 0;'>"; 
                text += "<div class='card-body'>";
                text += "<h6 class='card-title mb-2' style='font-size: 14px; font-weight: bold; color: #333;'>" + item.Name + "</h6>"; 
                text += "<p class='card-text mb-2' style='font-size: 23px; color:  #333; font-weight: bold;'>" + item.Price + " <span style='font-weight: bold;'>$</span></p>"; 
                text += "<a href='javascript:void(0)' class='btn btn-sm btn-info' data-toggle='modal' style='background: linear-gradient(40deg,#CC00FF, #FFCC00);' data-target='#update' data-whatever='" + item.Idp + "' style='font-size: 12px; text-decoration: none;'>Xem chi tiết <i class='far fa-eye'></i></a>"; // Added Bootstrap button classes for "Xem chi tiết" button
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


        //dang nhap
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


        //public IActionResult CheckLogin()
        //{
        //    string text = "";
        //    var idkh = HttpContext.Session.GetString("KHACHHANG");
        //    if (idkh != null)
        //    {
        //        text = "<form action='/Client/trangchu/Logout' method='post'><button type='submit' class='logout-button'><i class='fas fa-sign-out-alt'></i></button></form>";
        //    }

        //    return Json(new { data = text });
        //}

        [HttpGet]
        public JsonResult CheckLoginStatus()
        {
            string name = HttpContext.Session.GetString("KHACHHANG");
            khachhangDAO khachhang = new khachhangDAO();
            var ten = khachhang.checkstatus(name);
            return Json(new { data = ten });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Client/Trangchu/Login");
        }



        //dang ky
        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Dangky(string name, int age, string phone, string address, string username, string password, string image)
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

            return Json(new { mess = "Đăng ký thành công" });

        }

        //gio hang
        public JsonResult AddCart(int idp, int quantity, int price)
        {
            var username = HttpContext.Session.GetString("KHACHHANG");
            if (username == null)
            {
                return Json(new { mess = "Bạn cần đăng nhập để thêm vào giỏ hàng", state = false });
            }

            khachhangDAO makhach= new khachhangDAO();
            int ma = makhach.Login(HttpContext.Session.GetString("KHACHHANG"));
            
            bool kiemtralogin = false;
            if (ma != null)
            {
                kiemtralogin = true;
                hoadonDAO hoadon = new hoadonDAO();
                int idc = ma;
                if (hoadon.Kiemtragohang(idc) == true)
                {
                    chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
                    int idh = hoadon.getIDhoadon(idc);
                    Chitiethoadon item = new Chitiethoadon();
                    int cthd = chitiethoadon.Kiemtra(idp, idh);
                    if (cthd == -1)
                    {
                        item.Idp = idp;
                        item.Quantity = quantity;
                        item.Price = price;
                        item.Idh = idh;

                    }
                    else
                    {
                        item = chitiethoadon.getItem(cthd);
                        item.Quantity += 1;
                    }
                    chitiethoadon.InsertOrUpdate(item);
                    return Json(new { mess = "Thêm vào giỏ hàng thành công", state = kiemtralogin });
                }


                else if (hoadon.Kiemtragohang(idc) == false)
                {
                    Hoadon hd = new Hoadon();
                    hd.Idc = idc;
                    hoadon.InsertOrUpdate(hd);
                    chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
                    Chitiethoadon item = new Chitiethoadon();
                    item.Idp = idp;
                    item.Quantity = quantity;
                    item.Price = price;
                    item.Idh = hd.Idh;
                    chitiethoadon.InsertOrUpdate(item);
                    return Json(new { mess = "Thêm vào giỏ hàng thành công", state = kiemtralogin });
                }
            }

            return Json(new { mess = "Ko thanh cong ", state = kiemtralogin });
        }
        public ActionResult Cart()
        {
            return View();
        }
        //public JsonResult MyCart()
        //{

        //    chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
        //    int idc = HttpContext.Session.GetInt32("KHACHHANG") ?? 0;
        //    string text = "";
        //    var query = chitiethoadon.getCart(idc);
        //    foreach (var item in query)
        //    {
        //        text += "<tr>";
        //        text += "<td" + item.Idct + "</td>";
        //        text += "<td>";
        //        text += " <img src='" + item.Image + "' style='width:40px;height:40px;'/>";
        //        text += "</td>";
        //        text += "<td>" + item.Name + "</td>";
        //        text += "<td>";
        //        text += "<input type='number' class='form - control' readonly='readonly' value='" + item.Price + "'>";
        //        text += "</td>";
        //        text += "<td>";
        //        text += "<input type='number' class='form - control' id ='qua_" + item.Idct + "'  value='" + item.Quantity + "' onchange='cart.updateTotal(" + item.Idct + ")'>";
        //        text += "</td>";
        //        text += "<td>";
        //        text += "<input type='number' class='form - control' id='total_" + item.Idct + "' readonly='readonly' value='" + item.Total + "'>";
        //        text += "</td>";
        //        text += " <td><a href='/Client/trangchu/Delete/" + item.Idct + "'><i class='fa fa-trash' aria-hidden='true'></i> </a></td>";
        //        text += "</tr>";
        //    }
        //    return Json(new { data = text });
        //}
        [HttpPost]
        public JsonResult Mycart()
        {
            var username = HttpContext.Session.GetString("KHACHHANG");
            if (username == null)
            {
                return Json(new { mess = "Bạn cần đăng nhập để xem giỏ hàng", status = 0 });
            }

            khachhangDAO makhach = new khachhangDAO();
            int ma = makhach.Login(HttpContext.Session.GetString("KHACHHANG"));
            int tong = 0;
            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            int idc = ma;
            string text = "";
            var query = chitiethoadon.getCart(idc);
            foreach (var item in query)
            {
                text += "<tr style='border-bottom: 1px solid #ddd;'>";
                text += "<td style='max-height=300px; max-width: 300px;'><img src='" + item.Image + "' style='max-height: 100px; max-width: 100px; border-radius: 5px;' /></td>";
                text += "<td  style='color: #3498db; font-weight: bold;'>" + item.Name + "</td>";
                text += "<td  style='color: #FF0000; font-weight: bold;'>" + item.Price + "</td>";
                text += "<td><input type='number' class='form-control' min='1' id='qua_" + item.Idct + "'  value='" + item.Quantity + "' onchange='cart.updateTotal(" + item.Idct + ")' style='background-color: #ecf0f1;' ></td>";
                text += "<td><input type='number' class='form-control' id='total_" + item.Idct + "' readonly='readonly' value='" + item.Total + "' style='background-color: #d4efdf;'></td>";
                text += "<td><a href='javascript:void(0)' onclick='cart.delete(" + item.Idct + ")' style='color: #e74c3c; cursor: pointer;'><i class='fa fa-trash'></i></a></td>";
                text += "</tr>";
                tong += item.Total.Value;

                //text += "<tr style='border-bottom: 1px solid #ddd;'>";
                //text += "<td style='max-height: 300px; max-width: 300px;'><img src='" + item.Image + "' alt='Product Image' style='max-height: 100px; max-width: 100px; border-radius: 5px;' /></td>";
                //text += "<td style='color: #3498db; font-weight: bold;'>" + item.Name + "</td>";
                //text += "<td><input type='number' class='form-control' readonly='readonly' value='" + item.Price + "' style='background-color: #f5f5f5;'></td>";
                //text += "<td><input type='number' class='form-control' min='0' id='quantity_" + item.Idct + "' value='" + item.Quantity + "' onchange='cart.updateTotal(" + item.Idct + ")' style='background-color: #ecf0f1;'></td>";
                //text += "<td><input type='number' class='form-control' id='total_" + item.Idct + "' readonly='readonly' value='" + item.Total + "' style='background-color: #d4efdf;'></td>";
                //text += "<td><a href='javascript:void(0)' onclick='cart.delete(" + item.Idct + ")' style='color: #e74c3c; cursor: pointer;'><i class='fa fa-trash'></i></a></td>";
                //text += "</tr>";
                //tong += item.Total.Value;


            }
            return Json(new { data = text, tong = tong });
        }
        public JsonResult Order(string name, string sdt, string address, int tongtien)
        {   khachhangDAO khachhangDAO = new khachhangDAO();
            hoadonDAO hoadonDAO = new hoadonDAO();
            string username = HttpContext.Session.GetString("KHACHHANG") ;
            int idc = khachhangDAO.doi(username);
            var item = hoadonDAO.getItemOrder(idc);
            item.Name = name;
            item.Phone = sdt;
            item.Address = address;
            item.Status = 2;
            item.Total = tongtien;
            item.Date = DateTime.Now;
            hoadonDAO.InsertOrUpdate(item);
            return Json(new { mess = "Đặt hàng thành công " });
        }
        public JsonResult updateTotal(int id, int soluong)
        {
            chitiethoadonDAO chitiethoadon = new chitiethoadonDAO();
            var item = chitiethoadon.getItem(id);
            item.Quantity = soluong;
            var t = item.Price * soluong;
            chitiethoadon.InsertOrUpdate(item);
            //Laays toongr tien cua hoa don
            var tt = 0;
            var idh = item.Idh;
            hoadonDAO hoadon = new hoadonDAO();
            tt = hoadon.getTotal(idh ?? 0);
            return Json(new { tongtien = t, total = tt });
        }
        public ActionResult Delete(int id)
        {
            chitiethoadonDAO x = new chitiethoadonDAO();
            x.Detele(id);
            return Json(new { mess = "Xoa san pham thanh cong" });
        }
        

    }
}