using Microsoft.AspNetCore.Identity;
using SuperShopV2.Data.Entities;
using SuperShopV2.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShopV2.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("goncalorusso@gmail.com");
            if(user == null) 
            {
                user = new User
                {
                    FirstName = "Gonçalo",
                    LastName = "Russo",
                    Email = "goncalorusso@gmail.com",
                    UserName = "goncalorusso@gmail.com",
                    PhoneNumber = "123456788"
                };

                var result = await _userHelper.AddUserAsync(user, "654321");    //Cria este utilizador com esta Password
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");   
                }

            }

            if(!_context.Products.Any())
            {
                AddProduct("iPhone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("iWatch Series 4", user);
                AddProduct("iPad Mini", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100),
                User = user
            });
        }
    }
}
