using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication_C14.Entities;
using WebApplication_C14.interfaces;
using WebApplication_C14.server;
using WebApplication_C14.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserDb>();
//این خط کد، یک سیستم مدیریت کاربران کامل را به برنامه شما اضافه می‌کند
builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<UserDb>().AddDefaultTokenProviders();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    //تعداد بار های ناموفق
    options.Lockout.MaxFailedAccessAttempts = 5;
    //زمان قفل شدن 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    //این تنظیم مشخص می‌کند که آیا حساب‌های کاربری جدید نیز مشمول قفل شدن هستند یا خیر
    options.Lockout.AllowedForNewUsers = true;
    //این تنظیم مشخص می‌کند که رمز عبور باید حداقل شامل یک عدد باشد. این یک الزام امنیتی است که باعث افزایش پیچیدگی رمز عبور و کاهش احتمال هک شدن می‌شود.
    options.Password.RequireDigit = true;
    //این تنظیم مشخص می‌کند که رمز عبور باید حداقل شامل یک حرف کوچک باشد
    options.Password.RequireLowercase = true;
    //این تنظیم مشخص می‌کند که آیا رمز عبور باید شامل حداقل یک کاراکتر غیر الفبایی و عددی باشد یا خیر.
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    //این تنظیم مشخص می‌کند که کاربرانی که می‌خواهند در سیستم شما حساب ایجاد کنند، باید رمز عبوری حداقل به طول 6 کاراکتر انتخاب کنند.
    options.Password.RequiredLength = 20;
    //این تنظیم مشخص می‌کند که رمز عبور باید حداقل شامل یک کاراکتر منحصر به فرد باشد. به عبارت دیگر، حداقل یک کاراکتر در رمز عبور باید حداقل دو بار تکرار نشود
    options.Password.RequiredUniqueChars = 1;


});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.",
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
{
    {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Reference = new Microsoft.OpenApi.Models.OpenApiReference
            {
                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }
});
});
builder.Services.AddSingleton<IUserPRP, UserService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dfgkjropjowepjforepit598uy68t59i40fklrmthblkmsfl;dfropw8ueowkopjkfmvkbmklhmo"))
    };
});

var app = builder.Build();
builder.Services.AddAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();
