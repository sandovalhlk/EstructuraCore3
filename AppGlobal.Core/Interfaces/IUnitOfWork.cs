using AppGlobal.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Core.Interfaces
{
  public  interface IUnitOfWork : IDisposable
    {
        IPostRepository PostRepository { get; }

        IRepository<User> UserRepository { get; }

        IRepository<Comment> CommentRepository { get; }

        void SaveChanges();

        Task SaveChangesAsyn();

    }
}
