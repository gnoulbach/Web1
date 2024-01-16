using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;


namespace OnBuoi1.Models.DAO
{
    public class khachhangDAO
    {
        private OnTapContext context = new OnTapContext();
        public void InsertOrUpdate(Khachhang item)
        {
            if (item.Idc == 0)
            {
                context.Khachhangs.Add(item);
            }
            context.SaveChanges();
        }
        public Khachhang getItem(int idc)
        {

            return context.Khachhangs.Where(x => x.Idc == idc).FirstOrDefault();
        }
        public khachhangVIEW getItemView(int idc)
        {

            var query = (from a in context.Khachhangs
                         where a.Idc == idc
                         select new khachhangVIEW
                         {
                             Idc = a.Idc,
                             Name = a.Name,
                             Age = a.Age,
                             Phone = a.Phone,
                             Address = a.Address,
                             Username = a.Username,
                             Password = a.Password,
                             Image = a.Image
                         }).FirstOrDefault();
            return query;
        }

        public List<khachhangVIEW> getList()
        {
            var query = (from a in context.Khachhangs
                         select new khachhangVIEW
                         {
                             Idc = a.Idc,
                             Name = a.Name,
                             Age = a.Age,
                             Phone = a.Phone,
                             Address = a.Address,
                             Username = a.Username,
                             Password = a.Password,
                             Image = a.Image
                         }).ToList();
            return query;
        }
        public List<khachhangVIEW> Search(out int totalp, string name = "", int index = 1, int size = 7)
        {
            var query = context.Khachhangs.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            totalp = query.Count();
            var result = query
                .Select(a => new khachhangVIEW
                {
                    Idc = a.Idc,
                    Name = a.Name,
                    Age = a.Age,
                    Phone = a.Phone,
                    Address = a.Address,
                    Username = a.Username,
                    Password = a.Password,
                    Image = a.Image
                })
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            return result;
        }



        public void Detele(int idc)
        {
            Khachhang x = getItem(idc);
            context.Khachhangs.Remove(x);
            context.SaveChanges();
        }

        internal object search(string name, out int total, int index, int size)
        {
            throw new NotImplementedException();
        }

        public Boolean Login(String username, String password)
        {
            var query = (from a in context.Khachhangs
                         where a.Username == username && a.Password == password
                         select new khachhangVIEW
                         {
                             Idc = a.Idc,
                             Name = a.Name,
                             Age = a.Age,
                             Phone = a.Phone,
                             Address = a.Address,
                             Username = a.Username,
                             Password = a.Password,
                             Image = a.Image
                         }).FirstOrDefault();
            if (query != null)
                return true;
            return false;
        }
        public int Login(String username)
        {
            var query = (from a in context.Khachhangs
                         where a.Username == username
                         select new khachhangVIEW
                         {
                             Idc = a.Idc,

                         }).FirstOrDefault();
            return query.Idc;
        }

        public  khachhangVIEW checkstatus(String name)
        {
            var query = (from a in context.Khachhangs
                         where a.Username == name
                         select new khachhangVIEW
                         {
                             Idc = a.Idc,
                             Name = a.Name,

                         }).FirstOrDefault();
            return query;
        }
    }
}