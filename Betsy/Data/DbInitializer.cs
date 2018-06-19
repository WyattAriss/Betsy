using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Betsy.Models;
using Microsoft.AspNetCore.Identity;

namespace Betsy.Data {
    public class DbInitializer {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager) {
            context.Database.EnsureCreated();

            // Look for any fights.
            if (context.Fight.Any()) {
                return; // DB has been seeded
            }

            string role1 = "Admin";
            if (await roleManager.FindByNameAsync(role1) == null) {
                await roleManager.CreateAsync(new IdentityRole(role1));
            }

            string role2 = "Member";
            if (await roleManager.FindByNameAsync(role2) == null) {
                await roleManager.CreateAsync(new IdentityRole(role2));
            }

            string password = "P@$$w0rd";

            if (await userManager.FindByNameAsync("m@m.m") == null) {
                var user = new ApplicationUser {
                    UserName = "m@m.m",
                    Email = "m@m.m",
                    Money = 1000
                };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }

            if (await userManager.FindByNameAsync("a@a.a") == null) {
                var user = new ApplicationUser {
                    Email = "a@a.a",
                    UserName = "a@a.a",
                    Money = 1000
                };

                var result = await userManager.CreateAsync(user);
                if (result.Succeeded) {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            var boats = new List<Fight> {
                new Fight {
                    Fighter1 = "Donald Cerrone",
                    Fighter2 = "Leon Edwards",
                    Event = "UFC Fight Night: Cowboy vs Edwards",
                    Odds1 = 115,
                    Odds2 = -129
                },
                new Fight {
                    Fighter1 = "Ovince Saint Preux",
                    Fighter2 = "Tyson Pedro",
                    Event = "UFC Fight Night: Cowboy vs Edwards",
                    Odds1 = 114,
                    Odds2 = -141
                },
                new Fight {
                    Fighter1 = "Jessica-Rose Clark",
                    Fighter2 = "Jessica Eye",
                    Event = "UFC Fight Night: Cowboy vs Edwards",
                    Odds1 = -156,
                    Odds2 = 127
                },
                new Fight {
                    Fighter1 = "Daichi Abe",
                    Fighter2 = "Li Jingliang",
                    Event = "UFC Fight Night: Cowboy vs Edwards",
                    Odds1 = 240,
                    Odds2 = -320
                }
            };
            context.AddRange(boats);
            context.SaveChanges();
        }
    }
}
