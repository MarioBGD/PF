using AutoMapper;
using PF.DTO.Expenses;
using PF.DTO.Groups;
using PF.DTO.Users;
using PF.WebApi.DAL.Entities;
using PF.WebApi.DAL.Entities.Procedures;

namespace PF.WebApi.BLL.Core.Mapping
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthDTO, AuthEntity>();
                cfg.CreateMap<AuthEntity, AuthDTO>();

                cfg.CreateMap<UserDTO, UserEntity>();
                cfg.CreateMap<UserEntity, UserDTO>();

                cfg.CreateMap<FriendshipDTO, FriendshipEntity>();
                cfg.CreateMap<FriendshipEntity, FriendshipDTO>();

                cfg.CreateMap<GroupDTO, GroupEntity>();
                cfg.CreateMap<GroupEntity, GroupDTO>();

                cfg.CreateMap<MembershipDTO, MembershipEntity>();
                cfg.CreateMap<MembershipEntity, MembershipDTO>();

                cfg.CreateMap<ExpenseDTO, ExpenseEntity>();
                cfg.CreateMap<ExpenseEntity, ExpenseDTO>();

                cfg.CreateMap<PaymentDTO, PaymentEntity>();
                cfg.CreateMap<PaymentEntity, PaymentDTO>();

                cfg.CreateMap<BalanceDTO, BalanceData>();
                cfg.CreateMap<BalanceData, BalanceDTO>();

                cfg.CreateMap<GroupCurrencyDTO, GroupCurrencyEntity>();
                cfg.CreateMap<GroupCurrencyEntity, GroupCurrencyDTO>();
            })
            .CreateMapper();
        }
    }
}
