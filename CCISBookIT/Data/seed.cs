using CCISBookIT.Models;

namespace CCISBookIT.Data
{
    public class Seed
    {
        public static void Populate(IApplicationBuilder applicationBuilder)
        {
            using (var ServiceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = ServiceScope.ServiceProvider.GetService<ApplicationDbContext>();
                
                // Ensure the database is created
                context.Database.EnsureCreated();
                
                // Seed users if none exist
                if (!context.Users.Any()) 
                {
                    context.Users.AddRange(new List<User>()
                    {
                        new User()
                        {
                            FacultyID = "CCIS1",
                            FullName = "Renz Niño Baladjay",
                            Email = "renz@example.com",
                            PhoneNumber = "09123456789",
                            PasswordHash = "password"
                        }
                    });

                    context.SaveChanges();
                }  
            }
        }
    }
}
