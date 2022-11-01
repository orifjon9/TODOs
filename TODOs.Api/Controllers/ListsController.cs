using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TODOs.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TODOs.Api.Controllers
{
    [ApiController]
    [Route("v1/lists")]
    public class ListsController: ControllerBase
    {
        private readonly TodoDbContext _dbContext;
        private readonly ILogger<ListsController> _logger;

        public ListsController(TodoDbContext todoDbContext, ILogger<ListsController> logger)
        {
            _dbContext = todoDbContext;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Data.Entities.List>> GetAllAsync()
        {
            return await _dbContext.Lists
                .AsNoTracking()
                .AsQueryable()
                .ToListAsync();
        }

        [HttpGet("{listId}")]
        public async Task<ActionResult<Data.Entities.List>> GetByIdAsync(int listId)
        {
            var item = await _dbContext.Lists.FirstOrDefaultAsync(prop => prop.Id == listId);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("{listId}")]
        public async Task<string> UpdateAsync(int listId)
        {
            return await Task.FromResult($"Hello {listId}");
        }

        [HttpDelete("{listId}")]
        public async Task<string> DeleteAsync(int listId)
        {
            return await Task.FromResult($"Hello {listId}");
        }
    }
}

