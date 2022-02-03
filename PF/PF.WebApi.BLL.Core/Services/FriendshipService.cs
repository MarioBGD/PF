using AutoMapper;
using PF.DTO.Users;
using PF.WebApi.DAL.Core;
using PF.WebApi.DAL.Core.Repositories;
using PF.WebApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.BLL.Core.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IMapper _mapper;

        public FriendshipService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<FriendshipDTO>> GetFriendsOfUser(long userId)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var friendshipsRepo = new FriendshipsRepository(uow);
                IEnumerable<FriendshipEntity> friendshipEnities = await friendshipsRepo.GetAllFriendsOfUser(userId);
                IEnumerable<FriendshipDTO> friendshipDTOs = _mapper.Map<IEnumerable<FriendshipDTO>>(friendshipEnities);
                return friendshipDTOs;
            }
        }

        public async Task<bool> RemoveFriendship(long userOne, long userTwo)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                var friendshipsRepo = new FriendshipsRepository(uow);
                int affectedRows = await friendshipsRepo.DeleteFriendship(userOne, userTwo);
                uow.Commit();

                return affectedRows > 0;
            }
        }

        public async Task<FriendshipDTO> AddFriendById(long senderUserId, long reciverUserId)
        {
            FriendshipDTO friendshipDTO;

            using (IUnitOfWork uow = new UnitOfWork())
            {
                var friendshipsRepo = new FriendshipsRepository(uow);
                FriendshipEntity friendshipEntity = await friendshipsRepo.GetFriendship(senderUserId, reciverUserId);

                if (friendshipEntity != null)
                {
                    friendshipDTO = _mapper.Map<FriendshipDTO>(friendshipEntity);

                    if (friendshipDTO.Status == FriendshipDTO.FriendshipStatus.Invited &&
                        friendshipDTO.UserTwoId == senderUserId)
                    {
                        friendshipEntity.Status = (int)FriendshipDTO.FriendshipStatus.Accepted;
                        await friendshipsRepo.Update(friendshipEntity);
                        uow.Commit();

                        friendshipDTO = _mapper.Map<FriendshipDTO>(friendshipEntity);
                    }
                }
                else
                {
                    friendshipDTO = new FriendshipDTO
                    {
                        UserOneId = senderUserId,
                        UserTwoId = reciverUserId,
                        Status = FriendshipDTO.FriendshipStatus.Invited,
                    };

                    friendshipEntity = _mapper.Map<FriendshipEntity>(friendshipDTO);
                    friendshipEntity.CreatedDate = DateTime.Now;

                    await friendshipsRepo.Add(friendshipEntity);
                    uow.Commit();
                }
            }

            return friendshipDTO;
        }

        public async Task<FriendshipDTO> AddFriendByName(long senderUserId, string reciverUsername)
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                UsersRepository userRepo = new UsersRepository(uow);
                var reciverUserEntity = await userRepo.GetByLogin(reciverUsername);

                if (reciverUserEntity == null)
                {
                    return null;
                }

                var reslt = await AddFriendById(senderUserId, reciverUserEntity.Id);
                return reslt;
            }
        }
    }
}
