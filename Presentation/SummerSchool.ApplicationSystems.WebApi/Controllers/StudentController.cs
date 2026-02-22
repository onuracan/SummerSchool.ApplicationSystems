using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.Student.Request;
using SummerSchool.ApplicationSystems.Core.Services.Student;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

/// <summary>
/// Öğrenci yönetim işlemlerini yöneten controller
/// </summary>
[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class StudentController(IStudentQueryService studentQueryService,
                               IStudentCommandService studentCommandService,
                               IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly IStudentQueryService _studentQueryService = studentQueryService;
    private readonly IStudentCommandService _studentCommandService = studentCommandService;

    /// <summary>
    /// ID'ye göre öğrenci bilgisini getirir
    /// </summary>
    /// <param name="id">Öğrenci ID</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Öğrenci bilgileri</returns>
    /// <remarks>
    /// Örnek kullanım:
    /// 
    ///     GET /api/student?id=3fa85f64-5717-4562-b3fc-2c963f66afa6
    /// 
    /// Dönen bilgiler:
    /// - Ad, Soyad
    /// - TC Kimlik No, Öğrenci No
    /// - Bölüm, Fakülte
    /// - GSM, E-posta
    /// - Ülke kodu
    /// </remarks>
    /// <response code="200">Öğrenci başarıyla döndürüldü</response>
    /// <response code="204">Öğrenci bulunamadı</response>
    /// <response code="401">Yetkisiz erişim - Token gerekli</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpGet("student")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var response = await this._studentQueryService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Yeni öğrenci kaydı oluşturur
    /// </summary>
    /// <param name="request">Öğrenci bilgileri</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Oluşturma sonucu</returns>
    /// <remarks>
    /// Yeni öğrenci kaydı oluşturur. GSM ile giriş yapan öğrenciler için otomatik kayıt yapılır.
    /// 
    /// Örnek istek:
    /// 
    ///     POST /api/students
    ///     {
    ///         "firstName": "Ahmet",
    ///         "lastName": "Yılmaz",
    ///         "identityNumber": "12345678901",
    ///         "schoolNumber": "2021001234",
    ///         "department": "Bilgisayar Mühendisliği",
    ///         "faculty": "Mühendislik Fakültesi",
    ///         "phoneNumber": "5551234567",
    ///         "eMail": "ahmet.yilmaz@halic.edu.tr",
    ///         "countryCode": "TR"
    ///     }
    /// 
    /// **Not:** GSM numarası zorunludur.
    /// </remarks>
    /// <response code="200">Öğrenci başarıyla oluşturuldu</response>
    /// <response code="400">Geçersiz istek - Zorunlu alanlar eksik</response>
    /// <response code="401">Yetkisiz erişim</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPost("students")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._studentCommandService.CreateStudentAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Mevcut öğrenci bilgilerini günceller
    /// </summary>
    /// <param name="id">Öğrenci ID</param>
    /// <param name="request">Güncellenmiş öğrenci bilgileri</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Güncelleme sonucu</returns>
    /// <remarks>
    /// Öğrenci profil bilgilerini günceller.
    /// 
    /// Örnek istek:
    /// 
    ///     PUT /api/students/3fa85f64-5717-4562-b3fc-2c963f66afa6
    ///     {
    ///         "firstName": "Ahmet",
    ///         "lastName": "Yılmaz",
    ///         "identityNumber": "12345678901",
    ///         "schoolNumber": "2021001234",
    ///         "department": "Yazılım Mühendisliği",
    ///         "faculty": "Mühendislik Fakültesi",
    ///         "phoneNumber": "5551234567",
    ///         "eMail": "ahmet.yilmaz@halic.edu.tr",
    ///         "countryCode": "TR"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Öğrenci başarıyla güncellendi</response>
    /// <response code="404">Öğrenci bulunamadı</response>
    /// <response code="400">Geçersiz istek</response>
    /// <response code="401">Yetkisiz erişim</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPut("students/{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] UpdateStudentRequestDto request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await this._studentCommandService.UpdateStudentAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}
