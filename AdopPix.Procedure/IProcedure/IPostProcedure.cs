using AdopPix.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdopPix.Procedure.IProcedure
{
    public interface IPostProcedure
    {
        Task<string> CreateAsync(Post entity);
        Task CreateImageAsync(PostImage entity);
        Task<List<Post>> FindAllAsync();
        Task<List<Post>> FindAllAsyncNew();
        Task<List<Post>> FindAllAsyncOld();
        Task<List<Post>> FindAllAsyncLike();
        Task<Post> FindByPostId(string postId);
        Task<PostImage> FindImageByPostIdAsync(string postId);
        Task DeletePostAsync(Post postId);
        Task DeleteImageAsync(PostImage postId);
        Task UpdatePostAsync(Post entity);
        Task UpdateImageAsync(PostImage imageId);
        Task<List<PostImage>> GetAllImageAsync();
        Task LikeAsync(PostLike postlike);
        Task UnLikeAsync(PostLike postlike);
        Task <PostLike> CheckLikeStatusById(string postId, string userId);
        Task <int> ShowLikeById(string postId);
        /*Task CreateCommentAsync(PostComment postComment);
        Task DeleteCommentAsync(PostComment postComment);*/
    }
}
