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
    public class MembershipService : IMembershipService
    {
        private readonly IMapper _mapper;

        public MembershipService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<bool> AddMember(long userId, long groupId)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var membershipRepo = new MembershipsRepository(uow);
                MembershipEntity membership = await membershipRepo.Get(userId, groupId);

                if (membership == null)
                {
                    membership = new MembershipEntity
                    {
                        UserId = userId,
                        GroupId = groupId,
                        Status = 1
                    };

                    await membershipRepo.Add(membership);
                    uow.Commit();
                }
            }

            return true;
        }

        public async Task<IEnumerable<MembershipDTO>> GetAllMembershipsOfUser(long userId)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var repo = new MembershipsRepository(uow);
                IEnumerable<MembershipEntity> entities = await repo.GetAllOfUser(userId);
                IEnumerable<MembershipDTO> dtos = _mapper.Map<IEnumerable<MembershipDTO>>(entities);
                return dtos;
            }
        }

        public async Task<IEnumerable<MembershipDTO>> GetAllMembershipsOfGroup(long groupId)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var repo = new MembershipsRepository(uow);
                IEnumerable<MembershipEntity> entities = await repo.GetAllOfGroup(groupId);
                IEnumerable<MembershipDTO> dtos = _mapper.Map<IEnumerable<MembershipDTO>>(entities);
                return dtos;
            }
        }

        public async Task<bool> RemoveMember(long userId, long groupId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateMemberStatus(long userId, long groupId)
        {
            throw new NotImplementedException();
        }
    }
}
