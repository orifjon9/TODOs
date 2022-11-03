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
using Microsoft.AspNetCore.Http;
using TODOs.Api.Exceptions;

namespace TODOs.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly AutoMapper.IMapper _mapper;

        public TodosController(ITodoRepository todoRepository, AutoMapper.IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        [HttpGet("lists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Models.Responses.TodoViewModel>> GetAllAsync()
            => _mapper.Map<List<Models.Responses.TodoViewModel>>(await _todoRepository.SearchAsync(true, null));

        [HttpGet("lists/{listId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Models.Responses.TodoViewModel>> GetByListIdAsync([FromRoute] int listId)
            => _mapper.Map<List<Models.Responses.TodoViewModel>>(await _todoRepository.SearchAsync(null, listId));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Models.Responses.TodoViewModel>> AddAsync([FromBody] CreateUpdateTodoRequest payload)
        {
            var entity = _mapper.Map<Data.Entities.Todo>(payload);
            var createdEntity = await _todoRepository.AddAsync(entity);
            return CreatedAtAction(nameof(GetByIdAsync), new { todoId = createdEntity.Id }, createdEntity);
        }

        [HttpGet("{todoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Responses.TodoViewModel>> GetByIdAsync([FromRoute] int todoId)
        {
            var foundEntity = await _todoRepository.GetByIdAsync(todoId);
            if (foundEntity == null)
            {
                throw new NotFoundException("A todo was not found");
            }

            return _mapper.Map<Models.Responses.TodoViewModel>(foundEntity);
        }
        [HttpPut("{todoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Responses.TodoViewModel>> UpdateAsync([FromRoute] int todoId,
            [FromBody] CreateUpdateTodoRequest payload)
        {
            var foundEntity = await _todoRepository.GetByIdAsync(todoId);
            if (foundEntity == null)
            {
                throw new NotFoundException("A todo was not found");
            }

            var entity = _mapper.Map<Data.Entities.Todo>(payload);
            entity.Id = foundEntity.Id;

            return _mapper.Map<Models.Responses.TodoViewModel>(await _todoRepository.UpdateAsync(entity));
        }

        [HttpPatch("{todoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.Responses.TodoViewModel>> PatchAsync([FromRoute] int todoId,
            [FromBody] JsonPatchDocument<Data.Entities.Todo> payload)
        {
            var foundEntity = await _todoRepository.GetByIdAsync(todoId);
            if (foundEntity == null)
            {
                throw new NotFoundException("A todo was not found");
            }

            payload.ApplyTo(foundEntity);    
            return _mapper.Map<Models.Responses.TodoViewModel>(await _todoRepository.UpdateAsync(foundEntity));
        }

        [HttpDelete("{todoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync([FromRoute] int todoId)
        {
            var entity = await _todoRepository.GetByIdAsync(todoId);
            if (entity == null)
            {
                throw new NotFoundException("A todo was not found");
            }

            await _todoRepository.DeleteAsync(entity);
            return NoContent();
        }
    }
}

