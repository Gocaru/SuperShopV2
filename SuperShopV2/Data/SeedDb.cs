using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperShopV2.Data.Entities;
using SuperShopV2.Helpers;
using System;
using System.Collections.Generic;
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
            await _context.Database.MigrateAsync();   //Vê se existe BD; se não existe cria-a
                                                      //Uso "MigrateAsync" para quando for popular a base de Dados com o Seed, vai correr as migrações

            await _userHelper.CheckRoleAsync("Admin");      //Vê se exite o Role "Admin"; se não existe cria-o
            await _userHelper.CheckRoleAsync("Customer");   //Vê se exite o Role "Customer"; se não existe cria-o

            if(!_context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Lisboa" });
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Faro" });

                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });

                await _context.SaveChangesAsync();
            }


            var user = await _userHelper.GetUserByEmailAsync("goncalorusso@gmail.com"); //vou buscar este user
            if(user == null)    //se não exitir, crio o user
            {
                user = new User
                {
                    FirstName = "Gonçalo",
                    LastName = "Russo",
                    Email = "goncalorusso@gmail.com",
                    UserName = "goncalorusso@gmail.com",
                    PhoneNumber = "123456788",
                    Address = "Rua da Liberdade, 33",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "654321");    //Adiciono este utilizador com esta Password
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");   
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");    //Associo o role Admin ao user que criei
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");  //Verifico se o user tem o role Admin (não corremos o risco de ter algum user sem role)
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
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
