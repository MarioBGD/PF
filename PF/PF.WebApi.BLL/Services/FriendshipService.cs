using AutoMapper;
using PF.DTO.Users;
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
            UserDTO reciverUser = null;

            using (IUnitOfWork uow = new UnitOfWork())
            {
                UsersRepository userRepo = new UsersRepository(uow);
                reciverUser = await userRepo.GetByLogin(reciverUsername);
            }

            if (reciverUser == null)
            {
                return null;
            }

            var reslt = await AddFriendById(senderUserId, reciverUser.Id);
            return reslt;
        }
    }
}
