using AutoMapper;
using PF.DTO.Groups;
using PF.WebApi.DAL;
using PF.WebApi.DAL.Repositories;
using PF.WebApi.Entities;
using PF.WebApi.Infrastructure.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.WebApi.BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IMapper _mapper;

        public GroupService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<GroupDTO> CreateGroup(GroupDTO group, long ownerUserId)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var groupsRepository = new GroupsRepository(uow);
                GroupEntity groupEntity = _mapper.Map<GroupEntity>(group);
                long groupId = await groupsRepository.Add(groupEntity);

                var membershipsRepo = new MembershipsRepository(uow);
                MembershipEntity membership = new MembershipEntity
                {
                    GroupId = groupId,
                    UserId = ownerUserId,
                    Status = 3
                };
                await membershipsRepo.Add(membership);

                if (group.Currencies != null && group.Currencies.Count > 0)
                {
                    
                    GroupCurrenciesRepository currenciesRepository = new GroupCurrenciesRepository(uow);
                    var currencyDTOs = group.Currencies.AsEnumerable();

                    foreach (var currency in currencyDTOs)
                    {
                        currency.GroupId = groupId;
                    }

                    var currencyEntities = _mapper.Map<IEnumerable<GroupCurrencyEntity>>(currencyDTOs);
                    await currenciesRepository.AddRange(currencyEntities);
                }
                

                uow.Commit();

                group.Id = groupId;
                return group;
            }
        }

        public async Task<IEnumerable<GroupDTO>> GetAllGroupsOfUser(long userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GroupDTO>> GetSelectedGroupsOfUser(long userId, List<long> groups)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var groupsRepository = new GroupsRepository(uow);

                IEnumerable<GroupEntity> groupsEntities = await groupsRepository.GetRange(groups);
                IEnumerable<GroupDTO> groupsDTOs = _mapper.Map<IEnumerable<GroupDTO>>(groupsEntities);

                GroupCurrenciesRepository currenciesRepository = new GroupCurrenciesRepository(uow);

                foreach (var group in groupsDTOs)
                {
                    var currencyEntities = await currenciesRepository.GetCurrenciesOfGroup(group.Id);
                    var currencyDTOs = _mapper.Map<IEnumerable<GroupCurrencyDTO>>(currencyEntities);
                    group.Currencies = currencyDTOs.ToList();
                }

                return groupsDTOs;
            }
        }
    }
}
