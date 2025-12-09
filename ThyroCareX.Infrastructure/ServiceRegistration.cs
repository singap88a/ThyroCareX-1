using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models.Identity;
using ThyroCareX.Infrastructure.Context;

namespace ThyroCareX.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegisteration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddIdentity<User, IdentityRole<int>>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = true; // لازم يكون فيه رقم واحد على الأقل
                option.Password.RequireLowercase = true; // لازم يكون فيه حرف صغير
                option.Password.RequireNonAlphanumeric = true; // لازم يكون فيه رمز مثل !@#$
                option.Password.RequireUppercase = true; // لازم يكون فيه حرف كبير
                option.Password.RequiredLength = 6; // طول كلمة السر أقل حاجة 6 حروف
                option.Password.RequiredUniqueChars = 1; // لازم يكون فيه حرف فريد واحد على الأقل

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // لو فشلوا في الدخول أكثر من مرة، الحساب بيتقفل 5 دقايق
                option.Lockout.MaxFailedAccessAttempts = 5; // عدد المحاولات الفاشلة قبل الحظر
                option.Lockout.AllowedForNewUsers = true; // الحظر ينطبق كمان على المستخدمين الجدد

                // User settings.
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                // الحروف المسموح بيها في اسم المستخدم
                option.User.RequireUniqueEmail = true; // كل مستخدم لازم يكون عنده إيميل فريد
                option.SignIn.RequireConfirmedEmail = true; // لازم المستخدم يأكد الإيميل قبل الدخول

            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}
