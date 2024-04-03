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
                AsQueryable()
                        .Where(x => x.ClientId == clientId)
                        .ExecuteDelete();

                return _context.SaveChanges();
            }
            catch (Exception) { throw; }
        }

        public void DeleteManyOrAll(Ajax Ajax, string DeleteType)
        {
            throw new NotImplementedException();
        }

        public int CopyByClientId(int ClientId)
        {
            throw new NotImplementedException();
        }

        public int[] CopyManyOrAll(Ajax Ajax, string CopyType)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DataTables
        public DataTable GetAllInDataTable()
        {
            try
            {
                List<Client> lstClient = _context.Client.ToList();

                DataTable DataTable = new();
                DataTable.Columns.Add("ClientId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("Age", typeof(string));
                DataTable.Columns.Add("EsCasado", typeof(string));
                DataTable.Columns.Add("BornDateTime", typeof(string));
                DataTable.Columns.Add("Height", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("ProfilePicture", typeof(string));
                DataTable.Columns.Add("FavouriteColour", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("PhoneNumber", typeof(string));
                DataTable.Columns.Add("Tags", typeof(string));
                DataTable.Columns.Add("About", typeof(string));
                DataTable.Columns.Add("AboutInTextEditor", typeof(string));
                DataTable.Columns.Add("WebPage", typeof(string));
                DataTable.Columns.Add("BornTime", typeof(string));
                DataTable.Columns.Add("Colour", typeof(string));
                

                foreach (Client client in lstClient)
                {
                    DataTable.Rows.Add(
                        client.ClientId,
                        client.Active,
                        client.DateTimeCreation,
                        client.DateTimeLastModification,
                        client.UserCreationId,
                        client.UserLastModificationId,
                        client.Name,
                        client.Age,
                        client.EsCasado,
                        client.BornDateTime,
                        client.Height,
                        client.Email,
                        client.ProfilePicture,
                        client.FavouriteColour,
                        client.Password,
                        client.PhoneNumber,
                        client.Tags,
                        client.About,
                        client.AboutInTextEditor,
                        client.WebPage,
                        client.BornTime,
                        client.Colour
                        
                        );
                }

                return DataTable;
            }
            catch (Exception) { throw; }
        }

        public DataTable GetByClientIdInDataTable(int clientId)
        {
            try
            {
                Client client = _context.Client
                                                                .Where(x => x.ClientId == clientId)         
                                                                .FirstOrDefault();

                DataTable DataTable = new();
                DataTable.Columns.Add("ClientId", typeof(string));
                DataTable.Columns.Add("Active", typeof(string));
                DataTable.Columns.Add("DateTimeCreation", typeof(string));
                DataTable.Columns.Add("DateTimeLastModification", typeof(string));
                DataTable.Columns.Add("UserCreationId", typeof(string));
                DataTable.Columns.Add("UserLastModificationId", typeof(string));
                DataTable.Columns.Add("Name", typeof(string));
                DataTable.Columns.Add("Age", typeof(string));
                DataTable.Columns.Add("EsCasado", typeof(string));
                DataTable.Columns.Add("BornDateTime", typeof(string));
                DataTable.Columns.Add("Height", typeof(string));
                DataTable.Columns.Add("Email", typeof(string));
                DataTable.Columns.Add("ProfilePicture", typeof(string));
                DataTable.Columns.Add("FavouriteColour", typeof(string));
                DataTable.Columns.Add("Password", typeof(string));
                DataTable.Columns.Add("PhoneNumber", typeof(string));
                DataTable.Columns.Add("Tags", typeof(string));
                DataTable.Columns.Add("About", typeof(string));
                DataTable.Columns.Add("AboutInTextEditor", typeof(string));
                DataTable.Columns.Add("WebPage", typeof(string));
                DataTable.Columns.Add("BornTime", typeof(string));
                DataTable.Columns.Add("Colour", typeof(string));
                

                DataTable.Rows.Add(
                        client.ClientId,
                        client.Active,
                        client.DateTimeCreation,
                        client.DateTimeLastModification,
                        client.UserCreationId,
                        client.UserLastModificationId,
                        client.Name,
                        client.Age,
                        client.EsCasado,
                        client.BornDateTime,
                        client.Height,
                        client.Email,
                        client.ProfilePicture,
                        client.FavouriteColour,
                        client.Password,
                        client.PhoneNumber,
                        client.Tags,
                        client.About,
                        client.AboutInTextEditor,
                        client.WebPage,
                        client.BornTime,
                        client.Colour
                        
                        );

                return DataTable;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
