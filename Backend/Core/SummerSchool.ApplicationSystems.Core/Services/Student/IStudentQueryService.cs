using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Response;
using SummerSchool.ApplicationSystems.Core.Services.Base;

namespace SummerSchool.ApplicationSystems.Core.Services.Student;

public interface IStudentQueryService : IBaseService<Entities.Student>
{
    Task<ServiceResponseDto<StudentResponseDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
