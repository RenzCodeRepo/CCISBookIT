using CCISBookIT.Models;

namespace CCISBookIT.Data
{
    public class seed
    {
        public static void Populate(IApplicationBuilder applicationBuilder)
        {
            using (var ServiceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = ServiceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.users.Any()) 
                {
                    context.users.AddRange(new List<User>()
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
