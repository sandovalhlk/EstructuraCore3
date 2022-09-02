using AppGlobal.Core.QueryFilters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppGlobal.Infrastructure.Interfaces
{
 public interface IUriService
    {

        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}
