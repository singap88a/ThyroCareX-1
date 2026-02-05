using Microsoft.Extensions.DependencyInjection;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.InfrastructureBases;
using ThyroCareX.Infrastructure.Repository;

namespace ThyroCareX.Infrastructure
{
    public static class ModuleInfrustructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();


            services.AddScoped(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

            return services;

        }
    }
}
