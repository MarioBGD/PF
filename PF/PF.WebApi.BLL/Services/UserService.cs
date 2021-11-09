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
