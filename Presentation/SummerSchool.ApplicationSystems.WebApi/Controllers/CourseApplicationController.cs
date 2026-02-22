using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SummerSchool.ApplicationSystems.Core.DTOs.CourseApplication.Request;
using SummerSchool.ApplicationSystems.Core.Services.CourseApplication;
using System.Net;

namespace SummerSchool.ApplicationSystems.WebApi.Controllers;

/// <summary>
/// Ders başvuru işlemlerini yöneten controller
/// </summary>
[ApiController]
[Route("api/")]
[Produces("application/json")]
[Authorize]
public class CourseApplicationController(ICourseApplicationQueryService courseApplicationQueryService,
                                         ICourseApplicationCommandService courseApplicationCommandService,
                                         IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
{
    private readonly ICourseApplicationQueryService _courseApplicationQueryService = courseApplicationQueryService;
    private readonly ICourseApplicationCommandService _courseApplicationCommandService = courseApplicationCommandService;

    /// <summary>
    /// Giriş yapan öğrencinin tüm başvurularını listeler
    /// </summary>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Başvuru listesi</returns>
    /// <remarks>
    /// JWT token'dan öğrenci bilgisi alınarak sadece kendi başvuruları gösterilir.
    /// 
    /// Başvuru durumları:
    /// - 1: Başvuruldu
    /// - 2: Onaylandı
    /// - 3: Reddedildi
    /// </remarks>
    /// <response code="200">Başvuru listesi başarıyla döndürüldü</response>
    /// <response code="401">Yetkisiz erişim</response>
    /// <response code="400">İşlem sırasında kontrol hatası dönüldü</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpGet("me/applications")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourseApplicationsByStudentId(CancellationToken cancellationToken)
    {
        var response = await this._courseApplicationQueryService.GetCourseApplicationsByStudentIdAsync(cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Belirli bir derse yapılan tüm başvuruları listeler (Sadece Admin)
    /// </summary>
    /// <param name="courseId">Ders ID</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Başvuru listesi</returns>
    /// <remarks>
    /// Sadece admin kullanıcıları bu endpoint'e erişebilir.
    /// </remarks>
    /// <response code="200">Başvuru listesi başarıyla döndürüldü</response>
    /// <response code="401">Yetkisiz - Sadece admin erişebilir</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpGet("courses/{courseId}/applications")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCourseApplicationsByCourseId([FromRoute] Guid courseId, CancellationToken cancellationToken)
    {
        var response = await this._courseApplicationQueryService.GetCourseApplicationsByCourseIdAsync(courseId, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Derse başvuru yapar
    /// </summary>
    /// <param name="request">Ders ID içeren başvuru isteği</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Başvuru sonucu</returns>
    /// <remarks>
    /// İş kuralları:
    /// - Bir öğrenci aynı derse sadece 1 kez başvurabilir
    /// - Kontenjanı dolu derslere başvuru yapılamaz
    /// - Başvuru durumu otomatik olarak "Başvuruldu (1)" olarak ayarlanır
    /// 
    /// Örnek istek:
    /// 
    ///     POST /api/course-applications
    ///     {
    ///         "courseId": "guid"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Başvuru başarıyla oluşturuldu</response>
    /// <response code="400">Geçersiz istek - Kontenjan dolu veya zaten başvurulmuş</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPost("course-applications")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateCourseApplication([FromBody] CreateCourseApplicationRequestDto request, CancellationToken cancellationToken)
    {
        var response = await this._courseApplicationCommandService.CreateCourseApplicationAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }

    /// <summary>
    /// Başvuru durumunu günceller - Onayla/Reddet (Sadece Admin)
    /// </summary>
    /// <param name="id">Başvuru ID</param>
    /// <param name="request">Yeni durum ve red açıklaması</param>
    /// <param name="cancellationToken">İptal token</param>
    /// <returns>Güncelleme sonucu</returns>
    /// <remarks>
    /// Başvuru durumları:
    /// - 2: Onaylandı
    /// - 3: Reddedildi (RejectDescription zorunlu)
    /// 
    /// Örnek istek (Onay):
    /// 
    ///     PUT /api/course-applications/{id}/status
    ///     {
    ///         "applicationStatus": 2
    ///     }
    /// 
    /// Örnek istek (Red):
    /// 
    ///     PUT /api/course-applications/{id}/status
    ///     {
    ///         "applicationStatus": 3,
    ///         "rejectDescription": "Kontenjan dolu"
    ///     }
    /// 
    /// </remarks>
    /// <response code="200">Başvuru durumu başarıyla güncellendi</response>
    /// <response code="401">Yetkisiz - Sadece admin erişebilir</response>
    /// <response code="204">Başvuru bulunamadı</response>
    /// <response code="500">Sunucu hatası</response>
    [HttpPut("course-applications/{id}/status")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UpdateApplicationStatus([FromRoute] Guid id, [FromBody] UpdateCourseApplicationStatusRequestDto request, CancellationToken cancellationToken)
    {
        request.Id = id;

        var response = await this._courseApplicationCommandService.UpdateApplicationStatusAsync(request, cancellationToken).ConfigureAwait(false);

        return this.CreateJsonResponse(response);
    }
}
