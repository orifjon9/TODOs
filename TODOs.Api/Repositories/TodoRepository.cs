using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TODOs.Api.Repositories.Contracts;

namespace TODOs.Api.Repositories
{
    public class TodoRepository: BaseRepository<Data.Entities.Todo>, ITodoRepository
    {
        public TodoRepository(Data.TodoDbContext dbContext): base(dbContext) { }


        public Task<Data.Entities.Todo> GetByIdAsync(int entityId)
            => Find(prop => prop.Id == entityId).AsNoTracking().FirstOrDefaultAsync();

        public async Task<List<Data.Entities.Todo>> SearchAsync(bool? isLinkedToList, int? listId)
        {
            Expression<Func<Data.Entities.Todo, bool>> predicate = prop => prop.ListId > 0;
            if (listId.HasValue)
            {
                predicate = prop => prop.ListId == listId;
            }

            return await Find(predicate).ToListAsync();
        }

        public Task<Data.Entities.Todo> AddAsync(Data.Entities.Todo entity) => CreateAsync(entity);

        public Task<Data.Entities.Todo> UpdateAsync(Data.Entities.Todo entity) => EditAsync(entity);

        public Task DeleteAsync(Data.Entities.Todo entity) => RemoveAsync(entity);
    }
}

