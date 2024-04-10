using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.DTOs.Stories;
using Contracts.DTOs.Users;
using Contracts.DTOs.Wishlist;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Options;
using Services.Absractions;

namespace Services
{
    public class WishlistService : IWishlistService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public WishlistService(IRepositoryManager repositoryManager,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public async Task<WishlistResponseDTO> AddStoryUserWishList(StoryUserWishlistCreate storyUserWishlistCreate)
        {

            Wishlist wishlist = _mapper.Map<StoryUserWishlistCreate, Wishlist>(storyUserWishlistCreate);

            
           _repositoryManager.WishlistRepository.Insert(wishlist);


            WishlistResponseDTO response = _mapper.Map<Wishlist, WishlistResponseDTO>(wishlist);

            return response;
        }

        public async Task<IEnumerable<WishlistResponseDTO>> GetUserWishList(String ProviderToken)
        {
            IEnumerable<Wishlist> storiesWishlist = await _repositoryManager.WishlistRepository.getStoriesUserWishList(ProviderToken);

            IEnumerable<WishlistResponseDTO> storiesResponse =
            _mapper.Map<IEnumerable<Wishlist>, IEnumerable<WishlistResponseDTO>>(storiesWishlist);


            return storiesResponse;
        }
    }
}
