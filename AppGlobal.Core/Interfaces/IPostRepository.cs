using AppGlobal.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Core.Interfaces
{
  public  interface IPostRepository
    {
      Task<IEnumerable<Post>> GetPost();

        Task<Post> GetPost(int id);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(int id);
    }
}
