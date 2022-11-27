using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class SeedData
    {
        public static async Task SeedUser(DataContext context)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }
            try
            {
                var readJsonFile  = await File.ReadAllTextAsync("D:/Dotnet6/1/SocialApp/API/Data/UserSeedData.json"); 
                var users = JsonSerializer.Deserialize<List<AppUser>>(readJsonFile);
                foreach (var item in users)
                {
                    using var hmac = new HMACSHA512();
                    item.UserName = item.UserName.ToLower();
                    item.KnownAs = item.KnownAs.ToLower();
                    item.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                    item.PasswordSalt = hmac.Key;

                    context.Users.Add(item);
                }
                await context.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            
            
        }
    }
}