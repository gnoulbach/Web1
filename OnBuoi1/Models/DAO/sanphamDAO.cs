using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;

namespace OnBuoi1.Models.DAO
{
    public class sanphamDAO
    {
        private OnTapContext context = new OnTapContext();
        public void InsertOrUpdate(Sanpham item)
        {
            if (item.Idp == 0)
            {
                context.Sanphams.Add(item);
            }
            context.SaveChanges();
        }
        public Sanpham getItem(int idp)
        {

            return context.Sanphams.Where(x => x.Idp == idp).FirstOrDefault();
        }
        public sanphamVIEW getItemView(int idp)
        {

            var query = (from a in context.Sanphams
                         where a.Idp == idp
                         select new sanphamVIEW
                         {
                             Idp = a.Idp,
                             Name = a.Name,
                             Price = a.Price,
                             Image = a.Image
                         }).FirstOrDefault();
            return query;
        }

        public List<sanphamVIEW> getList()
        {
            var query = (from a in context.Sanphams
                         select new sanphamVIEW
                         {
                             Idp = a.Idp,
                             Name = a.Name,
                             Price = a.Price,
                             Image = a.Image
                         }).ToList();
            return query;
        }
        public List<sanphamVIEW> Search(out int totalp, string name = "", int index = 1, int size = 7)
        {
            var query = context.Sanphams.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            totalp = query.Count();
            var result = query
                .Select(a => new sanphamVIEW
                {
                    Idp = a.Idp,
                    Name = a.Name,
                    Price = a.Price,
                    Image = a.Image
                })
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            return result;
        }



        public void Detele(int idp)
        {
            Sanpham x = getItem(idp);
            context.Sanphams.Remove(x);
            context.SaveChanges();
        }

        internal object search(string name, out int total, int index, int size)
        {
            throw new NotImplementedException();
        }
    }
}
