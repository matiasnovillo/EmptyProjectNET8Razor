using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.CMSCore.DTOs;
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

namespace FiyiStore.Areas.CMSCore.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<Role> AsQueryable();

        #region Queries
        int Count();

        Role? GetByRoleId(int testId);

        List<Role?> GetAll();

        paginatedRoleDTO GetAllByNamePaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        bool Add(Role test);

        bool Update(Role test);

        bool DeleteByRoleId(int testId);
        #endregion

        #region Other methods
        DataTable GetAllInDataTable();
        #endregion
    }
}
