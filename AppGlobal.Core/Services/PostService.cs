using AppGlobal.Core.Entidades;
using AppGlobal.Core.Interfaces;
using System;
using System.Collections.Generic;
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

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
            
        }

    public async Task<Post> GetPost(int id)
        {

            return await _unitOfWork.PostRepository.GetById(id);
        }

        public  IEnumerable<Post> GetPosts()
        {
            return  _unitOfWork.PostRepository.GetAll();
        }


      
        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.PostRepository.GetById(post.UserId);
            if (user==null)
            {
                throw new Exception("User dosen't exist");
            }

            if (post.Description.Contains("sexo"))
            {
                throw new Exception("Content not allowed");
            }

            await _unitOfWork.PostRepository.Add(post);

        }
        public async Task<bool> UpdatePost(Post post)
        {
             await _unitOfWork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

    }

}