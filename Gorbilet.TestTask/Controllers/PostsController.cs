using Gorbilet.TestTask.DTOs;
using Gorbilet.TestTask.RequestHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Gorbilet.TestTask.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostsController : ControllerBase
    {

        private readonly PostsRequestHandler _postsRequestHandler;

        public PostsController(PostsRequestHandler postsRequestHandler)
        {
            _postsRequestHandler = postsRequestHandler;
        }

        [HttpPost("")]
        public async Task<ActionResult> CreatePost(Post post)
        {
            await _postsRequestHandler.CreatePost(post);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(string id)
        {
            var post = await _postsRequestHandler.GetPost(id);
            return post != null ? Ok(post) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(string id, UpdatePost post)
        {
            var updated = await _postsRequestHandler.UpdatePost(id, post);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(string id)
        {
            var deleted = await _postsRequestHandler.DeletePost(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
