using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using FiyiStore.Areas.CMSCore.Entities;
using FiyiStore.Areas.CMSCore.DTOs;
using FiyiStore.Areas.CMSCore.Interfaces;
using System.Data;
using FiyiStore.Areas.BasicCore;
using DocumentFormat.OpenXml.Spreadsheet;

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

namespace FiyiStore.Areas.CMSCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly FiyiStoreContext _context;

        public UserRepository(FiyiStoreContext context)
        {
            _context = context;
        }

        public IQueryable<User> AsQueryable()
        {
            try
            {
                return _context.User.AsQueryable();
            }
            catch (Exception) { throw; }
        }

        #region Queries
        public  int Count()
        {
            try
            {
                return _context.User.Count();
            }
            catch (Exception) { throw; }
        }

        public  User? GetByUserId(int userId)
        {
            try
            {
                return  _context.User
                                .FirstOrDefault(x => x.UserId == userId);
            }
            catch (Exception) { throw; }
        }

        public  List<User> GetAll()
        {
            try
            {
                return  _context.User.ToList();
            }
            catch (Exception) { throw; }
        }

        public paginatedUserDTO GetAllByUserIdPaginated(string textToSearch,
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

                int TotalUser =  _context.User.Count();

                var query = from user in _context.User
                            join userCreation in _context.User on user.UserCreationId equals userCreation.UserId
                            join userLastModification in _context.User on user.UserLastModificationId equals userLastModification.UserId
                            join role in _context.Role on user.RoleId equals role.RoleId
                            select new { User = user, UserCreation = userCreation, UserLastModification = userLastModification, Role = role };

                // Extraemos los resultados en listas separadas
                List<User> lstUser = query.Select(result => result.User)
                        .Where(x => strictSearch ?
                            words.All(word => x.UserId.ToString().Contains(word)) :
                            words.Any(word => x.UserId.ToString().Contains(word)))
                        .OrderByDescending(p => p.DateTimeLastModification)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
                List<User> lstUserCreation = query.Select(result => result.UserCreation).ToList();
                List<User> lstUserLastModification = query.Select(result => result.UserLastModification).ToList();
                List<Role> lstRole = query.Select(result => result.Role).ToList();

                return new paginatedUserDTO
                {
                    lstUser = lstUser,
                    lstUserCreation = lstUserCreation,
                    lstUserLastModification = lstUserLastModification,
                    lstRole = lstRole,
                    TotalItems = TotalUser,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            }
            catch (Exception) { throw; }
        }

        public User? GetByEmailAndPassword(string email,
            string password)
        {
            return  _context.User.AsQueryable()
                .Where(u => u.Password == password)
                .Where(u => u.Email == email)
                .FirstOrDefault();
        }
        #endregion

        #region Non-Queries
        public bool Add(User user)
        {
            try
            {
                 _context.User.Add(user);
                return  _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool Update(User user)
        {
            try
            {
                _context.User.Update(user);
                return  _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool CopyByUserId(int userId)
        {
            try
            {
                User userToCopy = _context.User
                                            .FirstOrDefault(x => x.UserId == userId);
                _context.User.Add(userToCopy);
                return _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }

        public bool DeleteByUserId(int userId)
        {
            try
            {
                AsQueryable()
                        .Where(x => x.UserId == userId)
                        .ExecuteDelete();

                return  _context.SaveChanges() > 0;
            }
            catch (Exception) { throw; }
        }
        #endregion
    }
}
