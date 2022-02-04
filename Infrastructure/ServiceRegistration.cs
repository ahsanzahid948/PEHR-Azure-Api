using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Auth;
using Application.Interfaces.Repositories.Support;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Auth.Client;
using Infrastructure.Repositories.Auth.MenuRole;
using Infrastructure.Repositories.Auth.Provider;
using Infrastructure.Repositories.Support;
using Infrastructure.Repositories.Support.Audit;
using Infrastructure.Repositories.Support.Entity;
using Infrastructure.Repositories.Support.EVV;
using Infrastructure.Repositories.Support.Insurance;
using Infrastructure.Repositories.Support.Practice;
using Infrastructure.Repositories.Support.Subscription;
using Infrastructure.Repositories.Support.TreatWrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositories
            services.AddTransient<IApiUserRepositoryAsync, ApiUserRepositoryAsync>();
            services.AddTransient<IKioskRepositoryAsync, KioskRepositoryAsync>();
            services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();
            services.AddTransient<IEntityRepositoryAsync, EntityRepositoryAsync>();
            services.AddTransient<IUserTokenRepositoryAsync, UserTokenRepositoryAsync>();
            services.AddTransient<ISupportUserRepositoryAsync, SupportUserRepositoryAsync>();
            services.AddTransient<ITaskRepositoryAsync, TaskRepositoryAsync>();
            services.AddTransient<IClientRepositoryAsync, ClientRepositoryAsync>();
            services.AddTransient<INoteRepositoryAsync, NoteRepositoryAsync>();
            services.AddTransient<ITicketRepositoryAsync, TicketRepositoryAsync>();
            services.AddTransient<ISupportEntityRepositoryAsync, SupportEntityRepositoryAsync>();
            services.AddTransient<ISubscriptionRepositoryAsync, SubscriptionRepositoryAsync>();
            services.AddTransient<IMenuRoleRepositoryAsync, MenuRoleRepositoryAsync>();
            services.AddTransient<IPracticeRepositoryAsync, PracticeRepositoryAsync>();
            services.AddTransient<IProviderRepositoryAsync, ProviderRepositoryAsync>();
            services.AddTransient<IEvvRepositoryAsync, EvvRepositoryAsync>();
            services.AddTransient<IAuthClientRepositoryAsync, AuthClientRepositoryAsync>();
            services.AddTransient<IInsuranceRepositoryAsync, InsuranceRepositoryAsync>();
            services.AddTransient<ISupportPracticeRepositoryAsync, SupportPracticeRepositoryAsync>();
            services.AddTransient<ITreatWriteRepositoryAsync, TreatWriteRepositoryAsync>();
            services.AddTransient<ICommentRepositoryAsync, CommentRepositoryAsync>();
            services.AddTransient<IAuditRepositoryAsync, AuditRepositoryAsync>();
            #endregion Repositories
        }
    }
}