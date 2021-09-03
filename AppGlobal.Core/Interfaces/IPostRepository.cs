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
    }
}
