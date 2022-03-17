using AppGlobal.Core.Entidades;
using AppGlobal.Core.Interfaces;
using AppGlobal.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace AppGlobal.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;
        //private readonly DbSet<T> _entitties;
        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }

        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);

        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public IRepository<Comment> CommentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

        public void Dispose()
        {
            if (_context!=null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsyn()
        {
        await _context.SaveChangesAsync();
        }
    }
}
