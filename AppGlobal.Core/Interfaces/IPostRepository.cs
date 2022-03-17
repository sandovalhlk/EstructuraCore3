using AppGlobal.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppGlobal.Core.Interfaces
{
    public  interface IPostRepository : IRepository<Post>
    {
      Task<IEnumerable<Post>> GetPostsByUser(int id);

    }
}
