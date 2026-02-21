using SummerSchool.ApplicationSystems.Mvc.Common.Constants;

var builder = WebApplication.CreateBuilder(args);

ServicesSection(builder.Services);
var app = builder.Build();
UseSection(app);
app.Run();


void ServicesSection(IServiceCollection services)
{
    services.AddControllersWithViews();
    services.AddHttpContextAccessor();

    services.AddHttpClient(HttpClientNames.API_CLIENT, x =>
    {
        x.BaseAddress = new Uri(builder.Configuration[ConfigurationKeys.API_URL].ToString());
        x.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    services.AddAuthentication(CookieAuthenticationConstants.STUDENT_SCHEME)
        .AddCookie(CookieAuthenticationConstants.STUDENT_SCHEME, options =>
        {
            options.Cookie.Name = CookieAuthenticationConstants.STUDENT_COOKIE_NAME;
            options.LoginPath = RouteConstants.AUTH_LOGIN;
            options.LogoutPath = RouteConstants.AUTH_LOGOUT;
            options.AccessDeniedPath = RouteConstants.ACCESS_DENIED;
            options.Cookie.Path = CookieAuthenticationConstants.STUDENT_COOKIE_PATH;
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.IsEssential = true;
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        })
        .AddCookie(CookieAuthenticationConstants.ADMIN_SCHEME, options =>
        {
            options.Cookie.Name = CookieAuthenticationConstants.ADMIN_COOKIE_NAME;
            options.LoginPath = RouteConstants.ADMIN_LOGIN;
            options.LogoutPath = RouteConstants.ADMIN_LOGOUT;
            options.AccessDeniedPath = RouteConstants.ADMIN_ACCESS_DENIED;
            options.Cookie.Path = CookieAuthenticationConstants.ADMIN_COOKIE_PATH;
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.IsEssential = true;
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        });

    services.AddAuthorization();
}

void UseSection(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler(RouteConstants.ERROR_500);
        app.UseHsts();
    }
    else
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStatusCodePagesWithRedirects("/Error/{0}");
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}
