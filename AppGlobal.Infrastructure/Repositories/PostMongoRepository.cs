using AppGlobal.Core.Entidades;
using AppGlobal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Infrastructure.Repositories
{
    public class PostMongoRepository : IPostRepository
    {
        public async Task<IEnumerable<Post>> GetPost()
        {
            throw new NotImplementedException();
        }
    }
}
