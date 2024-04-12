using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.BasicCore;
using FiyiStore.Areas.FiyiStore.Entities;
using FiyiStore.Areas.FiyiStore.DTOs;
using FiyiStore.Areas.FiyiStore.Interfaces;
using FiyiStore.Library;
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

namespace FiyiStore.Areas.FiyiStore.Repositories
{
    public class ClientRepository : IClientRepository
    {
        protected readonly FiyiStoreContext _context;

        public ClientRepository(FiyiStoreContext context)
        {
            _context = context;
        }

        public IQueryable<Client> AsQueryable()
        {
            try
            {
                return _context.Client.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public int Count()
        {
            try
            {
                return _context.Client.Count();
            }
            catch (Exception) { throw; }
        }

        public Client? GetByClientId(int clientId)
        {
            try
            {
                return _context.Client
                            .FirstOrDefault(x => x.ClientId == clientId);
            }
            catch (Exception) { throw; }
        }

        public List<Client?> GetAll()
        {
            try
            {
                return _context.Client.ToList();
            }
            catch (Exception) { throw; }
        }

        public paginatedClientDTO GetAllByClientIdPaginated(string textToSearch,
            bool strictSearch,
            int pageIndex, 
            int pageSize)
        {
            try
            {
                //textToSearch: "novillo matias  com" -> words: {novillo,matias,com}
                string[] words = Regex
                    .Replace(textToSearch
                    .Trim(), @"\s+", " ")
                    .Split(" ");

                int TotalClient = _context.Client.Count();

                var query = from client in _context.Client
                            join userCreation in _context.User on client.UserCreationId equals userCreation.UserId
                            join userLastModification in _context.User on client.UserLastModificationId equals userLastModification.UserId
                            select new { Client = client, UserCreation = userCreation, UserLastModification = userLastModification };

                // Extraemos los resultados en listas separadas
                List<Client> lstClient = query.Select(result => result.Client)
                        .Where(x => strictSearch ?
                            words.All(word => x.ClientId.ToString().Contains(word)) :
                            words.Any(word => x.ClientId.ToString().Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = query.Select(result => result.UserCreation).ToList();
                List<User> lstUserLastModification = query.Select(result => result.UserLastModification).ToList();

                return new paginatedClientDTO
                {
                    lstClient = lstClient,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    TotalItems = TotalClient,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }
        #endregion

        #region Non-Queries
        public int Add(Client client)
        {
            try
            {
                _context.Client.Add(client);
                _context.SaveChanges();
                
                return client.ClientId;   
            }
            catch (Exception) { throw; }
        }

        public int Update(Client client)
        {
            try
            {
                _context.Client.Update(client);
                return _context.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public int DeleteByClientId(int clientId)
        {
            try
            {
                int RowsDeleted = AsQueryable()
                        .Where(x => x.ClientId == clientId)
                        .ExecuteDelete();

                _context.SaveChanges();

                return RowsDeleted;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ajax"></param>
        /// <param name="deleteType"></param>
        /// <returns>Return the rows deleted</returns>
        public string DeleteManyOrAll(Ajax ajax, string deleteType)
        {
            try
            {
                if (deleteType == "All")
                {
                    var RegistersToDelete = _context.Client.ToList();
                    
                    List<Client> lstClient = _context.Client.ToList();
                    string RowsDeleted = "";

                    for (int i = 0; i < lstClient.Count; i++)
                    {
                        RowsDeleted += $@"{lstClient[i].ClientId},"; 
                    }

                    RowsDeleted = RowsDeleted.TrimEnd(',');

                    _context.Client.RemoveRange(RegistersToDelete);

                    _context.SaveChanges();

                    return RowsDeleted;
                }
                else
                {
                    string[] RowsChecked = ajax.AjaxForString.Split(',');

                    for (int i = 0; i < RowsChecked.Length; i++)
                    {
                        _context.Client
                                    .Where(x => x.ClientId == Convert.ToInt32(RowsChecked[i]))
                                    .ExecuteDelete();

                        _context.SaveChanges();
                    }

                    ajax.AjaxForString = ajax.AjaxForString.TrimEnd(',');

                    return ajax.AjaxForString;
                }
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns>The last entered ID after the copy transaction</returns>        
        public int CopyByClientId(int clientId)
        {
            try
            {
                Client Client = _context.Client
                                .Where(x => x.ClientId == clientId)
                                .FirstOrDefault();

                Client.ClientId = 0;

                _context.Client.Add(Client);
                _context.SaveChanges();

                return _context.Client
                                .OrderByDescending(x => x.ClientId)
                                .FirstOrDefault().ClientId;
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ajax"></param>
        /// <param name="copyType"></param>
        /// <returns>The number of rows copied</returns>
        public int CopyManyOrAll(Ajax ajax, string copyType)
        {
            try
            {
                int NumberOfRegistersEntered = 0;

                if (copyType == "All")
                {
                    List<Client> lstClient = [];
                    lstClient = _context.Client.ToList();

                    for (int i = 0; i < lstClient.Count; i++)
                    {
                        Client Client = lstClient[i];
                        Client.ClientId = 0;
                        _context.Client.Add(Client);
                        NumberOfRegistersEntered += _context.SaveChanges();
                    }
                }
                else
                {
                    string[] RowsChecked = ajax.AjaxForString.Split(',');

                    for (int i = 0; i < RowsChecked.Length; i++)
                    {
                        Client Client = _context.Client
                                                    .Where(x => x.ClientId == Convert.ToInt32(RowsChecked[i]))
                                                    .FirstOrDefault();
                        Client.ClientId = 0;
                        _context.Client.Add(Client);
                        NumberOfRegistersEntered += _context.SaveChanges();
                    }
                }

                return NumberOfRegistersEntered;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
