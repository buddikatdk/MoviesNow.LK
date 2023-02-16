using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesNow.LK.Data.Entity;

namespace MoviesNow.LK.Data
{
    public class DataContext:IdentityDbContext<User,Role,string>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
    }
}
