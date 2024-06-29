using CCISBookIT.Data.Enum;
using CCISBookIT.Models;

namespace CCISBookIT.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                //Bookinga
                if (!context.Bookings.Any())
                {
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
                            Status = Status.Expired,
                            RoomNo = "S501",
                            FacultyId = "CCISF001",
                        },

                        new Booking()
                        {
                            BookingId = "S511-20240613-1800",
                            Date = new DateTime(2024, 06, 13, 18, 00, 00),
                            StartTime = new TimeOnly(18,0),
                            Duration = 3,
                            EndTime = new TimeOnly(21,0),
                            Purpose = "Laboratory",
                            Status = Status.Expired,
                            RoomNo = "S511",
                            FacultyId = "CCISF004",
                        },

                         new Booking()
                        {
                            BookingId = "S504-20240520-1630",
                            Date = new DateTime(2024, 05, 20, 16, 30, 00),
                            StartTime = new TimeOnly(16,30),
                            Duration = 3,
                            EndTime = new TimeOnly(19,30),
                            Purpose = "Lecture",
                            Status = Status.Cancelled,
                            RoomNo = "S504",
                            FacultyId = "CCISF006",
                        },

                         new Booking()
                        {
                            BookingId = "S510-20240520-1630",
                            Date = new DateTime(2024, 05, 20, 16, 30, 00),
                            StartTime = new TimeOnly(16,30),
                            Duration = 3,
                            EndTime = new TimeOnly(19,30),
                            Purpose = "Lecture",
                            Status = Status.Cancelled,
                            RoomNo = "S510",
                            FacultyId = "CCISF002",
                        },

                        new Booking()
                        {
                            BookingId = "S503B-20240417-1030",
                            Date = new DateTime(2024, 05, 20, 10, 30, 00),
                            StartTime = new TimeOnly(10,30),
                            Duration = 2,
                            EndTime = new TimeOnly(12,30),
                            Purpose = "Lecture",
                            Status = Status.Cancelled,
                            RoomNo = "S503B",
                            FacultyId = "CCISF005",
                        }
                    });
                }

                //Users
                if (!context.Users.Any())
                {

                }

                //Rooms
                if (!context.Rooms.Any())
                {

                }
            }
        }
    }
}

