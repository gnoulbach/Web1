using Microsoft.EntityFrameworkCore;
using OnBuoi1.Helper;
using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnBuoi1.Models.DAO
{
    public class chitiethoadonDAO
    {
        private OnTapContext context = new OnTapContext();
        public void InsertOrUpdate(Chitiethoadon item)
            {
                if (item.Idct == 0)
                {
                    context.Chitiethoadons.Add(item);
                }
                context.SaveChanges();
            }
            public Chitiethoadon getItem(int idct)
            {

                return context.Chitiethoadons.Where(x => x.Idct == idct).FirstOrDefault();
            }
            public chitiethoadonVIEW getItemView(int idct)
            {

                var query = (from a in context.Chitiethoadons
                             join b in context.Sanphams on a.Idp equals b.Idp
                             where a.Idct == idct
                             select new chitiethoadonVIEW
                             {
                                 Idct = a.Idct,
                                 Idp = a.Idp,
                                 Name = b.Name,
                                 Idh = a.Idh,
                                 Price = b.Price,
                                 Quantity = a.Quantity,
                                 Total = b.Price * a.Quantity
                             }).FirstOrDefault();
                return query;
            }
            public chitiethoadonVIEW getTongTien(int idct, int soluong)
            {

                var query = (from a in context.Chitiethoadons
                             join b in context.Sanphams on a.Idp equals b.Idp
                             where a.Idct == idct
                             select new chitiethoadonVIEW
                             {
                                 Idct = a.Idct,
                                 Idp = a.Idp,
                                 Name = b.Name,
                                 Idh = a.Idh,
                                 Price = b.Price,

                                 Total = b.Price * a.Quantity
                             }).FirstOrDefault();
                return query;
            }
            public List<chitiethoadonVIEW> getList()
            {
                var query = (from a in context.Chitiethoadons
                             join b in context.Sanphams on a.Idp equals b.Idp
                             select new chitiethoadonVIEW
                             {
                                 Idct = a.Idct,
                                 Idp = a.Idp,
                                 Name = b.Name,
                                 Idh = a.Idh,
                                 Price = b.Price,
                                 Quantity = a.Quantity,
                                 Total = b.Price * a.Quantity
                             }).ToList();
                return query;
            }
            public List<chitiethoadonVIEW> Search(int idh)
            {
                var query = (from a in context.Chitiethoadons
                             join b in context.Sanphams on a.Idp equals b.Idp
                             where (a.Idh == idh)
                             select new chitiethoadonVIEW
                             {
                                 Idct = a.Idct,
                                 Idp = a.Idp,
                                 Name = b.Name,
                                 Idh = a.Idh,
                                 Price = b.Price,
                                 Quantity = a.Quantity,
                                 Total = b.Price * a.Quantity
                             }).ToList();


                return query;
            }

            public void Detele(int id)
            {
                Chitiethoadon x = getItem(id);
                context.Chitiethoadons.Remove(x);
                context.SaveChanges();
            }
            public int Kiemtra(int idp, int idh)
            {
                int id = -1;
                var query = (from a in context.Chitiethoadons
                             join b in context.Hoadons on a.Idh equals b.Idh
                             where (a.Idh == idh && a.Idp == idp )
                             select new chitiethoadonVIEW
                             {
                                 Idct = a.Idct,
                                 Idp = a.Idp,
                                 Idh = a.Idh,
                                 Price = a.Price,
                                 Quantity = a.Quantity,
                                 Total = a.Price * a.Quantity
                             }).FirstOrDefault();
                if (query != null)
                    id = query.Idct;
                return id;
            }
            public List<chitiethoadonVIEW> getCart(int idc)
            {
                var query = (from a in context.Chitiethoadons
                             join b in context.Sanphams on a.Idp equals b.Idp
                             join c in context.Hoadons on a.Idh equals c.Idh
                             where (c.Idc == idc)
                             select new chitiethoadonVIEW
                             {
                                 Idct = a.Idct,
                                 Idp = a.Idp,
                                 Name = b.Name,
                                 Idh = a.Idh,
                                 Price = b.Price,
                                 Quantity = a.Quantity,
                                 Total = b.Price * a.Quantity,
                                 Image = b.Image
                             }).ToList();
                return query;
            }
            public List<chitiethoadonVIEW> Soluongban(int month, int year)
            {
                DateTime start = DateServices.GetFirstDayOfMonth(month, year);
                DateTime end = DateServices.GetLastDayOfMonth(month, year);

                var query = (from a in context.Chitiethoadons
                             join b in context.Sanphams on a.Idp equals b.Idp
                             join c in context.Hoadons on a.Idh equals c.Idh
                             where c.Date >= start && c.Date <= end
                             group new { a, b } by new { a.Idp, b.Name, a.Idh, a.Price, b.Image} into grouped
                             select new chitiethoadonVIEW
                             {
                                 Idct = grouped.FirstOrDefault().a.Idct,
                                 Idp = grouped.Key.Idp,
                                 Name = grouped.Key.Name,
                                 Idh = grouped.Key.Idh,
                                 Price = grouped.Key.Price,
                                 Quantity = grouped.Sum(item => item.a.Quantity),
                                 Total = grouped.Key.Price * grouped.Sum(item => item.a.Quantity),
                                 Image = grouped.Key.Image
                             }).ToList();
                return query;
            }
        }
}
