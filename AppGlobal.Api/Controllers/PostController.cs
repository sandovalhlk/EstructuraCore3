using AppGlobal.Api.Responses;
using AppGlobal.Core.CustomEntities;
using AppGlobal.Core.DTOs;
using AppGlobal.Core.Entidades;
using AppGlobal.Core.Interfaces;
using AppGlobal.Core.QueryFilters;
using AppGlobal.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AppGlobal.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        
        
        //[HttpGet]
        //public  IActionResult GetPost()
        //{
        //    var posts =  _postService.GetPosts();
        //    var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
        //    var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
        //    return Ok(response);
        //}

        [HttpGet(Name =nameof(GetPost))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type=typeof(ApiResponse<IEnumerable<PostDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
        public  IActionResult GetPost([FromQuery] PostQueryFilter filters )
        {

            var posts = _postService.GetPosts(filters);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            //var postsDto = _mapper.Map<PagedList<PostDto>>(posts);
          
            //  var response = new ApiResponse<PagedList<PostDto>>(postsDto);
            var metadata = new Metadata{
                TotalCount=posts.TotalCount,
                PageSize=posts.PageSize,
                CurrentPage=posts.CurrentPage,
                TotalPages=posts.TotalPages,
                HasNextPage=posts.HasNextPage,
                HasPreviousPage=posts.HasPreviousPage,
                NextPageUrl= _uriService.GetPostPaginationUri(filters,Url.RouteUrl(nameof(GetPost))).ToString(),
                PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPost))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto) { 
            Meta=metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
            
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {

            var post = await _postService.GetPost(id);
            return Ok(post);
        }


        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _postService.InsertPost(post);
            postDto = _mapper.Map<PostDto>(post);

            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id,PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;
            var result= await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            
            var result =await _postService.DeletePost(id);
            return Ok(result);
        }

    }
}
