using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Seed(ModelBuilder builder)
    {
        builder
        .Entity<ApplicationEntity>()
        .HasData
        (
            new List<ApplicationEntity> 
            {
                new ApplicationEntity 
                {
                    Id = 1,
                    Name = "Anders GarnFors",
                    Company = "PTS",
                    MobileNumber = "0701234567",
                    Email = "anders.garnfors@pts.se",
                    ParticipationForm = ParticipationForm.IN_PERSON
                },
                new ApplicationEntity 
                {
                    Id = 2,
                    Name = "Camilla Denijs",
                    Company = "PTS",
                    MobileNumber = "0736405801",
                    Email = "camilla.denijs@pts.se",
                    ParticipationForm = ParticipationForm.AT_DISTANCE
                }
            }
        );
    }
}