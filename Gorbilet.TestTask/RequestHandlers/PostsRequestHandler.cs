using Gorbilet.TestTask.DTOs;
using Gorbilet.TestTask.Services;
using Gorbilet.TestTask.Services.Contracts;

namespace Gorbilet.TestTask.RequestHandlers
{
    public class PostsRequestHandler
    {
        private readonly RedisService _redis;

        public PostsRequestHandler(RedisService redis)
        {
            _redis = redis;
        }

        public Task CreatePost(Post post)
        {
            return _redis.SetValueAsync<PostInStorage>(post.Id, ToPostInStorage(post));
        }

        public async Task<Post?> GetPost(string id)
        {
            var postInStorage = await _redis.GetValueAsync<PostInStorage>(id);
            return postInStorage != default ? ToPost(id, postInStorage) : default;
        }

        public Task<bool> UpdatePost(string id, UpdatePost post)
        {
            return _redis.UpdateValueAsync<PostInStorage>(id, ToPostInStorage(post));
        }

        public Task<bool> DeletePost(string id)
        {
            return _redis.DeleteValueAsync<PostInStorage>(id);
        }

        private PostInStorage ToPostInStorage(Post post)
        {
            return new PostInStorage
            {
                Title = post.Title,
                Content = post.Content,
            };
        }

        private PostInStorage ToPostInStorage(UpdatePost post)
        {
            return new PostInStorage
            {
                Title = post.Title,
                Content = post.Content,
            };
        }

        private Post ToPost(string id, PostInStorage postInStorage)
        {
            return new Post
            {
                Id = id,
                Title = postInStorage.Title,
                Content = postInStorage.Content,
            };
        }
    }
}
