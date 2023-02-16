using Microsoft.AspNetCore.Identity;

namespace MoviesNow.LK.Data.Entity
{
    public class User: IdentityUser
    {
        public string? Country { get; set; }
    }
}
