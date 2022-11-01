using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TODOs.Data;
using System.Linq;

namespace TODOs.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodosController: ControllerBase
    {
        private readonly TodoDbContext _dbContext;
        private readonly ILogger<ListsController> _logger;

        public TodosController(TodoDbContext todoDbContext, ILogger<ListsController> logger)
        {
            _dbContext = todoDbContext;
            _logger = logger;
        }

        [HttpGet]
        public List<Data.Entities.Todo> GetAllAsync()
        {
            return _dbContext.Lists.SelectMany(prop => prop.Todos).ToList();
        }
    }
}

