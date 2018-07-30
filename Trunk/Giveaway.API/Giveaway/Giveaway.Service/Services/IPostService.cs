﻿using Giveaway.Data.Models;
using Giveaway.Data.Models.Database;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Giveaway.Service.Services
{
    public interface IPostService : IEntityService<Post>
    {
        ResponseMessage GetAllPost();
    }

    public class PostService : EntityService<Post>, IPostService
    {
        public ResponseMessage GetAllPost()
        {
            var posts = All();
            return new ResponseMessage(HttpStatusCode.OK, data: posts);
        }
    }
}
