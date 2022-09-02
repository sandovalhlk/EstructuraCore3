using AppGlobal.Core.CustomEntities;
using AppGlobal.Core.Entidades;
using AppGlobal.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Core.Interfaces
{
    public interface IPostService 
    {

        //IEnumerable<Post> GetPosts();

        PagedList<Post> GetPosts(PostQueryFilter filters);
        //IEnumerable<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPost(int id);

        Task InsertPost(Post post);

        Task<bool> UpdatePost(Post post);

        Task<bool> DeletePost(int id);

    }
}
