using AutoMapper;
using PF.DTO.Users;
using PF.WebApi.DAL.Core;
using PF.WebApi.DAL.Core.Repositories;
using PF.WebApi.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using PF.WebApi.BLL.Contracts;

namespace PF.WebApi.BLL.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers(List<long> ids)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                UsersRepository usersRepo = new UsersRepository(uow);

                IEnumerable<UserEntity> usersEntities = await usersRepo.GetRange(ids);
                IEnumerable<UserDTO> usersDto = _mapper.Map<IEnumerable<UserDTO>>(usersEntities);

                return usersDto;
            }
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                UsersRepository usersRepo = new UsersRepository(uow);

                UserEntity usersEntity = _mapper.Map<UserEntity>(userDTO);
                await usersRepo.Update(usersEntity);

                uow.Commit();

                return userDTO;
            }
        }
    }
}
