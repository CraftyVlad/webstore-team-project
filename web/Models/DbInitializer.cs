using System.Reflection.Metadata;
using web.Models;
using web.Models.Entities;

namespace web.Models
{
    public class DbInitializer
    {
        public static void Init(ApplicationContext db)
        {
            {
                // comment this
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.SaveChanges();
            }
        }
    }
}
