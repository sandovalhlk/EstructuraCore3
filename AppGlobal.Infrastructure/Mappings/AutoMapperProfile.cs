using AppGlobal.Core.DTOs;
using AppGlobal.Core.Entidades;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Infrastructure.Mappings
{
   public  class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }

    }
}
