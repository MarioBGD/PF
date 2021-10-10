using Microsoft.Extensions.DependencyInjection;
using PF.WebApi.BLL.Services;
using PF.WebApi.Infrastructure.Interfaces.IServices;
using PF.WebApi.Infrastructure.Maping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.BLL
{
    public static class DependencyInjection
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
