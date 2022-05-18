﻿using AdopPix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdopPix.Procedure.IProcedure
{
    public interface IPostProcedure
    {
        Task CreateAsync(Post entity);
        Task CreateImageAsync(PostImage entity);
        Task<List<Post>> FindAllAsync();
        Task<Post> FindByPostId(string postId);
        Task<PostImage> FindImageByPostIdAsync(string postId);
        Task DeletePostAsync(Post postId);
        Task DeleteImageAsync(PostImage postId);
        Task UpdatePostAsync(Post entity);
        Task UpdateImageAsync(PostImage imageId);
        Task LikeAsync(PostLike postId);
        Task UnLikeAsync(PostLike postId);
        Task<List<PostImage>> GetAllImageAsync();
    }
}
