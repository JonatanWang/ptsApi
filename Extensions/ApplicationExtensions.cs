public class ApplicationExtensions
{

    public static void DtoToEntity(ApplicationDto dto, ApplicationEntity entity)
    {
        entity.Name = dto.Name;
        entity.Company = dto.Company;
        entity.MobileNumber = dto.MobileNumber;
        entity.Email = dto.Email;
        entity.ParticipationForm = dto.ParticipationForm;
    }

    public static ApplicationDto ToDto(ApplicationEntity entity)
    {
        return new ApplicationDto
        (
            entity.Id,
            entity.Name,
            entity.Company,
            entity.MobileNumber,
            entity.Email,
            entity.ParticipationForm
        );
    }
}