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
    public interface IUserRepository
    {
        IQueryable<User> AsQueryable();

        #region Queries
        int Count();

        User? GetByUserId(int userId);

        List<User?> GetAll();

        paginatedUserDTO GetAllByUserIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);

        public User GetByEmailAndPassword(string email, string password);
        #endregion

        #region Non-Queries
        bool Add(User user);

        bool Update(User user);

        bool DeleteByUserId(int userId);

        bool CopyByUserId(int userId);
        #endregion
    }
}
