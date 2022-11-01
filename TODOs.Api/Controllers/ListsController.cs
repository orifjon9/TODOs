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
        public async Task<IEnumerable<Data.Entities.TodoList>> GetAllAsync()
        {

            return await _dbContext.Lists.ToListAsync();
            //return await Task.FromResult(new List<string> { "Hello", "Orifjon" });
        }

        [HttpGet("{listId}")]
        public async Task<string> GetByIdAsync(int listId)
        {
            return await Task.FromResult($"Hello {listId}");
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

