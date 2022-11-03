using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TODOs.Api.Repositories.Contracts
{
    public interface IListRepository
    {
        /// <summary>
        /// Provide a list entity by an identifier
        /// </summary>
        /// <param name="entityId">An identifier</param>
        Task<Data.Entities.List> GetByIdAsync(int entityId);

        /// <summary>
        /// Create a new list record
        /// </summary>
        /// <param name="item">an entity</param>
        Task<Data.Entities.List> AddAsync(Data.Entities.List entity);

        // TODO: Pagination
        /// <summary>
        /// Provides all List records
        /// </summary>
        Task<List<Data.Entities.List>> GetAllAsync();
    }
}

