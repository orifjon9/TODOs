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

namespace TODOs.Api.Controllers
{
    [ApiController]
    [Route("v1/lists")]
    public class ListsController: ControllerBase
    {
        private readonly IListRepository _listRepository;
        private readonly ILogger<ListsController> _logger;

        public ListsController(IListRepository listRepository, ILogger<ListsController> logger)
        {
            _listRepository = listRepository;
            _logger = logger;
        }

        [HttpGet]
        public Task<List<Data.Entities.List>> GetAllAsync()
            => _listRepository.GetAllAsync();

        [HttpPost]
        public async Task<Data.Entities.List> AddAsync([FromBody]CreateListRequest payload)
        {
            var entity = new Data.Entities.List
            {
                Label = payload.Label
            };

            return await _listRepository.AddAsync(entity);
        }
    }
}

