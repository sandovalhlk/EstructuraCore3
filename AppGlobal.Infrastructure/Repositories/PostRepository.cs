using AppGlobal.Core.Entidades;
using AppGlobal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        public Task<IEnumerable<Post>> GetPost()
        {
            throw new NotImplementedException();
        }
    }
}
