using DALL.Interfase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modells;
using Serilog;

namespace ProjectDb1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {


        private readonly Ipost _Post;

        public PostController(Ipost Itodo)
        {
            _Post = Itodo;
        }


        [HttpGet]
        [Route("/PostGet")]
        public async Task<ActionResult<List<Post>>> Get()
        {
            List<Post> res = await _Post.GetAllPost();
            //Log.Information("getPost");
            return Ok(res);
        }

        [HttpPost]
        [Route("/PostsPost")]
        public async Task<ActionResult> Post([FromBody] Post post)
        {
            await _Post.AddPost(post);
            return Ok();
        }

        [HttpDelete]
        [Route("/PostDelete{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _Post.DeletePost(id);
            return Ok();
        }

        [HttpPut]
        [Route("/PostPut{id}")]
        public async Task<ActionResult> Put(int id, Post post)
        {

            await _Post.UpdatePost(id, post);
            return Ok();
        }
    }

    
}
