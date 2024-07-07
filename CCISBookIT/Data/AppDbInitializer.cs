using CCISBookIT.Data.Enum;
using CCISBookIT.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Identity;

namespace CCISBookIT.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            var adminRole = new IdentityRole { Name = UserRole.Admin.ToString(), NormalizedName = UserRole.Admin.ToString().ToUpper() };
            var facultyRole = new IdentityRole { Name = UserRole.Faculty.ToString(), NormalizedName = UserRole.Faculty.ToString().ToUpper() };
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //Rooms
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

                //Users FuLLName = First Name, Middle Name, Last Name
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            FacultyID = "CCISF001",
                            FullName = "Maloi Ricalde Manzana",
                            Email = "maloimanzana@gmail.com",
                            PhoneNumber = "09171234567",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Maloi@567"),
                            Role = UserRole.Faculty,
                        },
                        new User()
                        {
                            FacultyID = "CCISF002",
                            FullName = "Chaeyoung Marie Reyes Mendoza",
                            Email = "chaemacle@gmail.com",
                            PhoneNumber = "09182345678",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Chaeyoung@567"),
                            Role = UserRole.Faculty,
                        },
                        new User()
                        {
                            FacultyID = "CCISF003",
                            FullName = "Sheena Mae Burgos Catacutan",
                            Email = "shecaminute@gmail.com",
                            PhoneNumber = "09193456789",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Sheena@789"),
                            Role = UserRole.Faculty,
                        },
                        new User()
                        {
                            FacultyID = "CCISF004",
                            FullName = "Ariana Grande Bautista",
                            Email = "arigaba@gmail.com",
                            PhoneNumber = "09204567890",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Ariana@890"),
                            Role = UserRole.Faculty,
                        },
                        new User()
                        {
                            FacultyID = "CCISF005",
                            FullName = "Frank Morales Ocean",
                            Email = "frankoceanlover911@gmail.com",
                            PhoneNumber = "09215678901",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Frank@901"),
                            Role = UserRole.Faculty,
                        },new User()
                        {
                            FacultyID = "CCISF006",
                            FullName = "Freddie Mercury Watson",
                            Email = "freddiequeen@gmail.com",
                            PhoneNumber = "09226789012",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Freddie@012"),
                            Role = UserRole.Faculty ,
                        },new User()
                        {
                            FacultyID = "CCISF007",
                            FullName = "Lebron James Bryan",
                            Email = "Lebron@gmail.com",
                            PhoneNumber = "09237890123",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Lebron@123"),
                            Role = UserRole.Faculty ,
                        },
                        new User()
                        {
                            FacultyID = "CCISF008",
                            FullName = "Renz Niño Baladjay",
                            Email = "renznino@gmail.com",
                            PhoneNumber = "09248901234",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Renz@234"),
                            Role = UserRole.Faculty,
                        },
                        new User()
                        {
                            FacultyID = "CCISF009",
                            FullName = "Jamescarl Quitarinio Dean",
                            Email = "jamescarl@gmail.com",
                            PhoneNumber = "09259012345",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("James@345"),
                            Role = UserRole.Faculty,
                        },
                        new User()
                        {
                            FacultyID = "CCISF010",
                            FullName = "Michelle Clemente Lopez",
                            Email = "michellelopez@gmail.com",
                            PhoneNumber = "09260123456",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Michelle@456"),
                            Role = UserRole.Faculty,
                        },
                        new User()
                        {
                            FacultyID = "CCISA011",
                            FullName = "Zabdiel Joseph De Belen Manzana",
                            Email = "zabdielpogi123@gmail.com",
                            PhoneNumber = "09271234567",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Zabdiel@567"),
                            Role = UserRole.Admin,
                        }
                    });
                    context.SaveChanges();
                }
                //Bookinga
                if (!context.Bookings.Any())
                {
                    var rooms = context.Rooms.ToList();
                    //BookingId = $"{RoomNo}_{Date:yyyyMMdd}_{StartTime:HHmm}";
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
                            FacultyId = "CCISF001",
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
                            FacultyId = "CCISF004",
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
                            FacultyId = "CCISF006",
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
                            FacultyId = "CCISF002",
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
                            FacultyId = "CCISF005",
                            Room = rooms.FirstOrDefault(r => r.RoomNo == "S503B")
                        }
                    });
                    context.Roles.Add(adminRole);
                    context.Roles.Add(facultyRole);
                    context.SaveChanges();
                }

            }
        }
    }
}

