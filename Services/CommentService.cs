using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.DTOs.Comment;
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
    public class CommentService : ICommentService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CommentService(IRepositoryManager repositoryManager,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public async Task<IEnumerable<CommentResponseDTO>> getStoriesComment(Int64 StroryId)
        {

            IEnumerable<Comment>? comments = await _repositoryManager.CommentRepository.getStoriesCommentByUser(StroryId);

            IEnumerable<CommentResponseDTO> responses = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResponseDTO>> (comments);

            return responses;

        }

        public async Task<Comment> addStoryCommentByUser(RequestAddCommentDTO requestBody)
        {

            Comment comment = _mapper.Map<RequestAddCommentDTO, Comment>(requestBody);


            _repositoryManager.CommentRepository.Insert(comment);



            return comment;

        }
    }
}