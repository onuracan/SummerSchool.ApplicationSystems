using SummerSchool.ApplicationSystems.Core.DTOs.Auth.Request;
using SummerSchool.ApplicationSystems.Core.DTOs.Base.Response;
using SummerSchool.ApplicationSystems.Core.Services.Auth;
using SummerSchool.ApplicationSystems.Core.Services.Student;
using SummerSchool.ApplicationSystems.Core.Services.Token;
using SummerSchool.ApplicationSystems.Shared.Enums;
using SummerSchool.ApplicationSystems.Shared.Models;

namespace SummerSchool.ApplicationSystems.Service.Services.Auth;

public class AuthService(IStudentQueryService studentQueryService,
                         ITokenService tokenService) : IAuthService
{
    private readonly IStudentQueryService _studentQueryService = studentQueryService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<ServiceResponseDto<UserModel>> StudentLoginAsync(StudentLoginRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._studentQueryService.GetFirstOrDefaultAsync(predicate: x => x.PhoneNumber == request.PhoneNumber, cancellationToken: cancellationToken).ConfigureAwait(false);

        var user = new UserModel()
        {
            Id = Guid.NewGuid(),
            PhoneNumber = request.PhoneNumber,
            UserType = UserType.Student
        };

        if (response.IsSuccessful)
        {
            user.Id = response.Result.Id;
            user.NameAndSurname = $"{response.Result.FirstName} {response.Result.LastName}";
            user.EMail = response.Result.EMail;
        }

        var tokenResult = this._tokenService.BuildToken(user);
        user.AccessToken = tokenResult.Item1;
        user.Expiration = tokenResult.Item2;

        return ServiceResponseDto<UserModel>.SetSuccess(user);
    }

    public ServiceResponseDto<UserModel> AdminLogin(AdminLoginRequestDto request)
    {
        if (request.UserName != "admin" && request.Password != "adminhalic")
            return ServiceResponseDto<UserModel>.SetFail(message: "Kullanıcı adınız ve parolanız yanlıştır. Lütfen bilgilerinizi kontrol ediniz.");

        var user = new UserModel()
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            NameAndSurname = request.Password,
            UserType = UserType.Admin
        };

        var tokenResult = this._tokenService.BuildToken(user);
        user.AccessToken = tokenResult.Item1;
        user.Expiration = tokenResult.Item2;

        return ServiceResponseDto<UserModel>.SetSuccess(user);
    }
}
