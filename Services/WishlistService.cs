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

        public async Task<WishlistResponseDTO?> GetStoryByWishlist(String ProviderToken, Int64 StoryId)
        {
            Wishlist?  story = await _repositoryManager.WishlistRepository.getStoryByWishList(ProviderToken, StoryId);

            WishlistResponseDTO storiesResponse =
            _mapper.Map<Wishlist?, WishlistResponseDTO>(story);


            return storiesResponse;
        }

        public async Task<WishlistResponseDTO> DeleteStoryWishList(StoryUserWishlistCreate storyUserWishlistCreate)
        {

            Wishlist wishlist = _mapper.Map<StoryUserWishlistCreate, Wishlist>(storyUserWishlistCreate);


            _repositoryManager.WishlistRepository.Delete(wishlist);


            WishlistResponseDTO response = _mapper.Map<Wishlist, WishlistResponseDTO>(wishlist);

            return response;
        }

    }
}
