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
        protected readonly DbSet<T> _entitties;
        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            _entitties = context.Set<T>();
        }

        public  IEnumerable<T> GetAll()
        {
            return  _entitties.AsEnumerable();
        }
        public async Task<T> GetById(int id) 
        {
            return await _entitties.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await  _entitties.AddAsync(entity);
            
        }

        public void Update(T entity)
        {

            _entitties.Update(entity);
            
        }

        public async Task Delete(int id)
        {
           T entity= await GetById(id);
            _entitties.Remove(entity);
            
        }

        
        
        
    }
}
