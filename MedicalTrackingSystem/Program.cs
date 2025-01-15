using MedicalTrackingSystem.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  // MVC desteği ekleniyor

// SQLite için DbContext'i yapılandırıyoruz
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=medicaltracking.db"));  // SQLite bağlantı dizesi

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    _ = app.UseDeveloperExceptionPage();
}
else
{
    _ = app.UseExceptionHandler("/Home/Error");
    _ = app.UseHsts();
}

// HTTPS yönlendirmesini aktif hale getiriyoruz
_ = app.UseHttpsRedirection();

// Statik dosyaların servis edilmesi için
app.UseStaticFiles();

// Routing işlemleri için
app.UseRouting();

// Authorization (yetkilendirme) işlemleri
app.UseAuthorization();

// Controller yönlendirmeleri
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Uygulamayı çalıştırıyoruz
app.Run();
