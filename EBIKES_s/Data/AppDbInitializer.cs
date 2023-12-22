using EBIKES_s.Data.Static;
using EBIKES_s.Models;
using Microsoft.AspNetCore.Identity;

namespace EBIKES_s.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Categories
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Title = "Road Bikes",
                            ImageUrl = "/images/B.jpg",
                            Description = "Road bikes are designed for fast and efficient riding on smooth, paved surfaces. They typically feature lightweight frames, thin tires, and drop handlebars that allow for an aerodynamic riding position. Road bikes are ideal for long-distance rides, fitness training, and competitive cycling events on roads."
                        },
                        new Category()
                        {
                            Title = "Mountain Bikes",
                            ImageUrl = "/images/B.jpg",
                            Description = "Mountain bikes are built to handle off-road terrain, including dirt trails, rocky paths, and uneven surfaces. They have sturdy frames, wide and knobby tires for better traction, and a suspension system to absorb shocks. Mountain bikes come in various subcategories like cross-country, trail, and downhill, each tailored for specific off-road conditions."
                        },
                        new Category()
                        {
                            Title = "Hybrid Bikes",
                            ImageUrl = "/images/B.jpg",
                            Description = "Hybrid bikes combine features of road bikes and mountain bikes, making them versatile for a variety of riding conditions. They typically have a comfortable upright riding position, medium-width tires suitable for both pavement and light trails, and a mix of features from both road and mountain bikes. Hybrid bikes are great for casual riders, commuters, and those seeking a general-purpose bicycle."
                        }
                    });
                    context.SaveChanges();
                }
                //Suppliers
                if (!context.Suppliers.Any())
                {
                    context.Suppliers.AddRange(new List<Supplier>()
                    {
                        new Supplier()
                        {
                            Name = "Supplier 1",
                            ImageUrl = "/images/p.png",
                            Description = "This is the description of the first Supplier"
                        },
                        new Supplier()
                        {
                            Name = "Supplier 2",
                            ImageUrl = "/images/p.png",
                            Description = "This is the description of the second Supplier"
                        },
                        new Supplier()
                        {
                            Name = "Supplier 3",
                            ImageUrl = "/images/p.png",
                            Description = "This is the description of the third Supplier"
                        }
                    });
                    context.SaveChanges();
                }
                //Products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Title = "Trek Madone SLR 9 Road Bike",
                            Description = " The Trek Madone SLR 9 is a high-performance road bike designed for avid cyclists and competitive riders. It features an aerodynamic carbon fiber frame and fork, providing excellent stiffness and responsiveness.",
                            Price = 399.50,
                            ImageUrl = "/images/b1.jpg",
                            createdAt = DateTime.Now.AddDays(-10),
                            CategoryId = 7,
                            SupplierId = 6
                        },
                        new Product()
                        {
                            Title = " Specialized Roubaix Expert Road Bike",
                            Description = "The Specialized Roubaix Expert is designed for riders who prioritize comfort on long rides without. It features a FACT 10r carbon fiber frame with Future Shock technology.",
                            Price = 499.50,
                            ImageUrl = "/images/b2.jpg",
                            createdAt = DateTime.Now.AddDays(-5),
                            CategoryId = 7,
                            SupplierId = 4
                        },
                        new Product()
                        {
                            Title = "Yeti Cycles SB130 Carbon GX Eagle Mountain Bike",
                            Description = "The Yeti Cycles SB130 is a trail-oriented mountain bike that strikes a balance between climbing prowess. Its carbon fiber frame is known for its durability and lightweight construction.",
                            Price = 599.50,
                            ImageUrl = "/images/b3.jpg",
                            createdAt = DateTime.Now.AddDays(-2),
                            CategoryId = 8,
                            SupplierId = 4
                        },
                        new Product()
                        {
                            Title = "Specialized S-Works Enduro Mountain Bike",
                            Description = "The Specialized S-Works Enduro is a high-end mountain bike designed for aggressive trail riding and enduro racing. Its FACT 11m carbon fiber frame provides strength and stiffness while keeping the overall weight low.",
                            Price = 519.50,
                            ImageUrl = "/images/b4.jpg",
                            createdAt = DateTime.Now.AddDays(-19),
                            CategoryId = 8,
                            SupplierId = 5
                        },
                        new Product()
                        {
                            Title = "Cannondale Quick CX 3 Hybrid Bike",
                            Description = "The Cannondale Quick CX 3 is a versatile hybrid bike that seamlessly blends features of both road and mountain bikes, making it suitable for a variety of riding conditions.",
                            Price = 100.50,
                            ImageUrl = "/images/b5.jpg",
                            createdAt = DateTime.Now.AddDays(-1),
                            CategoryId = 9,
                            SupplierId = 5
                        },
                        new Product()
                        {
                            Title = "Trek FX 3 Disc Hybrid Bike",
                            Description = "The Trek FX 3 Disc is a performance-oriented hybrid bike designed for fitness, commuting, and leisure riding. Its lightweight Alpha Gold Aluminum frame offers a comfortable geometry for a smooth and efficient ride.",
                            Price = 100.50,
                            ImageUrl = "/images/b6.png",
                            createdAt = DateTime.Now.AddDays(-1),
                            CategoryId = 9,
                            SupplierId = 6
                        },
                    });
                    context.SaveChanges();
                }


            }

        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@eproducts.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@eproducts.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
