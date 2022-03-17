using AppGlobal.Core.Entidades;
using AppGlobal.Core.Interfaces;
using AppGlobal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AppGlobal.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {

        public PostRepository(SocialMediaContext context): base(context) { }
        

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entitties.Where(x => x.UserId == userId).ToListAsync();
        }

      
    }
}
