using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;
using SummerSchool.ApplicationSystems.Core.Services.Base;

namespace SummerSchool.ApplicationSystems.Core.Services.Student;

public interface IStudentCommandService : IBaseService<Entities.Student>
{
    Task<ServiceResponseDto> CreateStudentAsync(CreateStudentRequestDto request, CancellationToken cancellationToken);
    Task<ServiceResponseDto> UpdateStudentAsync(UpdateStudentRequestDto request, CancellationToken cancellationToken);
}
