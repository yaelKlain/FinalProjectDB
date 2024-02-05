using DALL.Interfase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modells;

namespace ProjectDb1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private readonly IToDo _ToDo;

        public ToDoController(IToDo Itodo)
        {
            _ToDo = Itodo;
        }

        [HttpGet]
        [Route("/ToDoGet")]
        public async Task<ActionResult<List<ToDo>>> Get()
        {
            List<ToDo> res = await _ToDo.GetAllToDo();
            return Ok(res);
        }

        [HttpPost]
        [Route("/ToDoPost")]
        public async Task<ActionResult> Post([FromBody] ToDo task)
        {
            await _ToDo.AddToDo(task);
            return Ok();
        }

        [HttpDelete]
        [Route("/ToDoDelete{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _ToDo.DeleteToDo(id);
            return Ok();
        }

        [HttpPut]
        [Route("/ToDoPut{id}")]
        public async Task<ActionResult> Put(int id,ToDo task)
        {

            await _ToDo.UpdateToDo(id,task);
            return Ok();
        }


    }
}
