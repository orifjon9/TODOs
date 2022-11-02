using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TODOs.Data;
using System.Linq;
using TODOs.Api.Repositories.Contracts;
using TODOs.Api.Models.Requests;
using TODOs.Data.Entities;
using TODOs.Api.Repositories;
using Microsoft.AspNetCore.JsonPatch;

namespace TODOs.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ILogger<ListsController> _logger;

        public TodosController(ITodoRepository todoRepository, ILogger<ListsController> logger)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }

        [HttpGet("lists")]
        public async Task<List<Data.Entities.Todo>> GetAllAsync()
        {
            return await _todoRepository.SearchAsync(true, null);
        }

        [HttpGet("lists/{listId}")]
        public async Task<List<Data.Entities.Todo>> GetByListIdAsync([FromRoute] int listId)
        {
            return await _todoRepository.SearchAsync(null, listId);
        }

        [HttpPost]
        public async Task<Data.Entities.Todo> AddAsync([FromBody] CreateUpdateTodoRequest payload)
        {
            var entity = new Data.Entities.Todo
            {
                Label = payload.Label,
                ListId = payload.ListId,
                Status = payload.Status
            };

            return await _todoRepository.AddAsync(entity);
        }

        [HttpPut("{todoId}")]
        public async Task<ActionResult<Data.Entities.Todo>> UpdateAsync([FromRoute] int todoId,
            [FromBody] CreateUpdateTodoRequest payload)
        {
            var entity = await _todoRepository.GetByIdAsync(todoId);
            if (entity == null)
            {
                return NotFound();
            }

            entity = new Data.Entities.Todo
            {
                Label = payload.Label,
                ListId = payload.ListId,
                Status = payload.Status
            };

            return await _todoRepository.UpdateAsync(entity);
        }

        [HttpPatch("{todoId}")]
        public async Task<ActionResult<Data.Entities.Todo>> PatchAsync([FromRoute] int todoId,
            [FromBody] JsonPatchDocument<Data.Entities.Todo> payload)
        {
            var entity = await _todoRepository.GetByIdAsync(todoId);
            if (entity == null)
            {
                return NotFound();
            }

            payload.ApplyTo(entity);

            return await _todoRepository.UpdateAsync(entity);
        }

        [HttpDelete("{todoId}")]
        public async Task<ActionResult<Data.Entities.Todo>> DeleteAsync([FromRoute] int todoId)
        {
            var entity = await _todoRepository.GetByIdAsync(todoId);
            if (entity == null)
            {
                return NotFound();
            }

            await _todoRepository.DeleteAsync(entity);
            return NoContent();
        }
    }
}

