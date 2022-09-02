using AppGlobal.Core.CustomEntities;
using AppGlobal.Core.Entidades;
using AppGlobal.Core.Exceptions;
using AppGlobal.Core.Interfaces;
using AppGlobal.Core.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Core.Services
{
   public class PostService : IPostService
    {
        //esto sera sustituido por unit of work
        //private readonly IRepository<Post> _postRepository;
        //private readonly IRepository<User> _userRepository;

        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork= unitOfWork;
            _paginationOptions = options.Value;
            
        }

    public async Task<Post> GetPost(int id)
        {

            return await _unitOfWork.PostRepository.GetById(id);
        }


        //Filtrado de informacion por conjunto de parametros dentro de un objeto
        //public IEnumerable<Post> GetPosts(PostQueryFilter filters)
        //{   
        //    var  posts  =_unitOfWork.PostRepository.GetAll();

        //    if (filters.UserId != null)
        //        posts = posts.Where(x=>x.UserId==filters.UserId);

        //    if (filters.Date != null)
        //        posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());

        //    if (filters.Description != null)
        //        posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));

        //    return posts;

        //}

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;

            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var posts = _unitOfWork.PostRepository.GetAll();

            if (filters.UserId != null)
                posts = posts.Where(x => x.UserId == filters.UserId);

            if (filters.Date != null)
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());

            if (filters.Description != null)
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));

            var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

            return pagedPosts;

        }


        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.PostRepository.GetById(post.UserId);
            if (user==null)
            {
                throw new Exception("User dosen't exist");
            }

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if(userPost.Count()<10)
            {
                var lasPost = userPost.LastOrDefault();
                if((lasPost.Date-DateTime.Now).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }

            if (post.Description.Contains("sexo"))
            {
                throw new BusinessException("Content not allowed");
            }

            await _unitOfWork.PostRepository.Add(post);

        }
        public async Task<bool> UpdatePost(Post post)
        {
              _unitOfWork.PostRepository.Update(post);
           await _unitOfWork.SaveChangesAsyn();
            return true;

        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

    }

}