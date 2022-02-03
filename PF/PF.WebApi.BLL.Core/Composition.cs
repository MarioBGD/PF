using Microsoft.Extensions.DependencyInjection;
using PF.WebApi.BLL.Contracts;
using PF.WebApi.BLL.Core.Mapping;
using PF.WebApi.BLL.Core.Services;

namespace PF.WebApi.BLL.Core
{
    public static class Composition
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddSingleton(AutoMapperConfig.Initialize());

            return services;
        }
    }
}
