using System.Collections;
using AutoMapper;
using Contracts;
using Contracts.DTOs.Stories;
using Contracts.DTOs.Users;
using Contracts.Helpers;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Options;
using Services.Absractions;
using Services.DbEnum;
using Services.DTOs.Episodes;
using Services.DTOs.StoriyCategories;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IRepositoryManager repositoryManager,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        public async Task<UserResponseDTO> CreateUserAsync(UserCreateCustomerAuthen userCreateCustomerAuthen)
        {
            User? existUser = await _repositoryManager.UserRepository.GetUser(userCreateCustomerAuthen.ProviderToken);

            if (!(existUser is null))
            {
                existUser.Email = userCreateCustomerAuthen.Email;
                existUser.Password = userCreateCustomerAuthen.Password;
                existUser.ProfilePicture = userCreateCustomerAuthen.ProfilePicture;
                await _repositoryManager.UserRepository.UpdateAsync(existUser);

                UserResponseDTO response = _mapper.Map<User?, UserResponseDTO>(existUser);

                return response;
            }
            else
            {
                User user = new User()
                {
                    Email = userCreateCustomerAuthen.Email,
                    Password = userCreateCustomerAuthen.Password,
                    ProfilePicture = userCreateCustomerAuthen.ProfilePicture,
                    ProviderToken = userCreateCustomerAuthen.ProviderToken
                };
                _repositoryManager.UserRepository.Insert(user);

                UserResponseDTO response = _mapper.Map<User?, UserResponseDTO>(user);

                return response;
            }
        }

        public async Task<UserResponseDTO?> GetUserAsync(String ProviderToken)
        {
            User? user = await _repositoryManager.UserRepository.GetUser(ProviderToken);

            UserResponseDTO? response = _mapper.Map<User?, UserResponseDTO?>(user);

            return response;
        }
    }
}