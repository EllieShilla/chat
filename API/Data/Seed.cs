using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;
            AppUser appUser = new AppUser()
            {
                Email = "-",
                UserName = "Admin"
            };

            context.Users.Add(appUser);
            await context.SaveChangesAsync();
        }
    }
}