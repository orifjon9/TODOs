using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODOs.Api.Repositories.Contracts;
using System.Linq;

namespace TODOs.Api.Repositories
{
    public class ListRepository: BaseRepository<Data.Entities.List>, IListRepository
    {
        public ListRepository(Data.TodoDbContext dbContext) : base(dbContext) { }

        public Task<List<Data.Entities.List>> GetAllAsync() =>  Task.FromResult(Find(prop => true).ToList());

        public Task<Data.Entities.List> AddAsync(Data.Entities.List entity) => CreateAsync(entity);
    }
}

