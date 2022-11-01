using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TODOs.Api.Controllers
{
    [ApiController]
    [Route("v1/todos")]
    public class TodosController: ControllerBase
    {
        [HttpGet]
        public Task<List<string>> GetAllAsync()
        {
            return Task.FromResult(new List<string> { "Toto 1", "Todo 2" });
        }
    }
}

