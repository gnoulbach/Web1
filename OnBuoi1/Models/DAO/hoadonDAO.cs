using OnBuoi1.Helper;
using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;

namespace OnBuoi1.Models.DAO
{
    public class hoadonDAO
    {
        private OnTapContext context = new OnTapContext();
        public void InsertOrUpdate(Hoadon item)
        {
            if (item.Idh == 0)
            {
                context.Hoadons.Add(item);
            }
            context.SaveChanges();
        }
        public Hoadon getItem(int idh)
        {

            return context.Hoadons.Where(x => x.Idh == idh).FirstOrDefault();
        }
        public Hoadon getItemOrder(int idc)
        {

            return context.Hoadons.Where(x => x.Idc == idc ).FirstOrDefault();
        }
        public hoadonVIEW getItemView(int idh)
        {

            var query = (from a in context.Hoadons
                         where a.Idh == idh
                         select new hoadonVIEW
                         {
                             Idh = a.Idh,
                             Id = a.Id,
                             Idc = a.Idc,
                             Total = a.Total,
                             Date = a.Date,
                             Name = a.Name,
                             Phone = a.Phone,
                             Address = a.Address,
                             Status = a.Status
                         }).FirstOrDefault();
            return query;
        }

        public List<hoadonVIEW> getList()
        {
            var query = (from a in context.Hoadons
                         join b in context.Khachhangs on a.Idc equals b.Idc
                         select new hoadonVIEW
                         {
                             Idh = a.Idh,
                             Id = a.Id,
                             Idc = a.Idc,
                             Total = a.Total,
                             Date = a.Date,
                             Name = a.Name,
                             Phone = a.Phone,
                             Address = a.Address,
                             Status = a.Status
                         }).ToList();
            return query;
        }
        public List<hoadonVIEW> Search(out int totalp, string name = "", int index = 1, int size = 7)
        {
            var query = context.Hoadons.AsQueryable();

            // Filter by name if provided
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }

            // Filter by Status = 2
            query = query.Where(a => a.Status == 2);  // Add this line for the condition

            totalp = query.Count();

            var result = query
                .Select(a => new hoadonVIEW
                {
                    Idh = a.Idh,
                    Id = a.Id,
                    Idc = a.Idc,
                    Total = a.Total,
                    Date = a.Date,
                    Name = a.Name,
                    Phone = a.Phone,
                    Address = a.Address,
                    Status = a.Status
                })
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();

            return result;
        }




        public void Detele(int idh)
        {
            Hoadon x = getItem(idh);
            context.Hoadons.Remove(x);
            context.SaveChanges();
        }

        internal object search(string name, out int total, int index, int size)
        {
            throw new NotImplementedException();
        }



        public Boolean Kiemtragohang(int idc)
        {
            var query = (from a in context.Hoadons
                         where (a.Idc == idc)
                         select new hoadonVIEW
                         {
                             Idh = a.Idh,
                             Id = a.Id,
                             Idc = a.Idc,
                             Total = a.Total ?? 0,
                             Date = a.Date,
                             Name = a.Name,
                             Phone = a.Phone,
                             Address = a.Address,
                             Status = a.Status
                         }).FirstOrDefault();
            if (query != null)
                return true;
            return false;
        }
        public int getIDhoadon(int idc)
        {
            var query = (from a in context.Hoadons
                         where (a.Idc == idc)
                         select new hoadonVIEW
                         {
                             Idh = a.Idh,
                         }).FirstOrDefault();

            return query.Idh;
        }
        public int getTotal(int idh)
        {
            var query = (from a in context.Hoadons
                         join b in context.Chitiethoadons on a.Idh equals b.Idh
                         where a.Idh == idh
                         select new hoadonVIEW
                         {
                             Idh = a.Idh,
                             Id = a.Id,
                             Idc = a.Idc,
                             Total = (b.Quantity ?? 0) * (b.Price ?? 0),
                             Date = a.Date,
                             Name = a.Name,
                             Phone = a.Phone,
                             Address = a.Address,
                             Status = a.Status
                         }).ToList();
            return (int)query.Sum(x => x.Total);
        }
        public List<hoadonVIEW> History(int idc, out int total, int index = 1, int size = 10)
        {
            var query = (from a in context.Hoadons
                         join b in context.Khachhangs on a.Idc equals b.Idc

                         where b.Idc == idc 
                         select new hoadonVIEW
                         {
                             Idh = a.Idh,
                             Id = a.Id,
                             Idc = a.Idc,
                             Total = a.Total ?? 0,
                             Date = a.Date,
                             Name = a.Name,
                             Phone = a.Phone,
                             Address = a.Address,
                             Status = a.Status
                         }).ToList();
            total = query.Count();
            var result = query.Skip((index - 1) * size).Take(size).ToList();
            return result;
        }



        public List<int> Doanhthu(int nam)
        {
            List<int> rs = new List<int>();

            for (int i = 1; i < 13; i++)
            {
                DateTime start = DateServices.GetFirstDayOfMonth(i, nam);
                DateTime end = DateServices.GetLastDayOfMonth(i, nam);

                // Filter invoices with status 2 within the current month
                List<hoadonVIEW> query = (from a in context.Hoadons
                                          where a.Date >= start && a.Date <= end && a.Status == 2 // Added status filter
                                          select new hoadonVIEW
                                          {
                                              Idh = a.Idh,
                                              Id = a.Id,
                                              Idc = a.Idc,
                                              Total = a.Total ?? 0,
                                              Date = a.Date,
                                              Name = a.Name,
                                              Phone = a.Phone,
                                              Address = a.Address,
                                              Status = a.Status
                                          }).ToList();

                // Calculate and add monthly revenue to the list
                int doanhThuThang = (int)query.Sum(item => item.Total);
                rs.Add(doanhThuThang);
            }

            return rs;
        }


    }
}
