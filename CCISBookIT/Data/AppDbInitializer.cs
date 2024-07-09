using CCISBookIT.Data.Enum;
using CCISBookIT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CCISBookIT.Data
{
    public class AppDbInitializer
    {
        public static async Task Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string adminUserEmail = "renzbaladjay25@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        FacultyID = "CCISA001",
                        UserName = "renzbaladjay",  // Username without spaces
                        FullName = "Renz Niño Baladjay",  // Full name with spaces
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "09305215979"
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUser1Email = "michellelopez@gmail.com";
                var appUser1 = await userManager.FindByEmailAsync(appUser1Email);
                if (appUser1 == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FacultyID = "CCISF001",
                        UserName = "michellelopez",  // Username without spaces
                        FullName = "Michelle Kyla Lopez",  // Full name with spaces
                        Email = appUser1Email,
                        EmailConfirmed = true,
                        PhoneNumber = "09260123456"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUser2Email = "jamescarldean@gmail.com";
                var appUser2 = await userManager.FindByEmailAsync(appUser2Email);
                if (appUser2 == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FacultyID = "CCISF002",
                        UserName = "jamescarldean",  // Username without spaces
                        FullName = "Jamescarl Quitarinio Dean",  // Full name with spaces
                        Email = appUser2Email,
                        EmailConfirmed = true,
                        PhoneNumber = "09259012345"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appUser3Email = "zabdiel@gmail.com";
                var appUser3 = await userManager.FindByEmailAsync(appUser3Email);
                if (appUser3 == null)
                {
                    var newAppUser = new AppUser()
                    {
                        FacultyID = "CCISF003",
                        UserName = "zabdiel",  // Username without spaces
                        FullName = "Zabdiel Joseph Manzana",  // Full name with spaces
                        Email = appUser3Email,
                        EmailConfirmed = true,
                        PhoneNumber = "09271234567"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                if (!context.Rooms.Any())
                {
                    context.Rooms.AddRange(new List<Room>()
                    {
                        new Room { RoomNo = "S501", RoomType = RoomType.Laboratory.ToString() },
                        new Room { RoomNo = "S502", RoomType = RoomType.Laboratory.ToString() },
                        new Room { RoomNo = "S503", RoomType = RoomType.Laboratory.ToString() },
                        new Room { RoomNo = "S504", RoomType = RoomType.Lecture.ToString() },
                        new Room { RoomNo = "S505", RoomType = RoomType.Laboratory.ToString() },
                        new Room { RoomNo = "S508", RoomType = RoomType.Laboratory.ToString() },
                        new Room { RoomNo = "S510", RoomType = RoomType.Laboratory.ToString() },
                        new Room { RoomNo = "S511", RoomType = RoomType.Laboratory.ToString() }
                    });
                    context.SaveChanges();
                }

                if (!context.Bookings.Any())
                {
                    var rooms = context.Rooms.ToList();
                    context.Bookings.AddRange(new List<Booking>()
                    {
                        new Booking()
                        {
                            BookingId = "S501-20240521-0900",
                            Date = new DateTime(2024, 05, 21, 09, 00, 00),
                            StartTime = new TimeOnly(9,0),
                            Duration = 2,
                            EndTime = new TimeOnly(11,0),
                            Purpose = "Lecture",
                            Status = Status.Expired.ToString(),
                            RoomNo = "S501",
                            FacultyID = "CCISF003",
                            Room = rooms.FirstOrDefault(r => r.RoomNo == "S501")
                        },
                        new Booking()
                        {
                            BookingId = "S511-20240613-1800",
                            Date = new DateTime(2024, 06, 13, 18, 00, 00),
                            StartTime = new TimeOnly(18,0),
                            Duration = 3,
                            EndTime = new TimeOnly(21,0),
                            Purpose = "Laboratory",
                            Status = Status.Expired.ToString(),
                            RoomNo = "S511",
                            FacultyID = "CCISF002",
                            Room = rooms.FirstOrDefault(r => r.RoomNo == "S511")
                        },
                        new Booking()
                        {
                            BookingId = "S504-20240520-1630",
                            Date = new DateTime(2024, 05, 20, 16, 30, 00),
                            StartTime = new TimeOnly(16,30),
                            Duration = 3,
                            EndTime = new TimeOnly(19,30),
                            Purpose = "Lecture",
                            Status = Status.Cancelled.ToString(),
                            RoomNo = "S504",
                            FacultyID = "CCISF001",
                            Room = rooms.FirstOrDefault(r => r.RoomNo == "S504")
                        },
                        new Booking()
                        {
                            BookingId = "S510-20240520-1630",
                            Date = new DateTime(2024, 05, 20, 16, 30, 00),
                            StartTime = new TimeOnly(16,30),
                            Duration = 3,
                            EndTime = new TimeOnly(19,30),
                            Purpose = "Lecture",
                            Status = Status.Active.ToString(),
                            RoomNo = "S510",
                            FacultyID = "CCISF002",
                            Room = rooms.FirstOrDefault(r => r.RoomNo == "S510")
                        },
                        new Booking()
                        {
                            BookingId = "S503B-20240417-1030",
                            Date = new DateTime(2024, 05, 20, 10, 30, 00),
                            StartTime = new TimeOnly(10,30),
                            Duration = 2,
                            EndTime = new TimeOnly(12,30),
                            Purpose = "Lecture",
                            Status = Status.Cancelled.ToString(),
                            RoomNo = "S503",
                            FacultyID = "CCISF001",
                            Room = rooms.FirstOrDefault(r => r.RoomNo == "S503")
                        }
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
