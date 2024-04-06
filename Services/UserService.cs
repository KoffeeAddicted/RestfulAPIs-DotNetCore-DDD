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

            _repositoryManager.UserRepository.Insert(user);

            UserResponseDTO response = _mapper.Map<User, UserResponseDTO>(user);

            return response;
        }
    }
}
