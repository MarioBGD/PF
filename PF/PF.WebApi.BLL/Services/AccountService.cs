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
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;

        public AccountService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<AuthTokenDTO> Login(AuthDTO authData)
        {
            using (UnitOfWork uow = new UnitOfWork())
            {
                var authRepo = new AuthRepository(uow);
                AuthEntity auth = await authRepo.GetUserByLogin(authData.Login);

                if (auth == null || auth.HashedPassword != authData.HashedPassword)
                {
                    return new AuthTokenDTO
                    {
                        Message = "Niepoprawne dane logowania",
                        Success = false
                    };
                }

                return new AuthTokenDTO
                {
                    Success = true,
                    UserId = auth.UserId
                };
            }
        }

        public async Task<AuthTokenDTO> Register(AuthDTO authDataDto)
        {
            try
            {
                var validator = new PF.Common.Validators.AuthValidator();
                validator.ValidateLogin(authDataDto.Login);
            }
            catch (Exception e)
            {
                return new AuthTokenDTO
                {
                    Message = e.ToString()
                };
            }

            //if (string.IsNullOrEmpty(authData.HashedPassword) || authData.HashedPassword.Length < 60)
            //{
            //    return new AuthTokenDTO
            //    {
            //        Message = "Zly format hasha",
            //        Success = false
            //    };
            //}


            using (UnitOfWork uow = new UnitOfWork())
            {
                var authRepo = new AuthRepository(uow);
                AuthEntity auth = await authRepo.GetUserByLogin(authDataDto.Login);

                if (auth != null)
                {
                    return new AuthTokenDTO
                    {
                        Message = "Login juz zajety",
                        Success = false
                    };
                }

                //create new user
                var usersRepo = new UsersRepository(uow);
                UserEntity user = new UserEntity
                {
                    Login = authDataDto.Login,
                    Name = authDataDto.Name,
                    LastName = authDataDto.LastName
                };

                long userId = await usersRepo.Add(user);

                authDataDto.UserId = userId;
                AuthEntity authEntity = _mapper.Map<AuthEntity>(authDataDto);
                await authRepo.Add(authEntity);

                uow.Commit();

                return new AuthTokenDTO
                {
                    Success = true,
                    UserId = userId
                };
            }

            
        }
    }
}
