using AppGlobal.Core.QueryFilters;
using AppGlobal.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Infrastructure.Services
{
   public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService( string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl= $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}
