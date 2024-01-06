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
                             Address = a.Address
                         }).FirstOrDefault();
            return query;
        }

        public List<hoadonVIEW> getList()
        {
            var query = (from a in context.Hoadons
                         select new hoadonVIEW
                         {
                             Idh = a.Idh,
                             Id = a.Id,
                             Idc = a.Idc,
                             Total = a.Total,
                             Date = a.Date,
                             Name = a.Name,
                             Phone = a.Phone,
                             Address = a.Address
                         }).ToList();
            return query;
        }
        public List<hoadonVIEW> Search(out int totalp, string name = "", int index = 1, int size = 7)
        {
            var query = context.Hoadons.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
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
                    Address = a.Address
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

        public List<int> Doanhthu(int nam)
        {
            List<int> rs = new List<int>();

            for (int i = 1; i < 13; i++)
            {
                DateTime start = DateServices.GetFirstDayOfMonth(i, nam);
                DateTime end = DateServices.GetLastDayOfMonth(i, nam);

                // Lấy danh sách hóa đơn trong khoảng thời gian của tháng hiện tại
                List<hoadonVIEW> query = (from a in context.Hoadons
                                          where a.Date >= start && a.Date <= end
                                          select new hoadonVIEW
                                          {
                                              Idh = a.Idh,
                                              Id = a.Id,
                                              Idc = a.Idc,
                                              Total = a.Total ?? 0,
                                              Date = a.Date,
                                              Name = a.Name,
                                              Phone = a.Phone,
                                              Address = a.Address
                                          }).ToList();

                // Tính tổng doanh thu của tháng và thêm vào danh sách rs
                int doanhThuThang = (int)query.Sum(item => item.Total);
                rs.Add(doanhThuThang);
            }

            return rs;
        }

    }
}
