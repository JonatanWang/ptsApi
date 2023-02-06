using Microsoft.EntityFrameworkCore;

public interface IApplicationRepository
{
    Task<List<ApplicationDto>> GetAllApplications();
    Task<ApplicationDto> AddApplication(ApplicationDto dto);
    Task<ApplicationDto?> GetApplication(int id);
};

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext context;
    public ApplicationRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(ApplicationDbContext));
    }

    public async Task<ApplicationDto> AddApplication(ApplicationDto dto)
    {
        var entity = new ApplicationEntity();
        ApplicationExtensions.DtoToEntity(dto, entity);
        var addResult = context.Add(entity);
        await context.SaveChangesAsync();

        return ApplicationExtensions.ToDto(addResult.Entity);
    }

    public async Task<List<ApplicationDto>> GetAllApplications()
    {
        return await context.Applications.Select(a => 
        new ApplicationDto(a.Id, a.Name, a.Company, a.MobileNumber, a.Email, a.ParticipationForm))
        .ToListAsync();
    }

    public async Task<ApplicationDto?> GetApplication(int id)
    {
        var entity = await context.Applications.SingleOrDefaultAsync(a => a.Id == id);
        if(entity == null) return null;

        return ApplicationExtensions.ToDto(entity);
    }
}