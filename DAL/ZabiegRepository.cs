using System;
using PlacowkaZdrowia.Models;

namespace PlacowkaZdrowia.DAL
{
    public class ZabiegRepository : GenericRepository<Zabieg>
    {
        public ZabiegRepository(PlacowkaZdrowiaContext context)
            : base(context)
        {
        }

        public int UpdateZabiegKoszt(int multiplier)
        {
            return context.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier);
        }

    }
}