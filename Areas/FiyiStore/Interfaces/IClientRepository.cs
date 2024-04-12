using System.Data;
using FiyiStore.Areas.FiyiStore.Entities;
using FiyiStore.Areas.FiyiStore.DTOs;
using FiyiStore.Library;

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

namespace FiyiStore.Areas.FiyiStore.Interfaces
{
    public interface IClientRepository
    {
        IQueryable<Client> AsQueryable();

        #region Queries
        int Count();

        Client? GetByClientId(int clientId);

        List<Client?> GetAll();

        paginatedClientDTO GetAllByClientIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex,
            int pageSize);
        #endregion

        #region Non-Queries
        int Add(Client client);

        int Update(Client client);

        int DeleteByClientId(int client);

        string DeleteManyOrAll(Ajax Ajax, string DeleteType);

        int CopyByClientId(int ClientId);

        int CopyManyOrAll(Ajax Ajax, string CopyType);
        #endregion
    }
}
