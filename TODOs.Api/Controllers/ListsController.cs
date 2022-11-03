using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TODOs.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TODOs.Api.Repositories.Contracts;
using TODOs.Api.Repositories;
using TODOs.Api.Models.Requests;
using Microsoft.AspNetCore.Http;
using TODOs.Api.Exceptions;

namespace TODOs.Api.Controllers
{
    [ApiController]
    [Route("v1/lists")]
    public class ListsController: ControllerBase
    {
        private readonly IListRepository _listRepository;
        private readonly AutoMapper.IMapper _mapper;

        public ListsController(IListRepository listRepository, AutoMapper.IMapper mapper)
        {
            _listRepository = listRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Models.Responses.ListViewModel>> GetAllAsync()
            => _mapper.Map<List<Models.Responses.ListViewModel>>(await _listRepository.GetAllAsync());

        [HttpGet("{listId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.Responses.ListViewModel>> GetByIdAsync([FromRoute] int listId)
        {
            var foundEntity = await _listRepository.GetByIdAsync(listId);
            if (foundEntity == null)
            {
                throw new NotFoundException("A list was not found");
            }
            return _mapper.Map<Models.Responses.ListViewModel>(foundEntity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.Responses.ListViewModel>> AddAsync([FromBody]CreateListRequest payload)
        {
            var entity = _mapper.Map<Data.Entities.List>(payload);
            var createdEntity = _mapper.Map<Models.Responses.ListViewModel>(await _listRepository.AddAsync(entity));
            return CreatedAtAction(nameof(GetByIdAsync), new { listId = createdEntity.Id }, createdEntity);
        }
    }
}

