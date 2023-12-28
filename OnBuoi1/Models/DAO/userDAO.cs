using Microsoft.EntityFrameworkCore;
using OnBuoi1.Models.EF;
using OnBuoi1.Models.VIEW;

namespace OnBuoi1.Models.DAO
{
    public class userDAO
    {
        private OnTapContext context = new OnTapContext();
        public void InsertOrUpdate(User item)
        {
            if (item.Id == 0)
            {
                context.Users.Add(item);
            }
            context.SaveChanges();
        }
        public User getItem(int id)
        {

            return context.Users.Where(x => x.Id == id).FirstOrDefault();
        }
        public userVIEW getItemView(int id)
        {

            var query = (from a in context.Users
                         where a.Id == id
                         select new userVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Age = a.Age,
                             Address = a.Address
                             
                         }).FirstOrDefault();
            return query;
        }

        public List<userVIEW> getList()
        {
            var query = (from a in context.Users
                         select new userVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Age = a.Age,
                             Address = a.Address
                         }).ToList();
            return query;
        }
        public List<userVIEW> Search(out int total, string name = "", int index = 1, int size = 7)
        {
            var query = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            
            total = query.Count();
            var result = query
                .Select(a => new userVIEW
                {
                    Id = a.Id,
                    Name = a.Name,
                    Age = a.Age,
                    Address = a.Address
                })
                .Skip((index - 1) * size)
                .Take(size)
                .ToList();
            return result;
        }



        public void Detele(int id)
        {
            User x = getItem(id);
            context.Users.Remove(x);
            context.SaveChanges();
        }
    }
}
