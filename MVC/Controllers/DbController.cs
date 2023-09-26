using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Text;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        private readonly Db _db;

        public DbController(Db db)
        {
            _db = db;
        }

        public IActionResult Seed()
        {
            var vehicles = _db.Vehicles.ToList();
            _db.Vehicles.RemoveRange(vehicles);

            var models = _db.Models.ToList();
            _db.Models.RemoveRange(models);

            var brands = _db.Brands.ToList();
            _db.Brands.RemoveRange(brands);

            var colors = _db.Colors.ToList();
            _db.Colors.RemoveRange(colors);

			var users1 = _db.Users.ToList();
			foreach (var user in users1)
			{
				user.UserDetailId = -1; // Kullanıcının UserDetail referansını kaldırın
			}
			

			var userDetails = _db.UserDetails.ToList();
            _db.UserDetails.RemoveRange(userDetails);

            var users = _db.Users.ToList();
            _db.Users.RemoveRange(users);

           

            var customerDetails = _db.CustomerDetails.ToList();
            _db.CustomerDetails.RemoveRange(customerDetails);

            var customers = _db.Customers.ToList();
            _db.Customers.RemoveRange(customers);

            var roles = _db.Roles.ToList();
            _db.Roles.RemoveRange(roles);

            if (roles.Count > 0) 
            {
                _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Roles', RESEED, 0)"); 
            }

            

           

            var cities = _db.Cities.ToList();
            _db.Cities.RemoveRange(cities);

            var countries = _db.Countries.ToList();
            _db.Countries.RemoveRange(countries);


            _db.SaveChanges();


            _db.Brands.Add(new Brand()
            {
                Name = "Mercedes-Benz",
                Models = new List<Model>()
                {
                   new Model()
                   {
                       Name = "E 250 CDI Edition"
                   },

                    new Model()
                   {
                       Name = "C 200 Edition"
                   },

                     new Model()
                   {
                       Name = "E 180 Elite"
                   },

                    new Model()
                    {
                        Name = "A 200 BlueEfficiency AMG"
                    }
                }
            });

            _db.Colors.Add(new Color()
            {
                Name = "White"
            });

            _db.Colors.Add(new Color()
            {
                Name = "Grey"
            });

            _db.Colors.Add(new Color()
            {
                Name = "Silver Gray"
            });

            _db.Colors.Add(new Color()
            {
                Name = "Smoked" // Füme
            });

            _db.Colors.Add(new Color()
            {
                Name = "Black"
            });

            _db.SaveChanges();


            var turkey = new Country()
            {
                Name = "Türkiye",
                Cities = new List<City>()
                {
                    new City()
                    {
                        Name = "Ankara"
                    },

                     new City()
                    {
                        Name = "İzmir"
                    },

                      new City()
                    {
                        Name = "İstanbul"
                    },

                       new City()
                    {
                        Name = "Antalya"
                    },
                }
            };

            var azerbaycan = new Country()
            {
                Name = "Azerbaycan",
                Cities = new List<City>()
                {
                    new City()
                    {
                        Name = "Bakü"
                    },
                    new City()
                    {
                        Name = "Nahçıvan"
                    }
                }
            };
            _db.Countries.Add(turkey);

            _db.SaveChanges();

            var newCustomerDetail1 = new CustomerDetail()
            {
                Sex = Sex.Man,
                Email = "man1@hotmail.com",
                Phone = "05552221111",
                Address = "Maltepe Mahallesi 5434 Sk. No:24 Beşiktaş/İSTANBUL",
                CityId = _db.Cities.SingleOrDefault(c => c.Name == "İstanbul")?.Id ?? 0,
                CountryId = turkey.Id


            };

            var newCustomerDetail2 = new CustomerDetail()
            {
                Sex = Sex.Woman,
                Email = "woman2@hotmail.com",
                Phone = "05552221111",
                Address = "İzmir Mahallesi 3334 Sk. No:21 Bornova/İzmir",
                CityId = _db.Cities.SingleOrDefault(c => c.Name == "İzmir")?.Id ?? 0,
                CountryId = turkey.Id


            };

            var newCustomer1 = new Customer()
            {
                Name = "Ali",
                Surname = "Bakar",
                CustomerDetail = newCustomerDetail1
            };

            var newCustomer2 = new Customer()
            {
                Name = "Hatice",
                Surname = "Aslan",
                CustomerDetail = newCustomerDetail2
            };



            _db.CustomerDetails.AddRange(newCustomerDetail1, newCustomerDetail2);
            _db.Customers.AddRange(newCustomer1, newCustomer2);

            _db.SaveChanges();


            var newUserDetail1 = new UserDetail()
            {
                Sex = Sex.Man,
                Email = "man3@hotmail.com",
                Phone = "05552221111",
                Address = "Divrik Mahallesi 2120 Sk. No:34 Çankaya/Ankara",
                CityId = _db.Cities.SingleOrDefault(c => c.Name == "Ankara")?.Id ?? 0,
                CountryId = turkey.Id


            };

            var newUserDetail2 = new UserDetail()
            {
                Sex = Sex.Woman,
                Email = "woman2@hotmail.com",
                Phone = "05552221111",
                Address = "Divrik Mahallesi 2120 Sk. No:34 Çankaya/Ankara",
                CityId = _db.Cities.SingleOrDefault(c => c.Name == "Ankara")?.Id ?? 0,
                CountryId = turkey.Id


            };

            _db.UserDetails.AddRange(newUserDetail1, newUserDetail2);

            _db.Roles.Add(new Role()
            {
                Name = "Admin",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserName = "admin",
                        Password = "admin",
                        IsActive = true,
                         UserDetail = newUserDetail1

                    }
                }
            });

            _db.Roles.Add(new Role()
            {
                Name = "User",
                Users = new List<User>()
                {
                    new User()
                    {
                        UserName = "user",
                        Password = "user",
                        IsActive = true,
                        UserDetail = newUserDetail2
                    }
                }
            });

            

          
            

            _db.Vehicles.Add(new Vehicle()
            {
                Price = 2000000,
                Year = 2020,
                IsSold = true,
                BrandId = _db.Brands.SingleOrDefault(b => b.Name == "Mercedes-Benz").Id,
                ModelId = _db.Models.SingleOrDefault(m => m.Name == "E 250 CDI Edition").Id,
                ColorId = _db.Colors.SingleOrDefault(c => c.Name == "White").Id,
                CustomerId = _db.Customers.SingleOrDefault(c => c.Name == "Ali" && c.Surname == "Bakar").Id
            }) ;

            _db.Vehicles.Add(new Vehicle()
            {
                Price = 1500000,
                Year = 2022,
                IsSold = true,
                BrandId = _db.Brands.SingleOrDefault(b => b.Name == "Mercedes-Benz").Id,
                ModelId = _db.Models.SingleOrDefault(m => m.Name == "C 200 Edition").Id,
                ColorId = _db.Colors.SingleOrDefault(c => c.Name == "Grey").Id,
                CustomerId = _db.Customers.SingleOrDefault(c => c.Name == "Hatice" && c.Surname == "Aslan").Id
            });

            _db.Vehicles.Add(new Vehicle()
            {
                Price = 3000000,
                Year = 2023,
                IsSold = false,
                BrandId = _db.Brands.SingleOrDefault(b => b.Name == "Mercedes-Benz").Id,
                ModelId = _db.Models.SingleOrDefault(m => m.Name == "E 180 Elite").Id,
                ColorId = _db.Colors.SingleOrDefault(c => c.Name == "Silver Gray").Id,

            });

            _db.Vehicles.Add(new Vehicle()
            {
                Price = 3000000,
                Year = 2023,
                IsSold = false,
                BrandId = _db.Brands.SingleOrDefault(b => b.Name == "Mercedes-Benz").Id,
                ModelId = _db.Models.SingleOrDefault(m => m.Name == "A 200 BlueEfficiency AMG").Id,
                ColorId = _db.Colors.SingleOrDefault(c => c.Name == "Smoked").Id,

            });


            _db.SaveChanges();



            return Content("<p style =\"color:red;font-weight:bold;\">Database seed successful.</p>", "text/html", Encoding.UTF8);
        }
    }
}
