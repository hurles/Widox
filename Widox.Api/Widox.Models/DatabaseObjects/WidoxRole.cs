using Microsoft.AspNetCore.Identity;

namespace Widox.Models.DatabaseObjects
{
    public class WidoxRole : IdentityRole<long>
    {
        //used for migrations
        public WidoxRole() { }

        public WidoxRole(string roleName) 
            : base(roleName)
        {
        }
    }
}
