using AppGlobal.Core.Entidades;
using AppGlobal.Core.Interfaces;
using AppGlobal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppGlobal.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        private  readonly DbSet<T> _entitties;
        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            _entitties = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return  _entitties.AsEnumerable();
        }
        public async Task<T> GetById(int id) 
        {
            return await _entitties.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            _entitties.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {

            _entitties.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
           T entity= await GetById(id);
            _entitties.Remove(entity);
            await _context.SaveChangesAsync();
        }

        
        
        
    }
}
