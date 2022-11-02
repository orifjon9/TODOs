using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODOs.Data.Entities;

namespace TODOs.Api.Repositories.Contracts
{
    public interface ITodoRepository
    {
        /// <summary>
        /// Search Todo records based on input parameters
        /// </summary>
        /// <param name="isLinkedToList">Has any linked lists</param>
        /// <param name="listId">Linked a list id</param>
        Task<List<Data.Entities.Todo>> SearchAsync(bool? isLinkedToList, int? listId);

        /// <summary>
        /// Provide a Todo entity by an identifier
        /// </summary>
        /// <param name="entityId">An identifier</param>
        Task<Data.Entities.Todo> GetByIdAsync(int entityId);

        /// <summary>
        /// Create a new Todo record
        /// </summary>
        /// <param name="entity">A Todo entity</param>
        Task<Data.Entities.Todo> AddAsync(Data.Entities.Todo entity);

        /// <summary>
        /// Updates the asking Todo entity
        /// </summary>
        /// <param name="entity">Modifying entity</param>
        Task<Data.Entities.Todo> UpdateAsync(Data.Entities.Todo entity);

        /// <summary>
        /// Removes a Todo record by an identifier
        /// </summary>
        /// <param name="entity">Deleting entity</param>
        Task DeleteAsync(Data.Entities.Todo entity);
    }
}

