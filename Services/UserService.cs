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

            User user = _mapper.Map<UserCreateCustomerAuthen, User>(userCreateCustomerAuthen);

            User? existUser = await _repositoryManager.UserRepository.GetUser(user.ProviderToken);

            if (existUser != null)
            {
                User updatedData = new User
                {   Id = existUser.Id, 
                    Email = user.Email, 
                    Password = user.Password, 
                    ProviderToken = existUser.ProviderToken, 
                    ProfilePicture = user.ProfilePicture,
                    IsAdmin = existUser.IsAdmin,
                    IsDeleted = existUser.IsDeleted,
                    Wishlists = existUser.Wishlists
                }
                 ;
                _repositoryManager.UserRepository.UpdateAsync(updatedData);
            }
            else
            {
              _repositoryManager.UserRepository.Insert(user);
            }

            UserResponseDTO response = _mapper.Map<User?, UserResponseDTO>(user);

            return response;
        }

        public async Task<UserResponseDTO?> GetUserAsync(String ProviderToken)
        {
            User? user = await _repositoryManager.UserRepository.GetUser(ProviderToken);

            UserResponseDTO? response = _mapper.Map<User?, UserResponseDTO?>(user);

            return response;
        }
    }
}
