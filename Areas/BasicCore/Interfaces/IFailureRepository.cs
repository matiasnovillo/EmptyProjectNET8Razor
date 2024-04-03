using FiyiStore.Areas.BasicCore.Entities;
using FiyiStore.Areas.BasicCore.DTOs;
using System.Data;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2024
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

namespace FiyiStore.Areas.BasicCore.Interfaces
{
    public interface IFailureRepository
    {
        IQueryable<Failure> AsQueryable();

        #region Queries
        int Count();

        Failure? GetByFailureId(int failureId);

        List<Failure?> GetAll();

        paginatedFailureDTO GetAllByFailureIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(Failure failure);

        bool Update(Failure failure);

        bool DeleteByFailureId(int failureId);
        #endregion
    }
}
